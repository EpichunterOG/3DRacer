using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3D_Racer
{
    class Screen
    {

        public const float MAX_DIST = 50;
        int w, h;
        int[,] map;
        List<Texture2D> textures;
        List<Color[]> texPix;
        Color lessBlack = new Color(30, 30, 30);
        #region tempVars
        double cameraX, rayDirX, rayDirY, sideDistX, sideDistY, wallX, deltaDistX, deltaDistY, perpWallDist;
        int mapX, mapY, stepX, stepY, lineHeight, texNum, texX, texY, drawStart, drawEnd;
        bool hit;
        Color color;
        #endregion
        public Screen(int[,] map, List<Texture2D> textures, int w, int h)
        {

            this.map = map;
            this.textures = textures;
            this.w = w;
            this.h = h;
            texPix = new List<Color[]>();

            for (int i = 0; i < textures.Count; i++)
                texPix.Add(new Color[textures[0].Width * textures[0].Height]);

            for (int i = 0; i < textures.Count; i++)
            {
                textures[i].GetData<Color>(0, null, texPix[i], 0, textures[i].Width * textures[i].Height);
            }
        }
        public void update(Camera cam, Color[] pixels)
        {
            //clear the screen (top part is dark gray, bottom party is gray 
            //to make the top look like a celing and bottom like a floor)
            for (int n = 0; n < pixels.Length / 2; n++)
            {
                if (pixels[n] != lessBlack)
                    pixels[n] = lessBlack;
            }
            for (int i = pixels.Length / 2; i < pixels.Length; i++)
            {
                if (pixels[i] != Color.Black)
                    pixels[i] = Color.Black;
            }
            for (int x = 0; x < w; x++)
            {
                cameraX = 2 * x / (double)(w) - 1;
                rayDirX = cam.dir.X + cam.plane.X * cameraX;
                rayDirY = cam.dir.Y + cam.plane.Y * cameraX;
                // Map position
                mapX = (int)cam.pos.X;
                mapY = (int)cam.pos.Y;
                // length of ray from current position to next x or y-side
                // double sideDistX;
                // double sideDistY;
                // Length of ray from one side to next in map
                deltaDistX = Math.Sqrt(1 + (rayDirY * rayDirY)
                       / (rayDirX * rayDirX));
                deltaDistY = Math.Sqrt(1 + (rayDirX * rayDirX)
                       / (rayDirY * rayDirY));
                //double perpWallDist;
                // Direction to go in x and y
                // int stepX, stepY;
                hit = false;// was a wall hit
                int side = 0;// was the wall vertical or horizontal
                // Figure out the step direction and initial distance to a side
                if (rayDirX < 0)
                {
                    stepX = -1;
                    sideDistX = (cam.pos.X - mapX) * deltaDistX;
                }
                else
                {
                    stepX = 1;
                    sideDistX = (mapX + 1.0 - cam.pos.X) * deltaDistX;
                }
                if (rayDirY < 0)
                {
                    stepY = -1;
                    sideDistY = (cam.pos.Y - mapY) * deltaDistY;
                }
                else
                {
                    stepY = 1;
                    sideDistY = (mapY + 1.0 - cam.pos.Y) * deltaDistY;
                }
                // Loop to find where the ray hits a wall
                while (!hit && sideDistX < MAX_DIST && sideDistY < MAX_DIST)
                {
                    // Jump to next square
                    if (sideDistX < sideDistY)
                    {
                        sideDistX += deltaDistX;
                        mapX += stepX;
                        side = 0;
                    }
                    else
                    {
                        sideDistY += deltaDistY;
                        mapY += stepY;
                        side = 1;
                    }
                    // Check if ray has hit a wall
                    // System.out.println(mapX + ", " + mapY + ", " +
                    // map[mapX][mapY]);
                    try
                    {
                        if (map[mapX, mapY] > 0)
                            hit = true;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        Console.WriteLine(e.Message);
                        hit = true;
                    }
                }
                // Calculate distance to the point of impact
                if (hit)
                {
                    if (side == 0)
                        perpWallDist = Math.Abs((mapX - cam.pos.X + (1 - stepX) / 2)
                                / rayDirX);
                    else
                        perpWallDist = Math.Abs((mapY - cam.pos.Y + (1 - stepY) / 2)
                                / rayDirY);
                    // Now calculate the height of the wall based on the distance from
                    // the camera
                    //int lineHeight;
                    if (perpWallDist > 0)
                        lineHeight = (int)(h / perpWallDist);
                    else
                        lineHeight = h;
                    // calculate lowest and highest pixel to fill in current stripe
                    drawStart = -lineHeight / 2 + h / 2;
                    if (drawStart < 0)
                        drawStart = 0;
                    drawEnd = lineHeight / 2 + h / 2;
                    if (drawEnd >= h)
                        drawEnd = h - 1;
                    // add a texture
                    texNum = map[mapX, mapY] - 1;
                    //double wallX;// Exact position of where wall was hit
                    if (side == 1)
                    {// If its a y-axis wall
                        wallX = (cam.pos.X + ((mapY - cam.pos.Y + (1 - stepY) / 2) / rayDirY)
                                * rayDirX);
                    }
                    else
                    {// X-axis wall
                        wallX = (cam.pos.Y + ((mapX - cam.pos.X + (1 - stepX) / 2) / rayDirX)
                                * rayDirY);
                    }
                    wallX -= Math.Floor(wallX);
                    // x coordinate on the texture
                    texX = (int)(wallX * (textures[texNum].Width));
                    if (side == 0 && rayDirX > 0)
                        texX = textures[texNum].Width - texX - 1;
                    else if (side == 1 && rayDirY < 0)
                        texX = textures[texNum].Width - texX - 1;
                    // calculate y coordinate on texture
                    for (int y = drawStart; y < drawEnd; y++)
                    {
                        texY = (((y * 2 - h + lineHeight) << 6) / lineHeight) / 2;
                        Color color;

                        color = texPix[texNum][texX
                                + (texY * textures[texNum].Width)];

                        pixels[x + y * (w)] = color;

                    }
                }
            }
        }
    }
}
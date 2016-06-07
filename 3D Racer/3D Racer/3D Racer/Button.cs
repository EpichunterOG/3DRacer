using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Racer
{
    class Button
    {
        private String loc, text;
        private int width, height;
        private Texture2D blank;
        private SpriteFont font;
        private MouseState mouse;
        public MouseState Mouse
        {
            get { return mouse; }
            set { mouse = value; }
        }
        public bool play { get; set; }
        public Rectangle bounds { get; set; }
        public Button(String loc, String text, int width, int height, Texture2D blank, SpriteFont font) 
        {
            this.loc = loc;
            this.text = text;
            this.width = width;
            this.height = height;
            this.blank = blank;
            this.font = font;
            this.play = false;
        }
        public void drawButton(SpriteBatch spriteBatch) 
        {
            int centerX = width / 2;
            int centerY = height / 2;
            var size = this.size();
            Vector2 ms = new Vector2(mouse.X, mouse.Y);
            switch (loc.ToLower())
            {
                case "center":
                    bounds = new Rectangle(centerX - (size.Item1 / 2), centerY - (size.Item2 / 2), size.Item1, size.Item2);
                    if (ms.X < bounds.Right && ms.X > bounds.Left && ms.Y > bounds.Top && ms.Y < bounds.Bottom)
                    {
                        spriteBatch.Draw(blank, bounds, Color.Blue);
                        if (mouse.LeftButton == ButtonState.Pressed)
                        {
                            play = true;
                        }
                        
                    }
                    spriteBatch.DrawString(font, text, 
                        new Vector2(centerX - (size.Item1 / 2), centerY - (size.Item2 / 2)), Color.Red);
                    break;
                case "left":
                    break;
                case "right":
                    break;
                case "bottom_right":
                    break;
                case "bottom_left":
                    break;
                case "top_right":
                    break;
                case "top_left":
                    break;
            }
        }
        public Tuple<int, int> size() 
        {
            var measured = font.MeasureString(text);
            var size = new Tuple<int, int>((int)measured.X, (int)measured.Y);
            return size;
        }
    }
}

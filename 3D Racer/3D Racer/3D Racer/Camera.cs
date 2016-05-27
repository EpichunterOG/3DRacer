using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3D_Racer

{
    class Camera
    {
        public const float maxSpeed=20f;
        public const float breakConst = 2f;
        public Vector2 pos;
        public float speed;
        public Vector2 dir;
        public Vector2 plane;
        public float acceleration { get; set; }
        public float rotation { get; set; }
        float rAccr = 1f;
        private float dt;
        KeyboardState kb;
        MouseState ms, oldms;

        public Camera(Vector2 pos, Vector2 direction, Vector2 plane, float acceleration, float rotation)
        {
            this.pos = pos;
            this.dir = direction;
            this.plane = plane;
            this.acceleration = acceleration;
            this.rotation = rotation;
            speed = 0f;

        }
        public void update(int[,] map, GameTime time)
        {
            dt = time.ElapsedGameTime.Milliseconds / 1000f;

            kb = Keyboard.GetState();
            ms = Mouse.GetState();
            #region keyboard

            if (kb.IsKeyDown(Keys.D))
            {
                float oldxDir = dir.X;
                dir.X = (float)(dir.X * Math.Cos(-rotation * dt * rAccr) - dir.Y * Math.Sin(-rotation * dt * rAccr));
                dir.Y = (float)(oldxDir * Math.Sin(-rotation * dt * rAccr) + dir.Y * Math.Cos(-rotation * dt * rAccr));
                float oldxPlane = plane.X;
                plane.X = (float)(plane.X * Math.Cos(-rotation * dt * rAccr) - plane.Y * Math.Sin(-rotation * dt * rAccr));
                plane.Y = (float)(oldxPlane * Math.Sin(-rotation * dt * rAccr) + plane.Y * Math.Cos(-rotation * dt * rAccr));
            }
            if (kb.IsKeyDown(Keys.A))
            {

                float oldxDir = dir.X;
                dir.X = (float)(dir.X * Math.Cos(rotation * dt * rAccr) - dir.Y * Math.Sin(rotation * dt * rAccr));
                dir.Y = (float)(oldxDir * Math.Sin(rotation * dt * rAccr) + dir.Y * Math.Cos(rotation * dt * rAccr));
                float oldxPlane = plane.X;
                plane.X = (float)(plane.X * Math.Cos(rotation * dt * rAccr) - plane.Y * Math.Sin(rotation * dt * rAccr));
                plane.Y = (float)(oldxPlane * Math.Sin(rotation * dt * rAccr) + plane.Y * Math.Cos(rotation * dt * rAccr));

            }

            /*
            if (kb.IsKeyDown(Keys.W))
            {
                if (map[(int)(pos.X + dir.X * acceleration * dt), (int)pos.Y] == 0)
                {
                    pos.X += dir.X * acceleration * dt;
                }
                if (map[(int)(pos.X), (int)(pos.Y + dir.Y * acceleration * dt)] == 0)
                {
                    pos.Y += dir.Y * acceleration * dt;
                }
            }
             * */
            if (kb.IsKeyDown(Keys.W))
            {
                if (speed <= maxSpeed)

                    speed += acceleration * dt;
                
                
            }
            if(kb.IsKeyDown(Keys.L))
                rAccr=2.5f;
            else
                rAccr=1f;
            /*
            if (kb.IsKeyDown(Keys.S))
            {
                if (map[(int)(pos.X - dir.X * acceleration * dt), (int)pos.Y] == 0)
                {
                    pos.X -= dir.X * acceleration * dt;
                }
                if (map[(int)(pos.X), (int)(pos.Y - dir.Y * acceleration * dt)] == 0)
                {
                    pos.Y -= dir.Y * acceleration * dt;
                }
            }*/
            if (kb.IsKeyDown(Keys.S))
            {
                if (speed - ( breakConst * dt) >= 0f)
                    speed -= breakConst * dt;
                else if (speed - ( breakConst * dt) < 0f)
                    speed = 0f;

            }
            int mapNumX = 0;
            int mapNumY = 0;
            //This code is outside of the IF statement to test for IndexOutOfRangeExceptions that would otherwise crash the program.

            //These two are separate so that actions can be taken depending on which coordinate is throwing the IndexOutOfRangeException.
            try
            {
                 mapNumX = map[(int)(pos.X + dir.X*speed * dt), (int)pos.Y];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                mapNumX = 1;
            }
            try
            {
                mapNumY = map[(int)(pos.X), (int)(pos.Y + dir.Y*speed * dt)];
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                mapNumY = 1;
            }
            if (mapNumX <= 0)
            {
                pos.X += dir.X*speed*dt;
            }
            else
            {
                
            }
            if (mapNumY <= 0)
            {
                pos.Y += dir.Y*speed*dt;
            }

           
            #endregion
            #region mouse
            /*
            if (ms.X != oldms.X)
            {
                float dx = oldms.X - ms.X;

                float oldxDir = dir.X;
                dir.X = (float)(dir.X * Math.Cos(rotation * dx * dt) - dir.Y * Math.Sin(rotation * dx * dt));
                dir.Y = (float)(oldxDir * Math.Sin(rotation * dx * dt) + dir.Y * Math.Cos(rotation * dx * dt));
                float oldxPlane = plane.X;
                plane.X = (float)(plane.X * Math.Cos(rotation * dx * dt) - plane.Y * Math.Sin(rotation * dx * dt));
                plane.Y = (float)(oldxPlane * Math.Sin(rotation * dx * dt) + plane.Y * Math.Cos(rotation * dx * dt));

            }
            */
            oldms = ms;



           


            #endregion
        }

    }
}

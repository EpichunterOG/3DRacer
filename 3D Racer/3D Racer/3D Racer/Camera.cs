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

        public Vector2 pos;
        public Vector2 speed;
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
            speed = new Vector2(0f, 0f);

        }
        public void update(int[,] map, GameTime time)
        {
            dt = time.ElapsedGameTime.Milliseconds / 1000f;

            kb = Keyboard.GetState();
            ms = Mouse.GetState();
            #region keyboard
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
                
                    speed.X += dir.X * acceleration * dt;
                
               
                    speed.Y += dir.Y * acceleration * dt;
                
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

            if (map[(int)(pos.X + speed.X), (int)pos.Y] == 0)
            {
                pos.X += speed.X;
            }
            else
            {
                
            }
            if (map[(int)(pos.X), (int)(pos.Y + speed.Y)] == 0)
            {
                pos.Y += speed.Y;
            }

            if (kb.IsKeyDown(Keys.D))
            {
                float oldxDir = dir.X;
                dir.X = (float)(dir.X * Math.Cos(-rotation * dt*rAccr) - dir.Y * Math.Sin(-rotation * dt*rAccr));
                dir.Y = (float)(oldxDir * Math.Sin(-rotation * dt*rAccr) + dir.Y * Math.Cos(-rotation * dt*rAccr));
                float oldxPlane = plane.X;
                plane.X = (float)(plane.X * Math.Cos(-rotation * dt*rAccr) - plane.Y * Math.Sin(-rotation * dt*rAccr));
                plane.Y = (float)(oldxPlane * Math.Sin(-rotation * dt*rAccr) + plane.Y * Math.Cos(-rotation * dt*rAccr));
            }
            if (kb.IsKeyDown(Keys.A))
            {

                float oldxDir = dir.X;
                dir.X = (float)(dir.X * Math.Cos(rotation * dt*rAccr) - dir.Y * Math.Sin(rotation * dt*rAccr));
                dir.Y = (float)(oldxDir * Math.Sin(rotation * dt*rAccr) + dir.Y * Math.Cos(rotation * dt*rAccr));
                float oldxPlane = plane.X;
                plane.X = (float)(plane.X * Math.Cos(rotation * dt*rAccr) - plane.Y * Math.Sin(rotation * dt*rAccr));
                plane.Y = (float)(oldxPlane * Math.Sin(rotation * dt*rAccr) + plane.Y * Math.Cos(rotation * dt*rAccr));

            }
            #endregion
            #region mouse
            
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

            oldms = ms;



           


            #endregion
        }

    }
}

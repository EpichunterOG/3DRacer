using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3D_Racer
{
    class Title
    {
        private Texture2D bg, blank;
        private int width, height;
        private SpriteFont font;
        private MouseState mouse;

        public MouseState Mouse
        {
            get { return mouse; }
            set { mouse = value; }
        }
        public bool play { get; set; }
        public Title(Texture2D bg, Texture2D blank, SpriteFont font, int width, int height)
        {
            this.bg = bg;
            this.blank = blank;
            this.font = font;
            this.width = width;
            this.height = height;
        }
        public void DrawTitle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bg, new Rectangle(0, 0, width, height), Color.White);
            spriteBatch.DrawString(font, (new Vector2(mouse.X, mouse.Y)).ToString(), new Vector2(3, 40), Color.White);
            Button button = new Button("center", "text", width, height, blank, font);
            button.Mouse = mouse;
            button.drawButton(spriteBatch);
            this.play = button.play ? true : false;
        }
    }
}

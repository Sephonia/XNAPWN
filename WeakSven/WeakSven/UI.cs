using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    class UI
    {
        public bool drawn = true;

        public delegate void OnClick(UI sender);
        public event OnClick clicked;

        public Rectangle rect = Rectangle.Empty;
        public bool hovering = false;
        private bool startedClick = false;

        protected Texture2D debugSquare;
        protected SpriteFont font;

        public UI(SpriteFont fonte, Texture2D square)
        {
            font = fonte;
            debugSquare = square;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (rect.Contains(Mouse.GetState().X, Mouse.GetState().Y))
            {
                hovering = true;

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                    startedClick = true;

                if (Mouse.GetState().LeftButton == ButtonState.Released && startedClick)
                {
                    if (clicked != null)
                    {
                        clicked(this);
                        drawn = false;
                    }
                }
            }
            else
                hovering = false;

            if (Mouse.GetState().LeftButton == ButtonState.Released)
                startedClick = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(drawn)
                spriteBatch.Draw(debugSquare, rect, Color.White);
        }
    }
}

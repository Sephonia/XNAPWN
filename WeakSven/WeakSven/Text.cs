using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    class Text : UI
    {
        public delegate void OnTextChange(Text sender);
        public event OnTextChange textChanged;

        public delegate void OnRightCLick(Text sender);
        public event OnRightCLick rightClick;

        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                if (label != value)
                {
                    if (textChanged != null)
                        textChanged(this);
                }

                label = value;

                rect.Width = label.Length * 11;
                rect.Height = 24;
            }
        }

        private bool clicked = false;

        public Text(SpriteFont fonte, Texture2D square)
            : base(fonte, square) { }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                if (!clicked)
                {
                    if (rightClick != null)
                    {
                        rightClick(this);
                        drawn = false;
                    }
                }

                clicked = true;
                drawn = false;
            }
            else
                clicked = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if(drawn)
                spriteBatch.DrawString(font, Label, new Vector2(rect.X, rect.Y), Color.Black);
        }
    }
}

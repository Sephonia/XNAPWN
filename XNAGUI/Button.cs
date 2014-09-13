using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XNAGUI
{
    public class Button2 : Component
    {
        private Vector2 textPosition = Vector2.Zero;
        private string text = string.Empty;
        public string Text
        {
            get { return text; }
            set
            {
                text = value;

                Vector2 widthHeight = UIManager.font.MeasureString(text);

                textPosition = new Vector2(
                    Rect.X + ((Rect.Width / 2) - (widthHeight.X / 2)),
                    Rect.Y + ((Rect.Height / 2) - (widthHeight.Y / 2))
                    );
            }
        }
        public Button2(Rectangle whereItsAt) : base(whereItsAt) { }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UIManager.square, Rect, Color.White);
            spriteBatch.DrawString(UIManager.font, Text, textPosition, Color.Black);
        }


    }
}

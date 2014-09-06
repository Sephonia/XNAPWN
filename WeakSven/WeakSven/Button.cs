using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
    class Button : UI
    {
        public string Label { get; set; }

        public Button(SpriteFont fonte, Texture2D square, Rectangle size)
            : base(fonte, square)
        {
            rect = size;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (drawn)
            {
                if (!hovering)
                    base.Draw(spriteBatch);
                else
                {
                    spriteBatch.Draw(debugSquare, rect, Color.Crimson);
                }
                spriteBatch.DrawString(font, Label, new Vector2(
                    rect.X + (rect.Width * 0.5f) - (Label.Length + 40),
                    rect.Y + (rect.Height * 0.5f) - 7), Color.Black);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
    class Tex
    {
        private Texture2D img = null;
        public Rectangle Rect { get; private set; }

        public Tex(Texture2D texture, int x, int y)
        {
            img = texture;
            Rect = new Rectangle(x, y, 64, 64);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(img, Rect, Color.White);
        }
    }
}

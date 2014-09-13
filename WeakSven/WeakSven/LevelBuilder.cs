using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    class PaintObject
    {
        public int x, y;
        public char index;
    }

    class LevelBuilder
    {
        public MouseState previousMouse;

        int saveCount = 1;

        public Dictionary<char, Texture2D> Textures { get; private set; }

        List<PaintObject> paintings = new List<PaintObject>();

        char texIndex = '0';

        int gridX = 0, gridY = 0;

        public LevelBuilder()
        {
            Textures = new Dictionary<char, Texture2D>();
        }

        public void LoadTextures(ContentManager Content)
        {
            Textures.Add('b', Content.Load<Texture2D>("Characters/Block"));
            Textures.Add('g', Content.Load<Texture2D>("Characters/Grass"));
        }

        public void Update(GameTime gameTime, KeyboardState previousKeyboard)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.B))
                texIndex = 'b';
            else if (Keyboard.GetState().IsKeyDown(Keys.G))
                texIndex = 'g';
            else
                texIndex = '0';

            gridX = (Mouse.GetState().X / 25) * 25;
            gridY = (Mouse.GetState().Y / 25) * 25;

            // Checks to make sure we have clicked
            if (texIndex != '0' &&
                Mouse.GetState().LeftButton == ButtonState.Pressed &&
                previousMouse.LeftButton == ButtonState.Released)
            {
                paintings.Add(
                    new PaintObject() { x = gridX, y = gridY, index = texIndex }
                    );
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) &&
                previousKeyboard.IsKeyUp(Keys.S))
            {
                Save(saveCount);
                saveCount++;
            }

            previousMouse = Mouse.GetState();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (PaintObject p in paintings)
            {
                spriteBatch.Draw(
                    Textures[p.index],
                    new Rectangle(p.x, p.y, 25, 25),
                    Color.White
                    );
            }

            if (texIndex != '0')
            {
                spriteBatch.Draw(
                    Textures[texIndex],
                    new Rectangle(gridX, gridY, 25, 25),
                    Color.White
                    );
            }
        }

        public void Save(int sCount)
        {
            string data = string.Empty;

            string name;

            name = "level" + sCount.ToString();

            int maxX = 0, maxY = 0;
            foreach (PaintObject p in paintings)
            {
                if (p.x > maxX)
                    maxX = p.x;
                if (p.y > maxY)
                    maxY = p.y;
            }

            maxX = (maxX / 25) + 1;
            maxY = (maxY / 25) + 1;

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    data += "0";
                }

                data += "\n";
            }

            int tmpIndex = 0;
            foreach (PaintObject p in paintings)
            {
                int x = p.x / 25;
                int y = p.y / 25;

                tmpIndex = (y * maxX) + x + y;
                data = data.Remove(tmpIndex, 1).Insert(tmpIndex, p.index.ToString());
            }

            File.WriteAllText(name + ".txt", data);
        }
    }
}

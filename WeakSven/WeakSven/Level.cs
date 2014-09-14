using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
    class Level
    {
        public List<Tex> Texes { get; private set; }
        public Dictionary<char, Texture2D> Textures { get; private set; }
        public bool pCollides = false, eCollides = false;

        public int CurrentLevel { get; private set; }

        public Level()
        {
            Texes = new List<Tex>();
            Textures = new Dictionary<char, Texture2D>();
        }

        public void LoadTextures(ContentManager Content)
        {
            Textures.Add('b', Content.Load<Texture2D>("Characters/Block"));
            Textures.Add('g', Content.Load<Texture2D>("Characters/Grass"));
        }

        private void Unload()
        {
            Texes.Clear();
        }

        public void Next()
        {
            Load(CurrentLevel + 1);
        }

        public string GetLevelFile(int level)
        {
            return "Content/Level" + level + ".txt";
        }

        public void Load(int level)
        {
            if (!File.Exists(GetLevelFile(level)))
                level = 1;

            CurrentLevel = level;

            Unload();

            int y = 0;
            foreach (string line in File.ReadLines(GetLevelFile(level)))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (Textures.ContainsKey(line[i]))
                        Texes.Add(new Tex(Textures[line[i]], i * 25, y));
                }

                y += 25;
            }
        }

        public void Update(Enemy enem, GameTime gameTime)
        {
            foreach (Tex b in Texes)
            {
                if (Player.Instance.rect.Intersects(b.Rect))
                    Player.Instance.MoveBack();

                if (enem.rect.Intersects(b.Rect))
                    enem.Velocity = Vector2.Zero;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tex b in Texes)
                b.Draw(spriteBatch);
        }
    }
}

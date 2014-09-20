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
        public bool pCollides = false, eCollides = false, bCollides = false;

        public int CurrentLevel { get; private set; }

        public Level()
        {
            Texes = new List<Tex>();
            Textures = new Dictionary<char, Texture2D>();
        }

        public void LoadTextures(ContentManager Content)
        {
            Textures.Add('a', Content.Load<Texture2D>("Graphics/glass"));
            Textures.Add('b', Content.Load<Texture2D>("Characters/Block"));
            Textures.Add('c', Content.Load<Texture2D>("Graphics/brick"));
            Textures.Add('d', Content.Load<Texture2D>("Graphics/door"));
            Textures.Add('e', Content.Load<Texture2D>("Graphics/earth"));
            Textures.Add('f', Content.Load<Texture2D>("Graphics/roof"));
            Textures.Add('g', Content.Load<Texture2D>("Characters/Grass"));
            Textures.Add('h', Content.Load<Texture2D>("Graphics/leaves"));
            Textures.Add('i', Content.Load<Texture2D>("Graphics/metalH"));
            Textures.Add('j', Content.Load<Texture2D>("Graphics/metalV"));


            Textures.Add('l', Content.Load<Texture2D>("Graphics/lava"));
            Textures.Add('m', Content.Load<Texture2D>("Graphics/swamp"));
            Textures.Add('n', Content.Load<Texture2D>("Graphics/stone"));
            Textures.Add('o', Content.Load<Texture2D>("Graphics/window"));
            Textures.Add('p', Content.Load<Texture2D>("Graphics/torch"));

            Textures.Add('r', Content.Load<Texture2D>("Graphics/rock"));

            Textures.Add('u', Content.Load<Texture2D>("Graphics/woodH"));
            Textures.Add('v', Content.Load<Texture2D>("Graphics/woodV"));
            Textures.Add('w', Content.Load<Texture2D>("Graphics/water"));
            Textures.Add('y', Content.Load<Texture2D>("Graphics/riverB"));
            Textures.Add('z', Content.Load<Texture2D>("Graphics/riverT"));


            //for the buildings use the numlock to create the parts of the the house


            Textures.Add('/', Content.Load<Texture2D>("Home/castle1"));
            Textures.Add('*', Content.Load<Texture2D>("Home/castle2"));
            Textures.Add('-', Content.Load<Texture2D>("Home/castle3"));
            Textures.Add('+', Content.Load<Texture2D>("Home/castle4"));
            Textures.Add('.', Content.Load<Texture2D>("Home/castle5"));
            Textures.Add('=', Content.Load<Texture2D>("Home/castle6"));
            Textures.Add('_', Content.Load<Texture2D>("Home/castle7"));
            Textures.Add(';', Content.Load<Texture2D>("Home/caslte8"));
            Textures.Add('`', Content.Load<Texture2D>("Home/castle9"));

            Textures.Add('1', Content.Load<Texture2D>("Home/home1"));
            Textures.Add('2', Content.Load<Texture2D>("Home/home2"));
            Textures.Add('3', Content.Load<Texture2D>("Home/home3"));
            Textures.Add('4', Content.Load<Texture2D>("Home/home4"));
            Textures.Add('5', Content.Load<Texture2D>("Home/home5"));
            Textures.Add('6', Content.Load<Texture2D>("Home/home6"));
            Textures.Add('7', Content.Load<Texture2D>("Home/home7"));
            Textures.Add('8', Content.Load<Texture2D>("Home/home8"));
            Textures.Add('9', Content.Load<Texture2D>("Home/home9"));
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
            return "Content/Level/Level" + level + ".txt";
        }

        public void Load(int level)
        {
            if (!File.Exists(GetLevelFile(level)))
                level = 1;

            CurrentLevel = level;

            

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
                {
                    //b.Rect, Player.Instance.Position
                    // b
                    //Content/Characters/block

                    if (b.Equals("Characters/block"))
                        Player.Instance.MoveBack();
                }

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

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
        #region Variables
        public List<Tex> Texes { get; private set; }
        public List<Tex> collide { get; private set; }
        public List<Tex> slown { get; private set; }
        public List<Tex> slowAndDamage { get; private set; }

        public Dictionary<char, Texture2D> Textures { get; private set; }
        public Dictionary<char, Texture2D> collideTexture { get; private set; }
        public Dictionary<char, Texture2D> slowTexture { get; private set; }
        public Dictionary<char, Texture2D> slowDamageTexture { get; private set; }


        protected bool slowedDown = false;
        protected bool sloDamage = false;
        protected double slowTime = 5.0f;

        public Rectangle collideRect = new Rectangle();
        public Rectangle slowingRect = new Rectangle();
        public Rectangle sloAndDamRect = new Rectangle();
        #endregion

        public int CurrentLevel { get; private set; }

        public Level()
        {
            Texes = new List<Tex>();
            collide = new List<Tex>();
            slown = new List<Tex>();
            Textures = new Dictionary<char, Texture2D>();
            collideTexture = new Dictionary<char, Texture2D>();
            slowTexture = new Dictionary<char, Texture2D>();

        }

        public void LoadTextures(ContentManager Content)
        {
            //textures the player can ont walk through
            collideTexture.Add('a', Content.Load<Texture2D>("Graphics/glass"));
            collideTexture.Add('b', Content.Load<Texture2D>("Characters/Block"));
            collideTexture.Add('f', Content.Load<Texture2D>("Graphics/roof"));
            collideTexture.Add('o', Content.Load<Texture2D>("Graphics/window"));
            collideTexture.Add('w', Content.Load<Texture2D>("Graphics/water"));

            //slow and damage
            collideTexture.Add('l', Content.Load<Texture2D>("Graphics/lava"));

            //slow the character slows down
            collideTexture.Add('y', Content.Load<Texture2D>("Graphics/riverB"));
            collideTexture.Add('z', Content.Load<Texture2D>("Graphics/riverT"));
            collideTexture.Add('m', Content.Load<Texture2D>("Graphics/swamp"));

            //regular textures
            Textures.Add('c', Content.Load<Texture2D>("Graphics/brick"));
            Textures.Add('d', Content.Load<Texture2D>("Graphics/door"));
            Textures.Add('e', Content.Load<Texture2D>("Graphics/earth"));
            Textures.Add('g', Content.Load<Texture2D>("Characters/Grass"));
            Textures.Add('h', Content.Load<Texture2D>("Graphics/leaves"));
            Textures.Add('i', Content.Load<Texture2D>("Graphics/metalH"));
            Textures.Add('j', Content.Load<Texture2D>("Graphics/metalV"));
            Textures.Add('n', Content.Load<Texture2D>("Graphics/stone"));
            Textures.Add('p', Content.Load<Texture2D>("Graphics/torch"));
            Textures.Add('r', Content.Load<Texture2D>("Graphics/rock"));
            Textures.Add('u', Content.Load<Texture2D>("Graphics/woodH"));
            Textures.Add('v', Content.Load<Texture2D>("Graphics/woodV"));



            //for the buildings use the numlock to create the parts of the the house

            Textures.Add('*', Content.Load<Texture2D>("Home/castle2"));

            collideTexture.Add('/', Content.Load<Texture2D>("Home/castle1"));
            collideTexture.Add('-', Content.Load<Texture2D>("Home/castle3"));
            collideTexture.Add('+', Content.Load<Texture2D>("Home/castle4"));
            collideTexture.Add('.', Content.Load<Texture2D>("Home/castle5"));
            collideTexture.Add('=', Content.Load<Texture2D>("Home/castle6"));
            collideTexture.Add('_', Content.Load<Texture2D>("Home/castle7"));
            collideTexture.Add(';', Content.Load<Texture2D>("Home/caslte8"));
            collideTexture.Add('`', Content.Load<Texture2D>("Home/castle9"));

            Textures.Add('2', Content.Load<Texture2D>("Home/home2"));

            collideTexture.Add('1', Content.Load<Texture2D>("Home/home1"));
            collideTexture.Add('3', Content.Load<Texture2D>("Home/home3"));
            collideTexture.Add('4', Content.Load<Texture2D>("Home/home4"));
            collideTexture.Add('5', Content.Load<Texture2D>("Home/home5"));
            collideTexture.Add('6', Content.Load<Texture2D>("Home/home6"));
            collideTexture.Add('7', Content.Load<Texture2D>("Home/home7"));
            collideTexture.Add('8', Content.Load<Texture2D>("Home/home8"));
            collideTexture.Add('9', Content.Load<Texture2D>("Home/home9"));
        }

        protected virtual void Unload()
        {
            Texes.Clear();
            collide.Clear();
            slown.Clear();
            collideTexture.Clear();

            //CurrentLevel.Unload();
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
                        Texes.Add(new Tex(Textures[line[i]], i * 64, y));
                    else if(collideTexture.ContainsKey(line[i]))
                    {
                        if (collideTexture.ContainsKey(line[i]))
                            collide.Add(new Tex(collideTexture[line[i]], i * 64, y));
                        
                        else if (slowTexture.ContainsKey(line[i]))
                        {
                            if (slowTexture.ContainsKey(line[i]))
                                slown.Add(new Tex(slowTexture[line[i]], i * 64, y));
                        }
                    }
                }

                y += 64;
            }
        }

        public void Update(Enemy enem, GameTime gameTime)
        {
            foreach (Tex co in collide)
            {
                if (Player.Instance.rect.Intersects(co.Rect))
                    Player.Instance.MoveBack();


                if (enem.rect.Intersects(collideRect))
                    enem.Velocity = Vector2.Zero; 
            }
            foreach (Tex slo in slown)
            {
                if (Player.Instance.rect.Intersects(slo.Rect))
                {
                    Player.Instance.Velocity = new Vector2((Player.Instance.rect.X * 0.5f), (Player.Instance.rect.Y * 0.5f));
                    slowedDown = true;
                }
            }

            #region Slow and Damage
            //un comment once the slow and damage to the player is acheieved
            //foreach (Tex sloDam in slowAndDamage)
            //{
            //    if (Player.Instance.rect.Intersects(sloDam.Rect))
            //    {
            //        //Player.Instance.speed -= 3;
            //        Player.Instance.Velocity = new Vector2((Player.Instance.rect.X - 1.0f), (Player.Instance.rect.Y * 1.0f));
            //        Player.Instance.Health -= 7;
            //        sloDamage = true;
            //    }
            //}
            #endregion

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Tex b in Texes)
                b.Draw(spriteBatch);

            foreach (Tex c in collide)
                c.Draw(spriteBatch);

            foreach (Tex s in slown)
                s.Draw(spriteBatch);

            //foreach (Tex snd in slowAndDamage)
            //    snd.Draw(spriteBatch);

            //TODO: Populate and draw CollideTextures from tex list to be collided from
        }
    }
}

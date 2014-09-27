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

        int telXN, telYN, telXB, telYB;
        Vector2 prev = Vector2.Zero;


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
            collideTexture.Add('k', Content.Load<Texture2D>("Graphics/firePit"));
            collideTexture.Add('q', Content.Load<Texture2D>("Graphics/gPit"));

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
            Textures.Add('t', Content.Load<Texture2D>("Graphics/arrow"));
            Textures.Add('~', Content.Load<Texture2D>("Graphics/arrow2"));




            //for the buildings use the numlock to create the parts of the the house

            Textures.Add('*', Content.Load<Texture2D>("Home/gh2"));

            collideTexture.Add('/', Content.Load<Texture2D>("Home/gh1"));
            collideTexture.Add('-', Content.Load<Texture2D>("Home/gh3"));
            collideTexture.Add('+', Content.Load<Texture2D>("Home/gh4"));
            collideTexture.Add('.', Content.Load<Texture2D>("Home/gh5"));
            collideTexture.Add('=', Content.Load<Texture2D>("Home/gh6"));
            collideTexture.Add('_', Content.Load<Texture2D>("Home/gh7"));
            collideTexture.Add(';', Content.Load<Texture2D>("Home/gh8"));
            collideTexture.Add('`', Content.Load<Texture2D>("Home/gh9"));

            Textures.Add('@', Content.Load<Texture2D>("Home/wh2"));

            collideTexture.Add('!', Content.Load<Texture2D>("Home/wh1"));
            collideTexture.Add('#', Content.Load<Texture2D>("Home/wh3"));
            collideTexture.Add('$', Content.Load<Texture2D>("Home/wh4"));
            collideTexture.Add('%', Content.Load<Texture2D>("Home/wh5"));
            collideTexture.Add('^', Content.Load<Texture2D>("Home/wh6"));
            collideTexture.Add('&', Content.Load<Texture2D>("Home/wh7"));
            collideTexture.Add(':', Content.Load<Texture2D>("Home/wh8"));
            collideTexture.Add('"', Content.Load<Texture2D>("Home/wh9"));

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

        protected void Unload()
        {
            Texes.Clear();
            collide.Clear();
            slown.Clear();
        }

        public void Next()
        {
            Unload();
            CurrentLevel += 1;
            Load(CurrentLevel);
        }

        public void Previous()
        {
            Unload();
            CurrentLevel -= 1;
            Load(CurrentLevel);
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

                    else if (collideTexture.ContainsKey(line[i]))
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

            #region Teleport
            if (CurrentLevel == 1)
            {
                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = Game1.windowWidth - Game1.BIT_SIZE_PL;
            }
            else if (CurrentLevel == 2)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, Game1.windowWidth - Game1.BIT_SIZE_PL);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = 1024;

                telXB = 0;
                telYB = 6 * 64;
            }
            else if (CurrentLevel == 3)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, 1024);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = 384;

                telXB = 0;
                telYB = 7 * 64;
            }
            else if (CurrentLevel == 4)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, 384);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = 128;

                telXB = 0;
                telYB = 640;
            }
            else if (CurrentLevel == 5)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, 128);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = 960;

                telXB = 0;
                telYB = 16 * 64;
            }
            else if (CurrentLevel == 6)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, 960);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = 448;

                telXB = 0;
                telYB = 4 * 64;
            }
            else if (CurrentLevel == 7)
            {
                prev = new Vector2(1280 - Game1.BIT_SIZE_PL, 448);

                telXN = 1280 - Game1.BIT_SIZE_PL;
                telYN = Game1.windowWidth - Game1.BIT_SIZE_PL;

                telXB = 0;
                telYB = 640;
            }

            if (Player.Instance.Position == new Vector2(telXN, telYN))
            {
                Next();
                Player.Instance.Position = new Vector2(50, 300);
            }
            if(Player.Instance.Position == new Vector2(telXB, telYB))
            {
                if (CurrentLevel != 1)
                {
                    Previous();
                    Player.Instance.Position = new Vector2(prev.X - 64, prev.Y - 64);
                }
            }
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

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

        public char texIndex = '0';

        int gridX = 0, gridY = 0;

        public LevelBuilder()
        {
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


            Textures.Add('k', Content.Load<Texture2D>("Graphics/firePit"));
            Textures.Add('l', Content.Load<Texture2D>("Graphics/lava"));
            Textures.Add('m', Content.Load<Texture2D>("Graphics/swamp"));
            Textures.Add('n', Content.Load<Texture2D>("Graphics/stone"));
            Textures.Add('o', Content.Load<Texture2D>("Graphics/window"));
            Textures.Add('p', Content.Load<Texture2D>("Graphics/torch"));
            Textures.Add('q', Content.Load<Texture2D>("Graphics/gPit"));
            Textures.Add('r', Content.Load<Texture2D>("Graphics/rock"));

            Textures.Add('u', Content.Load<Texture2D>("Graphics/woodH"));
            Textures.Add('v', Content.Load<Texture2D>("Graphics/woodV"));
            Textures.Add('w', Content.Load<Texture2D>("Graphics/water"));
            Textures.Add('y', Content.Load<Texture2D>("Graphics/riverB"));
            Textures.Add('z', Content.Load<Texture2D>("Graphics/riverT"));


            //for the buildings use the numlock to create the parts of the the house


            Textures.Add('/', Content.Load<Texture2D>("Home/gh1"));
            Textures.Add('*', Content.Load<Texture2D>("Home/gh2"));
            Textures.Add('-', Content.Load<Texture2D>("Home/gh3"));
            Textures.Add('+', Content.Load<Texture2D>("Home/gh4"));
            Textures.Add('.', Content.Load<Texture2D>("Home/gh5"));
            Textures.Add('=', Content.Load<Texture2D>("Home/gh6"));
            Textures.Add('_', Content.Load<Texture2D>("Home/gh7"));
            Textures.Add(';', Content.Load<Texture2D>("Home/gh8"));
            Textures.Add('`', Content.Load<Texture2D>("Home/gh9"));

            Textures.Add('!', Content.Load<Texture2D>("Home/wh1"));
            Textures.Add('@', Content.Load<Texture2D>("Home/wh2"));
            Textures.Add('#', Content.Load<Texture2D>("Home/wh3"));
            Textures.Add('$', Content.Load<Texture2D>("Home/wh4"));
            Textures.Add('%', Content.Load<Texture2D>("Home/wh5"));
            Textures.Add('^', Content.Load<Texture2D>("Home/wh6"));
            Textures.Add('&', Content.Load<Texture2D>("Home/wh7"));
            Textures.Add(':', Content.Load<Texture2D>("Home/wh8"));
            Textures.Add('"', Content.Load<Texture2D>("Home/wh9"));

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

        public void Update(GameTime gameTime, KeyboardState previousKeyboard)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                texIndex = 'a';
            else if (Keyboard.GetState().IsKeyDown(Keys.B))
                texIndex = 'b';
            else if (Keyboard.GetState().IsKeyDown(Keys.C))
                texIndex = 'c';
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                texIndex = 'd';
            else if (Keyboard.GetState().IsKeyDown(Keys.E))
                texIndex = 'e';
            else if (Keyboard.GetState().IsKeyDown(Keys.F))
                texIndex = 'f';
            else if (Keyboard.GetState().IsKeyDown(Keys.G))
                texIndex = 'g';
            else if (Keyboard.GetState().IsKeyDown(Keys.H))
                texIndex = 'h';
            else if (Keyboard.GetState().IsKeyDown(Keys.I))
                texIndex = 'i';
            else if (Keyboard.GetState().IsKeyDown(Keys.J))
                texIndex = 'j';
            else if (Keyboard.GetState().IsKeyDown(Keys.K))
                texIndex = 'k';
            else if (Keyboard.GetState().IsKeyDown(Keys.L))
                texIndex = 'l';
            else if (Keyboard.GetState().IsKeyDown(Keys.M))
                texIndex = 'm';
            else if (Keyboard.GetState().IsKeyDown(Keys.N))
                texIndex = 'n';
            else if (Keyboard.GetState().IsKeyDown(Keys.O))
                texIndex = 'o';
            else if (Keyboard.GetState().IsKeyDown(Keys.P))
                texIndex = 'p';
            else if (Keyboard.GetState().IsKeyDown(Keys.Q))
                texIndex = 'q';
            else if (Keyboard.GetState().IsKeyDown(Keys.R))
                texIndex = 'r';
            else if (Keyboard.GetState().IsKeyDown(Keys.U))
                texIndex = 'u';
            else if (Keyboard.GetState().IsKeyDown(Keys.V))
                texIndex = 'v';
            else if (Keyboard.GetState().IsKeyDown(Keys.W))
                texIndex = 'w';
            else if (Keyboard.GetState().IsKeyDown(Keys.Y))
                texIndex = 'y';
            else if (Keyboard.GetState().IsKeyDown(Keys.Z))
                texIndex = 'z';

                //for the buildings
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                texIndex = '1';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                texIndex = '2';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad3))
                texIndex = '3';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                texIndex = '4';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad5))
                texIndex = '5';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                texIndex = '6';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad7))
                texIndex = '7';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                texIndex = '8';
            else if (Keyboard.GetState().IsKeyDown(Keys.NumPad9))
                texIndex = '9';

            else if (Keyboard.GetState().IsKeyDown(Keys.PrintScreen))
                texIndex = '/';
            else if (Keyboard.GetState().IsKeyDown(Keys.Scroll))
                texIndex = '*';
            else if (Keyboard.GetState().IsKeyDown(Keys.Pause))
                texIndex = '-';
            else if (Keyboard.GetState().IsKeyDown(Keys.Insert))
                texIndex = '+';
            else if (Keyboard.GetState().IsKeyDown(Keys.Home))
                texIndex = '.';
            else if (Keyboard.GetState().IsKeyDown(Keys.PageUp))
                texIndex = '=';
            else if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                texIndex = '_';
            else if (Keyboard.GetState().IsKeyDown(Keys.End))
                texIndex = ';';
            else if (Keyboard.GetState().IsKeyDown(Keys.PageDown))
                texIndex = '`';

            else if (Keyboard.GetState().IsKeyDown(Keys.F1))
                texIndex = '!';
            else if (Keyboard.GetState().IsKeyDown(Keys.F2))
                texIndex = '@';
            else if (Keyboard.GetState().IsKeyDown(Keys.F3))
                texIndex = '#';
            else if (Keyboard.GetState().IsKeyDown(Keys.F4))
                texIndex = '$';
            else if (Keyboard.GetState().IsKeyDown(Keys.F5))
                texIndex = '%';
            else if (Keyboard.GetState().IsKeyDown(Keys.F6))
                texIndex = '^';
            else if (Keyboard.GetState().IsKeyDown(Keys.F7))
                texIndex = '&';
            else if (Keyboard.GetState().IsKeyDown(Keys.F8))
                texIndex = ':';
            else if (Keyboard.GetState().IsKeyDown(Keys.F9))
                texIndex = '"'; 
            else
                texIndex = '0';


            gridX = (Mouse.GetState().X / 64) * 64;
            gridY = (Mouse.GetState().Y / 64) * 64;

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
                    new Rectangle(p.x, p.y, 64, 64),
                    Color.White
                    );
            }

            if (texIndex != '0')
            {
                spriteBatch.Draw(
                    Textures[texIndex],
                    new Rectangle(gridX, gridY, 64, 64),
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

            maxX = (maxX / 64) + 1;
            maxY = (maxY / 64) + 1;

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
                int x = p.x / 64;
                int y = p.y / 64;

                tmpIndex = (y * maxX) + x + y;
                data = data.Remove(tmpIndex, 1).Insert(tmpIndex, p.index.ToString());
            }

            File.WriteAllText(name + ".txt", data);
        }
    }
}

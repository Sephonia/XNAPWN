using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace WeakSven
{
    // sealed
    class Player : InteractiveCharacter
    {
        #region Singleton Stuff
        private static Player instance = null;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }


        private Player() : base() { Speed = 2.00f; }
        #endregion

        //public AudioSFX bing = new AudioSFX();
       
        public void SetName(string name) { Name = name; }

        protected int health = 100;
        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0)
                    health = 0;
            }
        }
        
        public KeyboardState lastKey;
        private bool movingX = false;
        private bool movingY = false;

        public override void Load(ContentManager Content, string imageFile)
        {
            base.Load(Content, imageFile);

        }

        public override void Update(GameTime gameTime)
        {
            movingX = movingY = false;

            // TODO:  Change player controls to fit your game
           

            if (Keyboard.GetState().IsKeyDown(Keys.W) ||
                Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                Velocity.Y = -Speed;
                movingY = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A) ||
                Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                Velocity.X = -Speed;
                movingX = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S) ||
                Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                Velocity.Y = Speed;
                movingY = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) ||
                Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                Velocity.X = Speed;
                movingX = true;
            }
           
            if (!movingX)
                Velocity.X  = 0;

            if (!movingY)
                Velocity.Y = 0;

            lastKey = Keyboard.GetState();
            base.Update(gameTime);
        }
    }

}
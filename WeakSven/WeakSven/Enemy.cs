using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Enemy : InteractiveCharacter
	{

        Random rnd = new Random();
        private int randDir;
        int num;

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

        public Enemy()
            : base() {
                Speed = 0.05f;
        }

        public void RandDir(GameTime gameTime)
        {
            num = rnd.Next(100);
                //Right
                if (num < 25)
                    Velocity.X = Speed;
                //Left
                if (num >= 25 && num < 50)
                    Velocity.X = -Speed;
                //Down
                if (num >= 50 && num < 75)
                    Velocity.Y = Speed;
                //Up
                if (num >= 75)
                    Velocity.X = -Speed;
        }

        
        public override void Load(ContentManager Content, string imageFile)
        {
            num = rnd.Next(100);
            base.Load(Content, imageFile); 
        }

		public override void Update(GameTime gameTime)
		{
            if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)
                RandDir(gameTime);
            else
                Velocity = Vector2.Zero;
            // TODO:  AI here 

            
			base.Update(gameTime);
		}

        
       
	}
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Enemy : InteractiveCharacter
	{
        //For Test Purposes ONLY!
       /* private static Enemy instance = null;
        public static Enemy Instance
        {
            get
            {
                if (instance == null)
                    instance = new Enemy();

                return instance;
            }
        }*/
        //For Test Purposes ONLY!
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
                CharSpeed = new Vector2(X_SPEED, Y_SPEED);
        }

        public void RandDir(GameTime gameTime)
        {
            num = rnd.Next(100);
                //Right
                if (num < 25)
                    Velocity.X = CharSpeed.X;
                //Left
                if (num >= 25 && num < 50)
                    Velocity.X = -CharSpeed.X;
                //Down
                if (num >= 50 && num < 75)
                    Velocity.Y = CharSpeed.Y;
                //Up
                if (num >= 75)
                    Velocity.X = -CharSpeed.Y;
        }

        
        public override void Load(ContentManager Content, string imageFile)
        {
            num = rnd.Next(100);
            base.Load(Content, imageFile); 
        }

		public override void Update(GameTime gameTime)
		{
            if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)

                //if (Player.Instance.Position != Enemy.Instance.Position) For Test Purposes ONLY!
            RandDir(gameTime);
            else
                Velocity = Vector2.Zero;
            // TODO:  AI here 

            
			base.Update(gameTime);
		}

        
       
	}
}
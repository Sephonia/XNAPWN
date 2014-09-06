using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Enemy : InteractiveCharacter
	{

       
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

        public override void Load(ContentManager Content, string imageFile)
        {
            base.Load(Content, imageFile);
            
            //bing.Sound = Content.Load<SoundEffect>("Audio/SFX/bing");
        }

		public override void Update(GameTime gameTime)
		{
            if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)
                Velocity.X = Speed * 20;

            else
                Velocity = Vector2.Zero;
			// TODO:  AI here
           
			base.Update(gameTime);
		}

        
       
	}
}
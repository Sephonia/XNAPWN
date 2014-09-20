using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Enemy : InteractiveCharacter
	{

        Random rnd = new Random();
        private int num;            // variable to hold our random number.
        protected bool ranCheck = false; // a boolean to see if we've grabbed a random number


        protected int health = 100;  // a protected health variable for enemy characters defaulted to 100
        public int Health        
        {
            get { return health; }  // capital Health setter that we can modify to change the values of lowercase health without directly causing unwanted access to health.
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
            if (ranCheck == false)              //since it starts false and we have no value, run the next block
            {
                num = rnd.Next(100);            // here we're setting our variable num to the new random number in a range 0-100 (random.Next() only operates in non-negative random values between a floor and ceiling default 0-1 that can also be specified, as we've done here (100))
                ranCheck = true;                // setting our check to true so we no longer set new random numbers to our num variable.
            }                                   // if else chain allows us to evaluate down the line for conditions in a linear range, E.X. below.
            if (num < 25)                       // anything below 25, this evalutes and everything else is ignored.
                Velocity.X = Speed * 20;
            else if (num < 50)                  // anything above 25 and below 50 will evaluate this, again, everything else is ignored.
                Velocity.X = -Speed * 20;
            else if (num < 75)                  // anything above 50 and below 75 will evalute this, do you see a pattern?
                Velocity.Y = Speed * 20;
            else                                // anything greater than 75 isn't handled before, so we can easily state that any value not handled (in our non-negative range 0-100) is always "else" (since the only unhandeled values are 75+)
                Velocity.Y = -Speed * 20;
        }

        
        public override void Load(ContentManager Content, string imageFile)
        {
            num = rnd.Next(100);
            base.Load(Content, imageFile); 
        }

		public override void Update(GameTime gameTime)
		{
            if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)  // every 3 seconds
                RandDir(gameTime);                                    // run our RanDir function to grab a value and assign a velocity.
            else
            {
                Velocity = Vector2.Zero;
                ranCheck = false;                // set the check back to false if it is not the 3rd second so the next time the RanDir function is called, it will grab a new random and assign it to num.
            }
            

            
			base.Update(gameTime);
		}

        
       
	}
}
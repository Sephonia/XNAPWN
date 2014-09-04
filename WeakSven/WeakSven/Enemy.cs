using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Enemy : InteractiveCharacter
	{
        int Attack = 5;
        int Defense = 5;

        //SpriteFont font;
        //Texture2D text;
        //Vector2 position = Vector2.Zero;

        Texture2D face = null;
        Rectangle rectangle;
        Vector2 place = new Vector2(300, 150);

        float velocity = 5.0f;

        //bool attacked = false;

		public Enemy()
			: base()
		{

		}

        public void Load(ContentManager Content, string imageName)
        {
            //GraphicsDevice graphicsDevice, SpriteFont fonts
            //text = new Texture2D(graphicsDevice, 1, 1);
            //text.SetData(new Color[] { Color.White });

            //font = fonts;

            face = Content.Load<Texture2D>(imageName);
            rectangle.Width = face.Width;
            rectangle.Height = face.Height;

        }
		public override void Update(GameTime gameTime)
		{
            rectangle.X = (int)place.X;
            rectangle.Y = (int)place.Y;

			// TODO:  AI here
           
			base.Update(gameTime);
		}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(face, rectangle, Color.White);
        }
        
       
	}
}
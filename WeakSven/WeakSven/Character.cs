using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
	class Character : Entity
	{
		public Animation animation = new Animation();

		public Rectangle rect = new Rectangle(0, 0, 0, 0);

        public Texture2D img = null;

		public Vector2 Position { get; set; }
		public Vector2 Velocity = Vector2.Zero;
		public float Speed { get; protected set; }

        
        public Character() : base() { Speed = 0.75f; }
        public Character(string name) : base(name) { Speed = 0.75f; }


        public virtual void Load(ContentManager Content, string imageFile)
        {
            animation.FrameCountX = 4;
            animation.FrameCountY = 4;
            animation.FramesPerSec = 33;

            animation.SpriteSheet = Content.Load<Texture2D>(imageFile);

            rect.X = (int)Position.X;
            rect.Y = (int)Position.Y;
            rect.Width = animation.FrameWidth;
            rect.Height = animation.FrameHeight;
        }

		public virtual void Update(GameTime gameTime)
		{
			Position += Velocity;

			rect.X = (int)Position.X;
			rect.Y = (int)Position.Y;

			if (Velocity == Vector2.Zero)
			{
				animation.Frame = 1;
				animation.Paused = true;
			}
			else
				animation.Paused = false;

			animation.Update(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			animation.Draw(spriteBatch, Position);
		}
	}
}
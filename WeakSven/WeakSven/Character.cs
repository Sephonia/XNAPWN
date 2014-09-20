using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    class Character : Entity
    {


        public Rectangle rect = new Rectangle(0, 0, 0, 0);

        public Texture2D img = null;

        protected Vector2 previousPos = Vector2.Zero;

        public Animation animation = new Animation();       
        
        public Vector2 Position { get; set; }
        public Vector2 Velocity = Vector2.Zero;
        protected float Speed { get; set; }




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
            previousPos = Position;
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

        public void MoveBack()
        {
            Position = previousPos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, Position);
        }
    }
}

//enemy.cs
//	public override void Update(GameTime gameTime)
//{
//    if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)
//      Velocity.X = CharSpeed.X * 20;

//player.cs
//public override void Update(GameTime gameTime)
//        {
//            // if (((int)gameTime.TotalGameTime.TotalSeconds) % 3 == 0)
//           //	bing.Play(gameTime);
//
//            // TODO:  Change player controls to fit your game
//
//            if (Keyboard.GetState().IsKeyDown(Keys.W) ||
//                Keyboard.GetState().IsKeyDown(Keys.Up))
//            {
//                Velocity.Y = -CharSpeed.X;
//            }
//            else if (Keyboard.GetState().IsKeyDown(Keys.A) ||
//                Keyboard.GetState().IsKeyDown(Keys.Left))
//           {
//                Velocity.X = -CharSpeed.X;
//            }
//            else if (Keyboard.GetState().IsKeyDown(Keys.S) ||
//                Keyboard.GetState().IsKeyDown(Keys.Down))
//            {
//                Velocity.Y = CharSpeed.Y;
//            }
//            else if (Keyboard.GetState().IsKeyDown(Keys.D) ||
//                Keyboard.GetState().IsKeyDown(Keys.Right))
//            {
//                Velocity.X = CharSpeed.Y;
//            }
//            else
//                Velocity = Vector2.Zero;
//           base.Update(gameTime);
//       }
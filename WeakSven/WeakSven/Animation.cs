using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WeakSven
{
	class Animation
	{
		public int FrameWidth { get; private set; }
		public int FrameHeight { get; private set; }

		private Texture2D spriteSheet = null;
		public Texture2D SpriteSheet
		{
			get { return spriteSheet; }
			set
			{
				spriteSheet = value;

				FrameWidth = value.Width / FrameCountX;
				FrameHeight = value.Height / FrameCountY;
			}
		}

		private const float Rotation = 0;
		private const float Scale = 1.0f;
		private const float Depth = 0.0f;
		public int FrameCountX { get; set; }
		public int FrameCountY { get; set; }
		private float TimePerFrame { get; set; }
		private int framesPerSecond = 33;
		public int FramesPerSec
		{
			get { return framesPerSecond; }
			set
			{
				TimePerFrame = 1.0f / value;
				framesPerSecond = value;
			}
		}

        public int sequenceStart = 0;
        public int sequenceEnd = 0;

		public int Frame { get; set; }
		public bool Paused { get; set; }
		public float TotalElapsed { get; set; }

		// class AnimatedTexture
		public void Update(GameTime gameTime)
		{
			float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (Paused)
				return;

			TotalElapsed += elapsed;
			if (TotalElapsed > TimePerFrame)
			{
				Frame++;
				// Keep the Frame between 0 and the total frames, minus one.

                if(sequenceStart == sequenceEnd && sequenceStart == 0 )
				    Frame = Frame % (FrameCountX * FrameCountY);
                else
                    Frame = Frame % (sequenceEnd - sequenceStart);

    			TotalElapsed -= TimePerFrame;
			}
		}

		// class AnimatedTexture
		public void Draw(SpriteBatch batch, Vector2 screenPos)
		{
			Draw(batch, Frame, screenPos);
		}

		public void Draw(SpriteBatch spriteBatch, int frame, Vector2 screenPos)
		{
			int xFrame = frame % FrameCountX;
			int yFrame = frame / FrameCountY;

			Rectangle sourcerect = new Rectangle(FrameWidth * xFrame, FrameHeight * yFrame,
				FrameWidth, FrameHeight);

			spriteBatch.Draw(spriteSheet, screenPos, sourcerect, Color.White,
				Rotation, Vector2.Zero, Scale, SpriteEffects.None, Depth);
		}
	}
}
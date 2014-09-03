using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Enemy face = new Enemy();
        Player sauce = new Player();
        Rectangle rect;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

			// Comment the following if you don't want to see the mouse
			IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");

			Player.Instance.Load(Content, "Characters/Player");
            face.Load(Content, "face");
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

            //if(sauce.rect.Intersects(face.Rect))
            //{
                
            //}

			Player.Instance.Update(gameTime);

            face.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();
            
            face.Draw(spriteBatch);
            Player.Instance.Draw(spriteBatch);
            
            
            spriteBatch.DrawString(font, "Health: " + sauce.GetHealth.ToString(), new Vector2(10, 10), Color.Yellow);
			

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

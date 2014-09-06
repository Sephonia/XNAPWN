using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //MainMenu menu;

        Enemy monster = new Enemy();        
        Circle playerHitBox = new Circle(0,0,64.0f);
        Circle monsterHitBox = new Circle(0,0,64.0f);

        Rectangle bg;
        Texture2D bgPic;
        public int bgSpeed = 5;
        public Vector2 velo = Vector2.Zero; //for the background

        int windowWidth;
        int windowHeight;

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

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            

            font = Content.Load<SpriteFont>("font");
            //menu = new MainMenu(font, windowWidth, windowHeight);

			Player.Instance.Load(Content, "Characters/Player");
            monster.Load(Content, "Enemy/Monster");

            bgPic = Content.Load<Texture2D>("BG_Art/rest");

        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

            
            bg.X = bgPic.Width;
            bg.Y = bgPic.Height;

            //if (sauce.Velocity.X > 0)
            //    bgPic.Width = bgSpeed;


			Player.Instance.Update(gameTime);
            
            monster.Update(gameTime);

            playerHitBox.center.X = (Player.Instance.rect.Width / 2);
            playerHitBox.center.Y = (Player.Instance.rect.Height / 2);

            monsterHitBox.center.X = (monster.rect.Width / 2);
            monsterHitBox.center.Y = (monster.rect.Height / 2);

            if (Player.Instance.rect.Intersects(monster.rect))
            {
                Player.Instance.Health += 5;                
            }
            if (playerHitBox.Intersects(monsterHitBox))
            {
                Player.Instance.Health -= 1;
            }
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

            //menu.Draw(spriteBatch);

            spriteBatch.Draw(bgPic, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

            monster.Draw(spriteBatch);
            Player.Instance.Draw(spriteBatch);


            spriteBatch.DrawString(font, "Player Hp: " + Player.Instance.Health.ToString(), new Vector2(10, 10), Color.Yellow);
            spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640,10),Color.Yellow);
			

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

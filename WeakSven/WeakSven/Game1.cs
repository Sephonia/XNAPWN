using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Enemy monster = new Enemy();
        Player sauce = new Player();
        //Circle playerHitBox = new Circle(0,0,64.0f);
        //Circle monsterHitBox = new Circle(0,0,64.0f);

        Rectangle bg;
        Texture2D bgPic;
        public int bgSpeed = 5;
        public Vector2 velo = Vector2.Zero; //for the background

        int windowWidth;
        int windowHeight;

        Texture2D titleBox;
        Button playButton;
        Text text;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            IsMouseVisible = true;

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");

            titleBox = new Texture2D(GraphicsDevice, 1, 1);
            titleBox.SetData(new Color[] { Color.White });

            playButton = new Button(font, titleBox, new Rectangle(50, 50, 150, 75));
            playButton.Label = "PLAY";

            text = new Text(font, titleBox);
            text.Label = "FINAL HOTSAUCE QUEST-RIM";

            playButton.clicked += playButton_clicked; 

            font = Content.Load<SpriteFont>("font");

			Player.Instance.Load(Content, "Characters/Player");
            monster.Load(Content, "Enemy/Monster");

            bgPic = Content.Load<Texture2D>("rest");

        }

        void playButton_clicked(UI sender)
        {
            ((Button)sender).Label = "LOADING...";

            playButton.clicked -= playButton_clicked;
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				this.Exit();

            playButton.Update(gameTime);
            text.Update(gameTime);
            bg.X = bgPic.Width;
            bg.Y = bgPic.Height;

            //if (sauce.Velocity.X > 0)
            //    bgPic.Width = bgSpeed;

			Player.Instance.Update(gameTime);
            
            monster.Update(gameTime);

            //playerHitBox.center.X = (sauce.rect.Width / 2);
            //playerHitBox.center.Y = (sauce.rect.Height / 2);

            //monsterHitBox.center.X = (monster.rect.Width / 2);
            //monsterHitBox.center.Y = (monster.rect.Height / 2);

            if (sauce.rect.Intersects(monster.rect))
            {
                sauce.GetHealth += 30;                
            }
            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

            spriteBatch.Draw(bgPic, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

            if (playButton.drawn)
            {
                playButton.Draw(spriteBatch);
                text.Draw(spriteBatch);
            }

            if (!playButton.drawn)
            {
                spriteBatch.DrawString(font, "Player Hp: " + sauce.GetHealth.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640, 10), Color.Black);
                monster.Draw(spriteBatch);
                Player.Instance.Draw(spriteBatch);
            }

			spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

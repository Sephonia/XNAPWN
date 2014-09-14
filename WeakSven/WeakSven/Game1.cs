using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNAGUI;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Button2 button = new Button2(new Rectangle(0,0,200,50));

        Level level1 = new Level();
        LevelBuilder builder = new LevelBuilder();

        private bool builderMode = false;

        Enemy monster = new Enemy();        
        Circle playerHitBox = new Circle(0,0,64.0f);
        Circle monsterHitBox = new Circle(0,0,64.0f);

        Rectangle whirl = new Rectangle(0, 0, 128, 128);
        Rectangle whirl2 = new Rectangle(0, 0, 128, 128);
        Texture2D circTex;

        KeyboardState previousKeyboard;

        Rectangle bg;
        Texture2D bgPic;

        Texture2D levelBG;


        public int bgSpeed = 5;
        public Vector2 velo = Vector2.Zero; //for the background

        Rectangle slideBar = new Rectangle(0, 50, 50, 300);
        Texture2D statSheet;
        bool isSliding = false;
        float sideSpeed = 2.0f;

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
            graphics.IsFullScreen = false;

            // if you don't want full screen play with these values.
            graphics.PreferredBackBufferHeight = 1200;
            graphics.PreferredBackBufferWidth = 1200;

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
        }

        protected override void Initialize()
        {
            base.Initialize();

            //level has started
            Combat.Instance.AddCombatant(monster);
            
          
		// Comment the following if you don't want to see the mouse
		IsMouseVisible = true;

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;
        }

        protected override void LoadContent()
        {

            
            circTex = Content.Load<Texture2D>("Test/circle");

            UIManager.Init(GraphicsDevice,Content.Load<SpriteFont>("Font"));
            button.Text = "Click Me!";

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

            bgPic = Content.Load<Texture2D>("BG_Art/rest");
            levelBG = Content.Load<Texture2D>("BG_Art/bg3");
            
            statSheet = Content.Load<Texture2D>("stat");

            builder.LoadTextures(Content);

            statSheet = Content.Load<Texture2D>("stat");

            level1.LoadTextures(Content);
            level1.Load(1);

            button.onClick += button_onClick;
        }

        void button_onClick(Component sender)
        {
            ((Button2)sender).Text = "Clicked!";
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

            whirl.X = (Player.Instance.rect.X - 32);
            whirl.Y = (Player.Instance.rect.Y - 32);

            whirl2.X = (monster.rect.X - 32);
            whirl2.Y = (monster.rect.Y - 32);

            bg.X = levelBG.Width;
            bg.Y = levelBG.Height;

            Player.Instance.Update(gameTime);
            bg.X = levelBG.Width;
            bg.Y = levelBG.Height;

            monster.Update(gameTime);


            playerHitBox.center.X = (Player.Instance.rect.Width / 2);
            playerHitBox.center.Y = (Player.Instance.rect.Height / 2);

            monsterHitBox.center.X = (monster.rect.Width / 2);
            monsterHitBox.center.Y = (monster.rect.Height / 2);


            if (builderMode)
                builder.Update(gameTime, previousKeyboard);




            if (!playButton.drawn)
            {
                Combat.Instance.Update(gameTime);

                if (!playButton.drawn && playerHitBox.Intersects(monsterHitBox))


            if (builderMode)
                builder.Update(gameTime, previousKeyboard);



            if (!playButton.drawn && playerHitBox.Intersects(monsterHitBox))
            {

                if (Player.Instance.rect.Intersects(monster.rect))
                {

                    if (Player.Instance.rect.Intersects(monster.rect))
                    {
                        Player.Instance.Health += 5;
                    }
                    if (playerHitBox.Intersects(monsterHitBox))
                    {
                        Player.Instance.Health -= 1;
                    }

                }

                previousKeyboard = Keyboard.GetState();

                if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                    (Keyboard.GetState().IsKeyUp(Keys.P)))
                {
                    isSliding = true;
                    sideSpeed++;
                }
            }

            previousKeyboard = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.P) ||
                (Keyboard.GetState().IsKeyUp(Keys.P)))
            {
                isSliding = true;
                sideSpeed++;
            }

                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin();

            spriteBatch.Draw(bgPic, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);

            if (playButton.drawn && builderMode == false)
            {
                playButton.Draw(spriteBatch);
                text.Draw(spriteBatch);
            }
		
            if (!playButton.drawn && builderMode == false)
            {

                spriteBatch.Draw(circTex, whirl, Color.White);
                spriteBatch.Draw(circTex, whirl2, Color.White);

                spriteBatch.Draw(levelBG, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
                level1.Draw(spriteBatch);


                spriteBatch.Draw(levelBG, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
                level1.Draw(spriteBatch);

                spriteBatch.DrawString(font, "Player Hp: " + Player.Instance.Health.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640, 10), Color.Yellow);
                monster.Draw(spriteBatch);
                Player.Instance.Draw(spriteBatch);
                
            }

            if(builderMode == true)
                builder.Draw(spriteBatch);

            if (Keyboard.GetState().IsKeyDown(Keys.P))
                spriteBatch.Draw(statSheet, slideBar, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

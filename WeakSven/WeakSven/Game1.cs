using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level level1 = new Level();
        LevelBuilder builder = new LevelBuilder();

        private bool builderMode = false;

        Enemy monster = new Enemy();        
        Circle playerHitBox = new Circle(0,0,64.0f);
        Circle monsterHitBox = new Circle(0,0,64.0f);

        KeyboardState previousKeyboard;

        Rectangle bg;
        Texture2D bgPic;
        public int bgSpeed = 5;
        public Vector2 velo = Vector2.Zero; //for the background

        int windowWidth;
        int windowHeight;

        Texture2D titleBox;
        Button playButton;
        Text text;

        Hover Hero;
        Rectangle playName;


        #region Hover and Slide
        //MouseState mouseState;
        //bool isHovering = false;

        //Texture2D nameBox; //player's name 
        //Texture2D badBox;  //enemy's name

        //Text name;
        //Text badName;

        //Rectangle playName = new Rectangle(30, 0, 20, 300); // the box that would show up on hover
        //Rectangle enemyName = new Rectangle(0, 0, 10, 300);// the box that would show up on hover

        //bool isSliding = false; // for the rectangle with all player stats to slide from the edge of the screen
        //Rectangle slideBar = new Rectangle(100, 0, 20, 300);
        //Texture2D slid;
        //float velocitySlide = 2.0f;

        
        #endregion


        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
<<<<<<< HEAD
            graphics.IsFullScreen = false;
=======
           // graphics.IsFullScreen = true;
>>>>>>> fe91478aa5e4290952677bf6cd2b462d8d4aa69d

            // if you don't want full screen play with these values.
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
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

<<<<<<< HEAD
            builder.LoadTextures(Content);

            level1.LoadTextures(Content);
            level1.Load(1);
=======
            Hero = new Hover(playName, font, "Player");
>>>>>>> fe91478aa5e4290952677bf6cd2b462d8d4aa69d

            #region hovering

            //nameBox = new Texture2D(GraphicsDevice, 1, 1);
            //nameBox.SetData(new Color[] { Color.White });
            
            //badBox = new Texture2D(GraphicsDevice, 1, 1);
            //badBox.SetData(new Color[] { Color.White });
            
            //name = new Text(font, nameBox);
            //name.Label = "Player";

            //badName = new Text(font, badBox);
            //badName.Label = "Enemy";
            #endregion

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

            //for hovering
            //name.Update(gameTime);
            //badName.Update(gameTime);

            bg.X = bgPic.Width;
            bg.Y = bgPic.Height;


			Player.Instance.Update(gameTime);
            
            monster.Update(gameTime);
            Hero.Update(gameTime);

            playerHitBox.center.X = (Player.Instance.rect.Width / 2);
            playerHitBox.center.Y = (Player.Instance.rect.Height / 2);

            monsterHitBox.center.X = (monster.rect.Width / 2);
            monsterHitBox.center.Y = (monster.rect.Height / 2);
            
            #region hovering
            //for hovering
            //playName.Width = nameBox.Width;
            //playName.Height = nameBox.Height;

            //enemyName.Width = badBox.Width;
            //enemyName.Height = badBox.Height;

            //if (Keyboard.GetState().IsKeyDown(Keys.P))
            //{
            //    isSliding = true;
            //}

            
            // HOVERING
            //mouseState = Mouse.GetState();

            //if (mouseState.X == Player.Instance.rect.X && 
            //    mouseState.Y == Player.Instance.rect.Y)
            //    isHovering = true;

            //else
            //    isHovering = false;
            #endregion

            if (builderMode)
                builder.Update(gameTime, previousKeyboard);



            if (!playButton.drawn && playerHitBox.Intersects(monsterHitBox))
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
            

            base.Update(gameTime);
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
                level1.Draw(spriteBatch);

                spriteBatch.DrawString(font, "Player Hp: " + Player.Instance.Health.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640, 10), Color.Yellow);
                monster.Draw(spriteBatch);
                Player.Instance.Draw(spriteBatch);
            }

<<<<<<< HEAD
            if(builderMode == true)
                builder.Draw(spriteBatch);
=======
            Hero.Draw(spriteBatch);

>>>>>>> fe91478aa5e4290952677bf6cd2b462d8d4aa69d

            #region hovering 
            //if (isHovering)
            //{
            //    spriteBatch.DrawString(font, "Player", new Vector2(300, 50),  Color.Pink);
            //    spriteBatch.DrawString(font, "Enemy", new Vector2(300, 50), Color.Pink);

            //    //spriteBatch.Draw(nameBox, playName, Color.LightBlue);
            //    //spriteBatch.Draw(badBox, enemyName, Color.Red);


            //}
#endregion

            #region sliding
            //if (isSliding)
            //{
            //    velocitySlide++; 
            //    spriteBatch.Draw(slid, slideBar, Color.Pink);
            //}
            #endregion
            
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

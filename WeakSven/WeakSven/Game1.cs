using System;
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

        Button2 button = new Button2(new Rectangle(0, 0, 200, 50));

        Level level1 = new Level();
        LevelBuilder builder = new LevelBuilder();
//        public bool bCollides = false;

        private bool builderMode = false;

        Enemy monster = new Enemy();
        Circle playerHitBox = new Circle(0, 0, 64.0f);
        Circle monsterHitBox = new Circle(0, 0, 64.0f);

        Rectangle whirl = new Rectangle(0, 0, 128, 128);
        Rectangle whirl2 = new Rectangle(0, 0, 128, 128);
        Texture2D circTex;

        KeyboardState previousKeyboard;

        Texture2D bgPic;

        Texture2D levelBG;


        public int bgSpeed = 5;
        public Vector2 velo = Vector2.Zero; //for the background


        //Rectangle slideBar = new Rectangle(0, 50, 50, 300);
        //Texture2D statSheet;
        bool isSliding = false;
        float sideSpeed = 2.0f;

        Rectangle slideBar = new Rectangle(0, 50, 50, 300);      

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
        }

        protected override void Initialize()
        {
            base.Initialize();

            //level has started
            Combat.Instance.AddCombatant(monster);            
            IsMouseVisible = true;

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;
        }

        protected override void LoadContent()
        {
            circTex = Content.Load<Texture2D>("Test/circle");

            UIManager.Init(GraphicsDevice, Content.Load<SpriteFont>("Font"));
            button.Text = "Click Me!";

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");

            titleBox = new Texture2D(GraphicsDevice, 1, 1);
            titleBox.SetData(new Color[] { Color.White });

            playButton = new Button(font, titleBox, new Rectangle(170, 970, 300, 125));
            playButton.Label = "PLAY";
            playButton.clicked += playButton_clicked;

            font = Content.Load<SpriteFont>("font");

            Player.Instance.Load(Content, "Characters/Player");
            Player.Instance.Position = new Vector2(100, 100);

            monster.Load(Content, "Enemy/pepper");


           // monster.Load(Content, "Enemy/Monster");


            bgPic = Content.Load<Texture2D>("BG_Art/bg4");
            levelBG = Content.Load<Texture2D>("BG_Art/bg3");

            builder.LoadTextures(Content);

            //statSheet = Content.Load<Texture2D>("BG_Art/stat");


            level1.LoadTextures(Content);

            level1.Load(6);
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

            //text.Update(gameTime);

            //bg.X = bgPic.Width;
            //bg.Y = bgPic.Height;



            whirl.X = (Player.Instance.rect.X - 32);
            whirl.Y = (Player.Instance.rect.Y - 32);

            whirl2.X = (monster.rect.X - 32);
            whirl2.Y = (monster.rect.Y - 32);

            Player.Instance.Update(gameTime);

            

            playerHitBox.center.X = (Player.Instance.rect.Width / 2);
            playerHitBox.center.Y = (Player.Instance.rect.Height / 2);

            monsterHitBox.center.X = (monster.rect.Width / 2);
            monsterHitBox.center.Y = (monster.rect.Height / 2);

            if (builderMode)
                builder.Update(gameTime, previousKeyboard);

            if (!playButton.drawn)
            {
                level1.Update(monster, gameTime);
                Player.Instance.Update(gameTime);
                Combat.Instance.Update(gameTime);
                monster.Update(gameTime);

                level1.Update(monster, gameTime);

                if (builderMode)
                    builder.Update(gameTime, previousKeyboard);

                previousKeyboard = Keyboard.GetState();

                
            }

            

            previousKeyboard = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (playButton.drawn && builderMode == false)
            {
                spriteBatch.Draw(bgPic, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
                playButton.Draw(spriteBatch);
            }

            if (!playButton.drawn && builderMode == false)

            {               
                spriteBatch.Draw(levelBG, new Rectangle(0, 0, windowWidth, windowHeight), Color.Black);
                level1.Draw(spriteBatch);

                
                spriteBatch.Draw(circTex, whirl, Color.White);
                spriteBatch.Draw(circTex, whirl2, Color.White); 
                
                


                spriteBatch.DrawString(font, "Player Hp: " + Player.Instance.Health.ToString(), new Vector2(10, 10), Color.Yellow);
                spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640, 10), Color.Yellow);

                monster.Draw(spriteBatch);
                Player.Instance.Draw(spriteBatch);
                spriteBatch.Draw(circTex, whirl, Color.White);
                spriteBatch.Draw(circTex, whirl2, Color.White);
     
            }

            if (builderMode == true)
                builder.Draw(spriteBatch);


           // if (Keyboard.GetState().IsKeyDown(Keys.P))
           //     spriteBatch.Draw(statSheet, slideBar, Color.White);


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

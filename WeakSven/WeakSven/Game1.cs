using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XNAGUI;

namespace WeakSven
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region Variables
        public const int BIT_SIZE = 32;
        public static int BIT_SIZE_PL = 64;


        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Button2 button = new Button2(new Rectangle(0, 0, 200, 50));

        Level level = new Level();

        LevelBuilder builder = new LevelBuilder();

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

        public static int windowWidth;
        public static int windowHeight;

        Texture2D titleBox;
        Button playButton;

        Text text;
        Rectangle playName;
        SpriteFont font;

        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;

            // if you don't want full screen play with these values.
            graphics.PreferredBackBufferHeight = 1280;
            graphics.PreferredBackBufferWidth = 1280;
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

            playButton = new Button(font, titleBox, new Rectangle(170, 1040, 345, 130));
            playButton.Label = "PLAY";
            playButton.clicked += playButton_clicked;

            font = Content.Load<SpriteFont>("font");

            Player.Instance.Load(Content, "Characters/Player");

            Player.Instance.Position = new Vector2(50, 300);

            monster.Load(Content, "Enemy/mooRight");

            monster.Position = new Vector2(400, 110);

            bgPic = Content.Load<Texture2D>("BG_Art/bg4");
            levelBG = Content.Load<Texture2D>("BG_Art/bg3");

            builder.LoadTextures(Content);
            level.LoadTextures(Content);

            level.Load(1);

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

        protected override void UnloadContent()
        { }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            playButton.Update(gameTime);


            whirl.X = (Player.Instance.rect.X - BIT_SIZE);
            whirl.Y = (Player.Instance.rect.Y - BIT_SIZE);

            whirl2.X = (monster.rect.X - BIT_SIZE);
            whirl2.Y = (monster.rect.Y - BIT_SIZE);


            playerHitBox.center.X = (Player.Instance.rect.Width / 2);
            playerHitBox.center.Y = (Player.Instance.rect.Height / 2);

            monsterHitBox.center.X = (monster.rect.Width / 2);
            monsterHitBox.center.Y = (monster.rect.Height / 2);

            if (builderMode)
                builder.Update(gameTime, previousKeyboard);

            if (!playButton.drawn)
            {
                Player.Instance.Update(gameTime);
                Combat.Instance.Update(gameTime);
                monster.Update(gameTime);

                level.Update(monster, gameTime);
                
                if (builderMode)
                    builder.Update(gameTime, previousKeyboard);

                previousKeyboard = Keyboard.GetState();


            }

            previousKeyboard = Keyboard.GetState();
            #region Binding characters to screen bounds
            if (Player.Instance.rect.X + Player.Instance.rect.Width > windowWidth)
            {
                //TODO: To go to the next level uncomment but the new layer will mimic the old layer in regards to collsion
                // if(Player.Instance.Position == new Vector2(1275, windowWidth))
                // {
                //     level2.Load(6);
                //     Player.Instance.Position = new Vector2(110, 350);
                Player.Instance.MoveBack();
                // }
            }

            if (Player.Instance.rect.X < 0)
                Player.Instance.MoveBack();
            if (Player.Instance.rect.Y + Player.Instance.rect.Height > windowHeight)
                Player.Instance.MoveBack();
            if (Player.Instance.rect.Y < 0)
                Player.Instance.MoveBack();
            if (monster.rect.X + monster.rect.Width > windowWidth)
                monster.MoveBack();
            if (monster.rect.X < 0)
                monster.MoveBack();
            if (monster.rect.Y + monster.rect.Height > windowHeight)
                monster.MoveBack();
            if (monster.rect.Y < 0)
                monster.MoveBack();
            #endregion

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

                level.Draw(spriteBatch);

                spriteBatch.Draw(circTex, whirl, Color.White);
                spriteBatch.Draw(circTex, whirl2, Color.White);

                spriteBatch.DrawString(font, "Player Hp: " + Player.Instance.Health.ToString(), new Vector2(10, 10), Color.Black);
                spriteBatch.DrawString(font, "Monster HP: " + monster.Health.ToString(), new Vector2(640, 10), Color.Black);

                monster.Draw(spriteBatch);
                Player.Instance.Draw(spriteBatch);
                spriteBatch.Draw(circTex, whirl, Color.White);
                spriteBatch.Draw(circTex, whirl2, Color.White);

            }

            if (builderMode == true)
                builder.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

//TODO: MATH IDEA
/////               HERE IS THE THEORY I HAD TOWARD THE LONG RANGE THING.
//
//Probabaly none of this would work, iterator looks bad because i wrote this when
//i was Attribute work.
//
//but basically, while the player and the enemy roam around 
//
//if the player is standing in one spot for too long and 
//    is within the range of the enemy's radar. 
//the radar would turn on (be drawn)
//and would stay on until the player is out of range.
//
//the player is always center of the radar.
//
//if the player remains within the radar the eenemy will fire.
//while the firing is going on the enemy is stationary 
//the player tries to move out of range
//
//while the enemy is shooting 
//the fireblasts will shoot At the player in random order within the matrix
//
//if abstract fireblast is already fired and the player is already out of range 
//the fireblast will continue its path.
//
//the enemy will continue firing if the player is still within the enemy range.
//
//
//let me know if this is too crazy, if was thinking about the math portion that we
//need to add 
//we would use magnitude, normalize, matrix, etc.
//
//just a thought
//
//
//Texture2D radar;
//Rectangle radarRect = new Rectangle(0, 0, 96, 96);
//bool radarOn = false;
//float stillTime = 5.0f;
//bool firing = false;
//bool shotsfired = false;
//(clas needed)Projectiles  plasmaBlast; or pB for short
//float Random randBlast;
//
//Enemy eLoc = new Vector2(Enemy.Position.X, Enemy.Position.Y);
//Vector2 playerLoc = new Vector2(Player.Instance.Position.X, Player.Instance.Position.Y);
//
//Vector2 NormLongAtk = new Vector2(eloc, playerLoc);
//Vector3 eRadar = new Vector3((radarRect.X + (32*2)), (radarRect.Y + (32*2)), 
//                            ((radarRect.x + radarRect.Y) + (32*2)));
//
//Vector2 blastMag = new Vector2 (pB.X, pB.Y);
//
//Vector2 NormBlast = new Vector2(playerLoc, blastMag);
//
//
//
/////

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using RC_Framework;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Net.NetworkInformation;

namespace finalAssignment
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        static public Texture2D texDefault = null;
        static public Texture2D texMove = null;
        static public Texture2D texHurt = null;
        static public Texture2D texFire = null;

        static public Texture2D texBack1 = null;
        static public Texture2D texBack2 = null;
        static public Texture2D texBack3 = null;
        static public Texture2D texEnemy1Stay = null;
        static public Texture2D texEnemy1Move = null;
        static public Texture2D texEnemy2Stay = null;
        static public Texture2D texEnemy2Move = null;
        static public Texture2D texBossStay = null;
        static public Texture2D texBossMove = null;
        static public Texture2D texPower = null;
        static public Texture2D texPower1 = null;
        static public Texture2D texPowerWord = null;
        static public Texture2D texBar = null;
        static public Texture2D texBullet = null;
        static public Texture2D texGround = null;
        static public Texture2D texCoin = null;
        static public Texture2D texGround2 = null;
        static public Texture2D texIcon = null;
        static public Texture2D texChat1 = null;
        static public Texture2D texChat2 = null;
        static public Texture2D texChat3 = null;
        static public Texture2D texChat5 = null;
        static public Texture2D texBomb = null;
        static public Texture2D texShield = null;
        static public Texture2D texTurn = null;
        static public Texture2D texbIcon = null;
        static public Texture2D texWin = null;
        static public Texture2D texLose = null;
        static public Texture2D texPause = null;
        static public Texture2D texAnim1 = null;
        static public Texture2D texAnim2 = null;
        static public Texture2D texAnim3 = null;
        static public Texture2D texHelp = null;
        static public Texture2D texSnow = null;
        static public Texture2D texEFire = null;
        static public Texture2D texParticle1 = null;
        static public Texture2D texParticle2 = null;
        static public Texture2D texParticle3 = null;
        static public Texture2D texIntro = null;
        static public Texture2D texDie = null;
        static public int score = 0;
        static public int highestScore = 0;
        static public bool startOver = false;
        static public int[] scores;
        static public int i = 0;

        static public RC_GameStateManager levelManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 1200;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LineBatch.init(GraphicsDevice);

            string dir = dir = Util.findDirWithFile("Cloud9.png");

            texBack1 = Util.texFromFile(GraphicsDevice, dir + "background1.png");
            texBack2 = Util.texFromFile(GraphicsDevice, dir + "Stage2.png");
            texBack3 = Util.texFromFile(GraphicsDevice, dir + "Stage3.png");
            texAnim1 = Util.texFromFile(GraphicsDevice, dir + "Boom3.png");
            texAnim2 = Util.texFromFile(GraphicsDevice, dir + "Boom6.png");
            texAnim3 = Util.texFromFile(GraphicsDevice, dir + "Boom16.png");
            texTurn = Util.texFromFile(GraphicsDevice, dir + "turn.png");
            texPause = Util.texFromFile(GraphicsDevice, dir + "pauseScreen.png");
            texHelp = Util.texFromFile(GraphicsDevice, dir + "Untitled.png");
            texEFire = Util.texFromFile(GraphicsDevice, dir + "fire.png");
            texChat1 = Util.texFromFile(GraphicsDevice, dir + "text1.png");
            texChat2 = Util.texFromFile(GraphicsDevice, dir + "text2.png");
            texChat3 = Util.texFromFile(GraphicsDevice, dir + "chat3.png");            
            texChat5 = Util.texFromFile(GraphicsDevice, dir + "chat5.png");
            texWin = Util.texFromFile(GraphicsDevice, dir + "screen2.png");
            texLose = Util.texFromFile(GraphicsDevice, dir + "screen1.png");
            texCoin = Util.texFromFile(GraphicsDevice, dir + "Coin3.png");
            texBomb = Util.texFromFile(GraphicsDevice, dir + "bomb.png");
            texbIcon = Util.texFromFile(GraphicsDevice, dir + "bIcon.png");
            texGround = Util.texFromFile(GraphicsDevice, dir + "Mountain4.png");
            texIcon = Util.texFromFile(GraphicsDevice, dir + "icon.png");
            texShield = Util.texFromFile(GraphicsDevice, dir + "shield.png");
            texDefault = Util.texFromFile(GraphicsDevice, dir + "soldier_default.png");
            texMove = Util.texFromFile(GraphicsDevice, dir + "soldier_move1.png");
            texHurt = Util.texFromFile(GraphicsDevice, dir + "soldier_hurt1.png");
            texFire = Util.texFromFile(GraphicsDevice, dir + "soldier_fire1.png");
            texPower = Util.texFromFile(GraphicsDevice, dir + "powerBar.png");
            texPower1 = Util.texFromFile(GraphicsDevice, dir + "powerBar1.png");
            texPowerWord = Util.texFromFile(GraphicsDevice, dir + "power.png");
            texBar = Util.texFromFile(GraphicsDevice, dir + "bar.png");
            texBullet = Util.texFromFile(GraphicsDevice, dir + "Bullet.png");
            texEnemy1Stay = Util.texFromFile(GraphicsDevice, dir + "enemy1Stay.png");
            texEnemy1Move = Util.texFromFile(GraphicsDevice, dir + "enemy1Move.png");
            texEnemy2Stay = Util.texFromFile(GraphicsDevice, dir + "enemy2Stay.png");
            texEnemy2Move = Util.texFromFile(GraphicsDevice, dir + "enemy2Move.png");
            texBossStay = Util.texFromFile(GraphicsDevice, dir + "bossStay.png");
            texBossMove = Util.texFromFile(GraphicsDevice, dir + "bossMove.png");
            texParticle1 = Util.texFromFile(GraphicsDevice, dir + "snow1.png");
            texParticle2 = Util.texFromFile(GraphicsDevice, dir + "snow2.png");
            texParticle3 = Util.texFromFile(GraphicsDevice, dir + "Cloud9.png");
            texIntro = Util.texFromFile(GraphicsDevice, dir + "intro.jpg");
            texDie = Util.texFromFile(GraphicsDevice, dir + "soldier_die.png");
            scores = new int[20];

            levelManager = new RC_GameStateManager();

            levelManager.AddLevel(0, new Intro());
            levelManager.getLevel(0).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(0).LoadContent();

            levelManager.AddLevel(1, new Stage_1());
            levelManager.getLevel(1).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(1).LoadContent();

            levelManager.AddLevel(2, new Stage1end());
            levelManager.getLevel(2).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(2).LoadContent();

            levelManager.AddLevel(3, new Stage_2());
            levelManager.getLevel(3).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(3).LoadContent();

            levelManager.AddLevel(4, new Stage2end());
            levelManager.getLevel(4).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(4).LoadContent();

            levelManager.AddLevel(5, new Final_stage());
            levelManager.getLevel(5).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(5).LoadContent();

            levelManager.AddLevel(6, new pause());
            levelManager.getLevel(6).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(6).LoadContent();

            levelManager.AddLevel(7, new help());
            levelManager.getLevel(7).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(7).LoadContent();

            levelManager.AddLevel(8, new win());
            levelManager.getLevel(8).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(8).LoadContent();

            levelManager.AddLevel(9, new lose());
            levelManager.getLevel(9).InitializeLevel(GraphicsDevice, spriteBatch, Content, levelManager);
            levelManager.getLevel(9).LoadContent();

            levelManager.setLevel(0);
        }

        protected override void Update(GameTime gameTime)
        {
            RC_GameStateParent.getKeyboardAndMouse();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (RC_GameStateParent.keyState.IsKeyDown(Keys.P) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.P)) // ***
            {
                levelManager.pushLevel(6);
            }
            if (RC_GameStateParent.keyState.IsKeyDown(Keys.F1) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.F1)) // ***
            {
                levelManager.pushLevel(7);
            }
            if ((RC_GameStateParent.keyState.IsKeyDown(Keys.D1) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.D1))
                || (RC_GameStateParent.keyState.IsKeyDown(Keys.D2) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.D2))) // ***
            {
                for (int i = 0; i < 6; i++)
                {
                    levelManager.getLevel(i).LoadContent();
                }
            }

            levelManager.getCurrentLevel().Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {         
            levelManager.getCurrentLevel().Draw(gameTime);
            base.Draw(gameTime);
        }  
    }
}
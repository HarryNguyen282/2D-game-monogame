using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using RC_Framework;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Net.Mail;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Xna.Framework.Audio;

namespace finalAssignment
{
    class Final_stage : RC_GameStateParent
    {
        SpriteFont Font;

        Sprite3 main = null;
        Sprite3 bar = null;
        Sprite3 ground = null;

        ImageBackground powerBar = null;
        ImageBackground powerBar1 = null;
        ImageBackground power = null;
        ImageBackground background = null;
        ImageBackground icon = null;
        ImageBackground icon1 = null;

        static public HealthBar healthBar = null;
        static public HealthBar bossHealth = null;

        Sprite3 enemy1 = null;
        Sprite3 enemy2 = null;
        Sprite3 enemy3 = null;

        Sprite3 boss = null;
        Sprite3 bullet = null;
        Sprite3 bomb1 = null;
        Sprite3 bomb2 = null;
        Sprite3 bomb3 = null;
        Sprite3 bomb4 = null;
        Sprite3 shield1 = null;
        Sprite3 shield2 = null;
        Sprite3 chat1 = null;
        Sprite3 chat2 = null;
        Sprite3 chat3 = null;
        Sprite3 chat4 = null;
        Sprite3 chat5 = null;
        Sprite3 turn = null;
        Sprite3 bossBase = null;
        Sprite3 eFire = null;
        Sprite3 eFire2 = null;
        Sprite3 eFire3 = null;

        SpriteList enemy = null;        
        SpriteList bomb = null;
        SpriteList shield = null;
        SpriteList chat = null;
        SpriteList boom = null;
        SpriteList bC = null;

        Random rnd = new Random();

        bool showBB = false;
        bool bulletFired = false;
        bool takeInput = true;
        bool moveEnemy = false;
        bool fire = true;
        bool intro = true;
        bool shieldActive = false;
        bool animChanged = false;
        bool hitBoss = false;
        bool play = true;
        bool playSound1 = true;
        bool playSound2 = true;
        bool playSound3 = true;
        bool playSound4 = true;

        int ballOffset;
        int yAngle = 1;
        int direction = 1;
        int a = -1;
        int enemyTick;
        int enemyTurn = 1;
        int tick;
        int animTick;
        int bossTick;
        int endTick;

        float mainSpeed = 5.5f;
        float pushBackDist = 20f;
        float b = 1;
        float xVel = 0;
        float yVel = 0;
        float gravity;
        float bulletX;
        float bulletY;
        float powerStrenght;
        float bombSpeed = 5f;
        float eGravity = 5f;
        float xFireOffset1;
        float yFireOffset1;
        float xFireOffset2;
        float yFireOffset2;
        float xFireOffset3;
        float yFireOffset3;

        Vector2 lineRoot = new Vector2();
        Vector2 linePeak = new Vector2();
        Vector2 enemy1Pos = new Vector2();
        Vector2 enemy2Pos = new Vector2();
        Vector2 enemy3Pos = new Vector2();
        Vector2 bossPos = new Vector2();

        Vector2[] bossPosList = new Vector2[5];
        Vector2[] enemy1PosList = new Vector2[3];
        Vector2[] enemy2PosList = new Vector2[3];
        Vector2[] enemy3PosList = new Vector2[3];

        ParticleSystem p;
        ParticleSystem p2;
        ParticleSystem p3;
        ParticleSystem p4;

        Vector2 pTarget = new Vector2(0, 0);

        Rectangle rectangle = new Rectangle(0, 0, 1200, 800);

        SoundEffect sound1;
        LimitSound limSound1;

        SoundEffect sound2;
        LimitSound limSound2;

        SoundEffect sound3;
        SoundEffectInstance limSound3;

        public override void LoadContent()
        {
            Font = Content.Load<SpriteFont>("Font");

            sound1 = Content.Load<SoundEffect>("bombSound");
            limSound1 = new LimitSound(sound1, 1);
            sound2 = Content.Load<SoundEffect>("flack");
            limSound2 = new LimitSound(sound2, 1);
            sound3 = Content.Load<SoundEffect>("backgroundMusic1");
            limSound3 = sound3.CreateInstance();

            background = new ImageBackground(Game1.texBack3, Color.White, graphicsDevice);

            ground = new Sprite3(true, Game1.texGround, 0, 795);
            ground.setWidthHeight(1200, 100);
            ground.setBB(0, 0, 1200, 100);

            powerBar = new ImageBackground(Game1.texPower, Color.White, graphicsDevice);
            powerBar.setPos(110, 680);
            powerBar.setWidthHeight(1100, 350);
            powerBar1 = new ImageBackground(Game1.texPower1, Color.White, graphicsDevice);
            powerBar1.setPos(110, 680);
            powerBar1.setWidthHeight(1100, 350);
            power = new ImageBackground(Game1.texPowerWord, Color.White, graphicsDevice);
            power.setWidthHeight(500, 220);
            power.setPos(-170, 745);

            bar = new Sprite3(true, Game1.texBar, -1275, 620);
            bar.setWidthHeight(1400, 480);

            bossPosList = new Vector2[5] { new Vector2(250, 200), new Vector2(400, 350), new Vector2(650, 500), new Vector2(800, 450), new Vector2(950, 200) };
            enemy1PosList = new Vector2[3] {new Vector2(1000, 350),   new Vector2(800, 120),  new Vector2(250, 400),};
            enemy2PosList = new Vector2[3] { new Vector2(200, 550), new Vector2(650, 300),  new Vector2(500, 200) };
            enemy3PosList = new Vector2[3] { new Vector2(750, 650), new Vector2(400, 600), new Vector2(950, 650), };

            icon = new ImageBackground(Game1.texIcon, null, new Rectangle(85, -20, 80, 80), Color.White);
            icon1 = new ImageBackground(Game1.texbIcon, null, new Rectangle(1050, 5, 30, 30), Color.White);

            boom = new SpriteList();

            Vector2[] fireAnim = new Vector2[5];
            fireAnim[0].X = 0; fireAnim[0].Y = 0;
            fireAnim[1].X = 1; fireAnim[1].Y = 0;
            fireAnim[2].X = 2; fireAnim[2].Y = 0;
            fireAnim[3].X = 3; fireAnim[3].Y = 0;
            fireAnim[4].X = 4; fireAnim[4].Y = 0;

            eFire = new Sprite3(false, Game1.texEFire, -100, 0);
            eFire.setXframes(5);
            eFire.setYframes(1);
            eFire.setWidthHeight(64, 70);         
            eFire.setAnimationSequence(fireAnim, 0, 4, 10);
            eFire.setAnimFinished(0);
            eFire.animationStart();

            eFire2 = new Sprite3(false, Game1.texEFire, -100, 0);
            eFire2.setXframes(5);
            eFire2.setYframes(1);
            eFire2.setWidthHeight(62, 70);
            eFire2.setAnimationSequence(fireAnim, 0, 4, 10);
            eFire2.setAnimFinished(0);
            eFire2.animationStart();

            eFire3 = new Sprite3(false, Game1.texEFire, -100, 0);
            eFire3.setXframes(5);
            eFire3.setYframes(1);
            eFire3.setWidthHeight(64, 70);
            eFire3.setAnimationSequence(fireAnim, 0, 4, 10);
            eFire3.setAnimFinished(0);
            eFire3.animationStart();

            bomb = new SpriteList();
            bomb1 = new Sprite3(true, Game1.texBomb, -100, 0);
            bomb1.setWidthHeight(65, 30);
            bomb1.boundingSphereRadius = 30;
            bomb2 = new Sprite3(true, Game1.texBomb, -100, 0);
            bomb2.setWidthHeight(65, 30);
            bomb2.boundingSphereRadius = 30;
            bomb3 = new Sprite3(true, Game1.texBomb, -100, 0);
            bomb3.setWidthHeight(65, 30);
            bomb3.boundingSphereRadius = 30;
            bomb4 = new Sprite3(true, Game1.texBomb, -100, 0);
            bomb4.setWidthHeight(65, 30);
            bomb4.boundingSphereRadius = 30;

            shield = new SpriteList();
            shield1 = new Sprite3(true, Game1.texShield, 0, 0);
            shield1.setWidthHeight(20, 125);
            shield1.setHSoffset(new Vector2(10,0));
            shield1.setBB(20, 0, 80, 535);
            shield2 = new Sprite3(true, Game1.texShield, 0, 0);
            shield2.setWidthHeight(20, 125);
            shield1.setHSoffset(new Vector2(10, 0));
            shield2.setBB(20, 0, 80, 535);

            healthBar = new HealthBar(Color.Red, Color.Black, Color.White, true);
            healthBar.bounds = new Rectangle(10, 10, 100, 20);
            healthBar.currentHp = 100;
            healthBar.gapOfbar = 1;

            bossHealth = new HealthBar(Color.DarkOrange, Color.White, Color.Black, true);
            bossHealth.gapOfbar = 1;
            bossHealth.currentHp = 100;

            chat = new SpriteList();
            chat1 = new Sprite3(false, Game1.texChat1, 0, 0);
            chat2 = new Sprite3(false, Game1.texChat2, 0, 0);
            chat3 = new Sprite3(false, Game1.texChat3, 0, 0);
            chat5 = new Sprite3(false, Game1.texChat5, 0, 0);
            turn = new Sprite3(false, Game1.texTurn, 350, 0);

            boss = new Sprite3(false, Game1.texBossStay, 600, -300);
            boss.setHSoffset(new Vector2(boss.getWidth() / 2, boss.getHeight() / 2));
            boss.setBB(10, 35, 90, 100);
            boss.setFlip(SpriteEffects.FlipHorizontally);
            boss.boundingSphereRadius = 100;

            bossBase = new Sprite3(true, Game1.texGround, boss.getPosX() - 15, boss.getPosY() + 85);
            bossBase.setWidthHeight(105, 10);
            bossBase.setBB(0, 0, 64, 55);

            enemy = new SpriteList();
    
            main = new Sprite3(true, Game1.texDefault, 100, 680);
            main.setXframes(5);
            main.setYframes(2);
            main.setWidthHeight(135, 115);
            main.setHSoffset(new Vector2(30, 0));
            main.setBB(30, 0, 250, 400);
            Vector2[] anim = new Vector2[10];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 0; anim[5].Y = 1;
            anim[6].X = 1; anim[6].Y = 1;
            anim[7].X = 2; anim[7].Y = 1;
            anim[8].X = 3; anim[8].Y = 1;
            anim[9].X = 4; anim[9].Y = 1;
            main.setAnimationSequence(anim, 0, 9, 10);      
            main.animationStart();

            bullet = new Sprite3(false, Game1.texBullet, 10, 10);
            bullet.setWidthHeight(30, 30);
            bullet.setBBToTexture();

            enemy1 = new Sprite3(false, Game1.texEnemy2Stay, -100, 450);
            enemy1.setHSoffset(new Vector2(enemy1.getWidth()/2, enemy1.getHeight()/2));
            enemy1.setBB(20, 15, 75, 90);
            enemy1.boundingSphereRadius = 60;

            enemy2 = new Sprite3(false, Game1.texEnemy1Stay, 550, -100);
            enemy2.setHSoffset(new Vector2(enemy2.getWidth() / 2, enemy2.getHeight() / 2));
            enemy2.setBB(20, 15, 75, 90);
            enemy2.boundingSphereRadius = 60;
  
            enemy3 = new Sprite3(false, Game1.texEnemy2Stay, 1300, 450);
            enemy3.setHSoffset(new Vector2(enemy3.getWidth() / 2, enemy3.getHeight() / 2));
            enemy3.setBB(20, 15, 75, 90);
            enemy3.boundingSphereRadius = 60;

            enemy.addSpriteReuse(enemy1);
            enemy.addSpriteReuse(enemy2);
            enemy.addSpriteReuse(enemy3);

            bomb.addSpriteReuse(bomb1);
            bomb.addSpriteReuse(bomb2);
            bomb.addSpriteReuse(bomb3);
            bomb.addSpriteReuse(bomb4);

            chat.addSpriteReuse(chat2);
            chat.addSpriteReuse(chat1);
            chat.addSpriteReuse(chat3);
            chat.addSpriteReuse(chat4);
            chat.addSpriteReuse(chat5);

            shield.addSpriteReuse(shield1);
            shield.addSpriteReuse(shield2);
            shield.addSpriteReuse(bossBase);

            boom.addSpriteReuse(eFire);
            boom.addSpriteReuse(eFire2);
            boom.addSpriteReuse(eFire3);

            bC = new SpriteList();
            bC.addSpriteReuse(main);
            bC.addSpriteReuse(enemy1);
            bC.addSpriteReuse(enemy2);
            bC.addSpriteReuse(enemy3);
            bC.addSpriteReuse(ground);

            xVel = (80 - yAngle) * 0.1f;

            p = new ParticleSystem(new Vector2(600, 0), 25, 1000, 60);
            p.setMandatory1(Game1.texParticle1, new Vector2(5, 5), new Vector2(20, 20), Color.White, new Color(255, 255, 255, 100));
            p.setMandatory2(-1, 1, 1, 10, 0);
            rectangle = new Rectangle(0, 0, 1200, 900);
            p.setMandatory3(1200, rectangle);
            p.setMandatory4(new Vector2(0, 0.005f), new Vector2(1, 0), new Vector2(0, 0));
            p.randomDelta = new Vector2(0.01f, 0.01f);
            p.Origin = 1;
            p.originRectangle = new Rectangle(0, 0, 1200, 10);
            p.activate();

            p2 = new ParticleSystem(new Vector2(900, 0), 25, 1000, 60);
            p2.setMandatory1(Game1.texParticle2, new Vector2(6, 6), new Vector2(22, 22), Color.White, new Color(255, 255, 255, 100));
            p2.setMandatory2(-1, 1, 1, 15, 0);
            rectangle = new Rectangle(0, 0, 1200, 900);
            p2.setMandatory3(360, rectangle);
            p2.setMandatory4(new Vector2(0, 0.01f), new Vector2(1, 0), new Vector2(0, 0));
            p2.randomDelta = new Vector2(0.01f, 0.01f);
            p2.Origin = 1;
            p2.originRectangle = new Rectangle(0, 0, 1200, 10);

            p3 = new ParticleSystem(new Vector2(155, 345), 20, 990, 100);
            p3.setMandatory1(Game1.texParticle3, new Vector2(12, 7), new Vector2(24, 14), Color.DarkGray, Color.LightGray);
            p3.setMandatory2(-1, 1, 1, 4, 0);
            rectangle = new Rectangle(0, 0, 1200, 900);
            p3.setMandatory3(120, rectangle);
            p3.setMandatory4(new Vector2(0, -0.003f), new Vector2(0.20f, -0.20f), new Vector2(0.1f, 0.1f));
            p3.randomDelta = new Vector2(0.01f, 0.01f);
            p3.moveTowards = 0;
            p3.activate();

            p4 = new ParticleSystem(new Vector2(300, 105), 20, 990, 100);
            p4.setMandatory1(Game1.texParticle3, new Vector2(10, 5), new Vector2(20, 10), Color.DarkGray, Color.LightGray);
            p4.setMandatory2(-1, 1, 1, 6, 0);
            rectangle = new Rectangle(0, 0, 1200, 900);
            p4.setMandatory3(120, rectangle);
            p4.setMandatory4(new Vector2(0, -0.003f), new Vector2(0.20f, -0.20f), new Vector2(0.1f, 0.1f));
            p4.randomDelta = new Vector2(0.01f, 0.01f);
            p4.moveTowards = 0;
            p4.activate();
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (Stage_2.startOver)
            {
                boss.setPos(600, -300);
                enemy1.setPos(-100, 450);
                enemy2.setPos(550, -100);
                enemy3.setPos(1300, 450);
                intro = true;
                healthBar.currentHp = 100;
                bossHealth.currentHp = 100;
                enemyTurn = 1;
                tick = 0;
                animTick = 0;
                bossTick = 0;
                endTick = 0;              
            }

            limSound3.Play();
            tick++;
            if (intro)
            {
                Stage_2.startOver = false;
                fire = false;
                takeInput = false;
                boss.setActiveAndVisible(true);
                boss.moveTo(new Vector2(600, 300), 4f, false);
                if (boss.getPos() == new Vector2(600, 300)) 
                {
                    chat3.setPos(boss.getPos() - new Vector2(250, 200));
                    chat3.setVisible(true);
                    if (tick > 300)
                    {
                        chat3.setVisible(false);
                        intro = false;
                        bulletFired = true;
                        fire = true;                                         
                    }
                }
            }        

            if (tick == 500) { p2.activate(); }
            if (tick % 1500 == 0) { p2.reset(); }
            if (tick % 1000 == 0) { p.reset(); p3.reset(); p4.reset(); }

            if (hitBoss)
            {
                if (tick % 5 == 0)
                {
                    bossTick++;
                    if (boss.visible == false) { boss.setVisible(true); } else boss.setVisible(false);
                }                 
            }
            if (bossTick == 10) { hitBoss = false; boss.setVisible(true); bossTick = 0; }

            bossBase.setPos(boss.getPosX() - 55, boss.getPosY() + 85);
            bossHealth.bounds = new Rectangle(1090, 10, 100, 20);

            chat2.setPos(boss.getPosX() - 250, boss.getPosY() - 200);
            chat1.setPos(boss.getPosX() - 250, boss.getPosY() - 200);
            chat4.setPos(boss.getPosX() - 250, boss.getPosY() - 200);
            chat5.setPos(boss.getPosX() - 250, boss.getPosY() - 200);

            shield1.setPos(boss.getPosX() - 75, boss.getPosY() - 50);
            shield2.setPos(boss.getPosX() + 60, boss.getPosY() - 50);

            shield1.setActiveAndVisible(shieldActive);
            shield2.setActiveAndVisible(shieldActive);

            eFire.setPos(enemy1.getPosX() - xFireOffset1, enemy1.getPosY() - yFireOffset1);
            eFire2.setPos(enemy2.getPosX() - xFireOffset2, enemy2.getPosY() - yFireOffset2);
            eFire3.setPos(enemy3.getPosX() - xFireOffset3, enemy3.getPosY() - yFireOffset3);

            if (RC_GameStateParent.keyState.IsKeyDown(Keys.B) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.B)) { showBB = !showBB; };

            if (takeInput)
            {
                if (RC_GameStateParent.keyState.IsKeyDown(Keys.Up))
                {
                    if (yAngle < 70) yAngle++; yVel = (-yAngle * 0.1f) / 1.5f;
                    xVel = (80 - yAngle) * 0.1f;
                }
                else if (RC_GameStateParent.keyState.IsKeyDown(Keys.Down))
                {
                    if (yAngle > 0) yAngle--; yVel = (-yAngle * 0.1f) / 1.5f;
                    xVel = (80 - yAngle) * 0.1f;
                }

                if (RC_GameStateParent.keyState.IsKeyDown(Keys.Left))
                {

                    main.setTexture(Game1.texMove, false);
                    main.setFlip(SpriteEffects.FlipHorizontally);
                    main.setHSoffset(new Vector2(150, 0));
                    main.setBB(150, 0, 250, 400);
                    main.setTickBetweenFrame(2);
                    if (main.getPosX() > 50)
                    {
                        main.setPosX(main.getPosX() - mainSpeed);
                    }
                    direction = -1;
                    ballOffset = -50;
                }

                else if (RC_GameStateParent.keyState.IsKeyDown(Keys.Right))
                {
                    main.setTexture(Game1.texMove, false);
                    main.setTickBetweenFrame(2);
                    main.setFlip(SpriteEffects.None);
                    main.setHSoffset(new Vector2(30, 0));
                    main.setBB(30, 0, 250, 400);
                    if (main.getPosX() + main.getWidth() < 1190)
                    {
                        main.setPosX(main.getPosX() + mainSpeed);
                    }
                    direction = 1;
                    ballOffset = 0;
                }
                else
                {
                    if (fire)
                    {
                        main.setTexture(Game1.texDefault, false);
                        main.setTickBetweenFrame(10);
                    }
                }
            }

            bullet.setPosX(main.getPosX() + main.getWidth() / 2 + ballOffset*1.5f);
            bullet.setPosY(main.getPosY() + main.getHeight() / 2);

            powerStrenght = bar.getPosX() + bar.getWidth();

            bool shot = false;

            if (RC_GameStateParent.prevKeyState.IsKeyDown(Keys.Space) && fire)
            {
                b = 1;
                if (powerStrenght < 1195) { bar.setPosX(bar.getPosX() + 4); }
                animChanged = true;
            }
            else
            {
                shot = true;
                if (powerStrenght < 232) { bar.setPosX(-1275); }
                else
                {                    
                    fire = false;
                    takeInput = false;
                    if (animChanged)
                    {
                        if (direction == -1)
                        {
                            main.setTexture(Game1.texFire, false);
                            main.setTickBetweenFrame(10);
                            main.setFlip(SpriteEffects.FlipHorizontally);
                        }
                        else
                        {
                            main.setTexture(Game1.texFire, false);
                            main.setFlip(SpriteEffects.None);
                            main.setTickBetweenFrame(10);
                        }
                    }                       
                    float a = (powerStrenght - 125) / 1075;
                    gravity = 1 / (a * 100f);
                    yVel = yVel + gravity;
                    float xVelo = xVel + a * 0.1f;

                    bulletY = bullet.getPosY() + yVel * b;
                    bulletX = bullet.getPosX() + xVelo * b * direction;
                    b += 2.5f;

                    bullet.active = true;
                    bullet.visible = true;
                    bullet.setPos(new Vector2(bulletX, bulletY));
                    bullet.alignDisplayAngle();
                    bulletCollision();                     
                }               
            } 

            if (shot) { animTick++; }

            if (shot && animTick >= 120)
            {
                main.setTexture(Game1.texDefault, false);
                main.setTickBetweenFrame(10);
                shot = false;
                animTick = 0;
                animChanged = false;
            }

            if (bulletFired)
            {
                enemy1Pos = enemy1PosList[rnd.Next(0,3)];
                enemy2Pos = enemy2PosList[rnd.Next(0, 3)];
                enemy3Pos = enemy3PosList[rnd.Next(0, 3)];
                bossPos = bossPosList[rnd.Next(0, 4)];
                moveEnemy = true;
                bulletFired = false;
                a = a * -1;
            }

            if (moveEnemy)
            {
                takeInput = false;
                fire = false;
                enemyTick++;
                if (enemyTurn == 1) { callMinions(); }
                else if (enemyTurn == 2) { moveMinions(); }
                else if (enemyTurn == 3) { rainFire(); }
                else if (enemyTurn == 4) { minionSuicide(); mainCollision(); }
            }

            if (healthBar.currentHp == 0)
            {
                endTick++;
                main.setTexture(Game1.texDie, false);
                main.setTickBetweenFrame(30);
                main.setAnimFinished(0);
                takeInput = false;
                fire = false;
                limSound3.Stop();
                //if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; }
                if (endTick > 120) { Game1.levelManager.pushLevel(9); endTick = 0; }
            }

            if (bossHealth.currentHp == 0)
            {
                endTick++;
                if (endTick < 60)
                {
                    moveEnemy = false;
                    takeInput = false;
                    boss.active = false;
                    boss.visible = false;
                    enemy1.setPos(-200, 0);
                    enemy2.setPos(-200, 0);
                    enemy3.setPos(-200, 0);
                    chat1.setActiveAndVisible(false);
                    chat2.setActiveAndVisible(false);
                    chat3.setActiveAndVisible(false);
                    chat4.setActiveAndVisible(false);
                    chat5.setActiveAndVisible(false);                    
                   // if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; }
                }
                else { Game1.levelManager.pushLevel(8); limSound3.Stop(); }               
            }

            if (fire)
            {
                if (tick % 45 == 0)
                {
                    if (turn.visible == false) { turn.setVisible(true); } else turn.setVisible(false);
                }
            }

            if (bullet.getPosX() > 1200 || bullet.getPosX() < 0)
            {
                bullet.active = false;
                bullet.visible = false;
                bar.setPosX(-1275);
                yVel = (-yAngle * 0.1f) / 1.5f;
                xVel = (80 - yAngle) * 0.1f;
                bulletFired = true;
            }
            linePeak = main.getPos() + new Vector2(main.getWidth() / 2 + 50 * direction - yAngle * 0.3f * direction + ballOffset, main.getHeight() / 2 + 10 - yAngle);
            lineRoot = main.getPos() + new Vector2(main.getWidth() / 2 + ballOffset, main.getHeight() / 2 + 10);

            boom.animationTick(gameTime);
            main.animationTick(gameTime);
            p.Update(gameTime);
            p2.Update(gameTime);
            p3.Update(gameTime);
            p4.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            p.Draw(spriteBatch);
            p2.Draw(spriteBatch);
            p3.Draw(spriteBatch);
            p4.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            icon.Draw(spriteBatch);
            icon1.Draw(spriteBatch);
            bossHealth.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
            shield.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            chat.Draw(spriteBatch);
            turn.draw(spriteBatch);
            spriteBatch.DrawString(Font, "SCORE " + (Game1.score*100).ToString(), new Vector2(700, 10), Color.LightGoldenrodYellow);

            LineBatch.drawLine(spriteBatch, Color.White, lineRoot, linePeak);
           
            main.Draw(spriteBatch);
            bullet.Draw(spriteBatch);
            boss.Draw(spriteBatch);
            bomb.Draw(spriteBatch);
            powerBar.Draw(spriteBatch);
            bar.Draw(spriteBatch);
            powerBar1.Draw(spriteBatch);
            power.Draw(spriteBatch);
            boom.Draw(spriteBatch);

            if (showBB) { showBoundingBoxes(); };

            spriteBatch.End();

        }      

        protected void callMinions()
        {
            eFire.setVisible(false);
            eFire3.setVisible(false);
            eFire2.setVisible(false);
            if (enemyTick < 120)
            {
                chat5.setVisible(false);
                chat4.setVisible(false);
                boss.setTexture(Game1.texBossMove, false);
                if (boss.getPosX() > bossPos.X) { boss.setFlip(SpriteEffects.FlipHorizontally); } else { boss.setFlip(SpriteEffects.None); }
                boss.moveTo(bossPos, 3.5f, false);
            }
            else
            {
                boss.setTexture(Game1.texBossStay, false);
                boss.setFlip(SpriteEffects.None);
                if (enemyTick < 180)
                {
                    chat2.setVisible(true);
                }
                else if (enemyTick < 400)
                {
                    chat2.setVisible(false);
                    enemy1.setActiveAndVisible(true);
                    enemy1.moveAndDodge(enemy1Pos, 6.5f, boss, pushBackDist);
                    enemy2.setActiveAndVisible(true);
                    enemy2.moveAndDodge(enemy2Pos, 6.5f, boss, pushBackDist);
                    enemy3.setActiveAndVisible(true);
                    enemy3.moveAndDodge(enemy3Pos, 6.5f, boss, pushBackDist);
                }
                else
                {
                    moveEnemy = false;
                    takeInput = true;
                    enemyTick = 0;
                    enemyTurn++;
                    fire = true;
                }
            }      
        }

        protected void moveMinions()
        {
            if (enemyTick < 120)
            {
                boss.setTexture(Game1.texBossMove, false);
                if (boss.getPosX() > bossPos.X) { boss.setFlip(SpriteEffects.FlipHorizontally); } else { boss.setFlip(SpriteEffects.None); }
                //boss.moveTo(bossPos, 3.5f, false);
                boss.bossMoveAndDodge(bossPos, 5f, enemy, pushBackDist);
            }
            else if (enemyTick < 240)
            {
                boss.setTexture(Game1.texBossStay, false);
                boss.setFlip(SpriteEffects.None);

                enemy1.setTexture(Game1.texEnemy2Move, false);
                enemy2.setTexture(Game1.texEnemy1Move, false);
                enemy3.setTexture(Game1.texEnemy2Move, false);

                if (enemy1.getPosX() > enemy1Pos.X) { enemy1.setFlip(SpriteEffects.FlipHorizontally); } else { enemy1.setFlip(SpriteEffects.None); }
                if (enemy2.getPosX() > enemy2Pos.X) { enemy2.setFlip(SpriteEffects.FlipHorizontally); } else { enemy2.setFlip(SpriteEffects.None); }
                if (enemy3.getPosX() > enemy3Pos.X) { enemy3.setFlip(SpriteEffects.FlipHorizontally); } else { enemy3.setFlip(SpriteEffects.None); }

                if (enemy1.getVisible() == true) { enemy1.moveAndDodge(enemy1Pos, 5.5f, boss, pushBackDist); }
                if (enemy2.getVisible() == true) { enemy2.moveAndDodge(enemy2Pos, 5.5f, boss, pushBackDist); }
                if (enemy3.getVisible() == true) { enemy3.moveAndDodge(enemy3Pos, 5.5f, boss, pushBackDist); }
            }
            else 
            { 
                moveEnemy = false;
                enemy1.setTexture(Game1.texEnemy2Stay, false);
                enemy2.setTexture(Game1.texEnemy1Stay, false);
                enemy3.setTexture(Game1.texEnemy2Stay, false);
                enemy1.setFlip(SpriteEffects.None);
                enemy2.setFlip(SpriteEffects.None);
                enemy3.setFlip(SpriteEffects.None);
                takeInput = true;
                enemyTick = 0;
                enemyTurn++;
                fire = true;
            }
        }

        protected void minionSuicide()
        {
            if (enemyTick < 120)
            {
                boss.setTexture(Game1.texBossMove, false);
                if (boss.getPosX() > bossPos.X) { boss.setFlip(SpriteEffects.FlipHorizontally); } else { boss.setFlip(SpriteEffects.None); }
                boss.moveTo(bossPos, 3.5f, false);
            }
            else
            {               
                boss.setTexture(Game1.texBossStay, false);
                boss.setFlip(SpriteEffects.None);               
            }

            if (enemy1.getVisible() == false && enemy2.getVisible() == false && enemy3.getVisible() == false && enemyTick > 180)
            {                          
                moveEnemy = false;
                takeInput = true;
                enemyTick = 0;
                enemyTurn = 1;
                fire = true;  
                chat5.setVisible(false);
            }
            else
            {
                if (enemy1.getPosX() > main.getPosX()) { enemy1.setFlip(SpriteEffects.FlipHorizontally); xFireOffset1 = 11; } else { enemy1.setFlip(SpriteEffects.None); }
                if (enemy2.getPosX() > main.getPosX()) { enemy2.setFlip(SpriteEffects.FlipHorizontally); xFireOffset2 = 0; } else { enemy2.setFlip(SpriteEffects.None); }
                if (enemy3.getPosX() > main.getPosX()) { enemy3.setFlip(SpriteEffects.FlipHorizontally); xFireOffset3 = 11;} else { enemy3.setFlip(SpriteEffects.None); }

                if (enemy1.getVisible() == true) { enemy1.moveTo(main.getPos(), 5f, false); chat5.setVisible(true); }
                if (enemy2.getVisible() == true) { enemy2.moveTo(main.getPos(), 5f, false); chat5.setVisible(true); }
                if (enemy3.getVisible() == true) { enemy3.moveTo(main.getPos(), 5f, false); chat5.setVisible(true); }
            }                 
        }

        protected void rainFire()
        {            
            if (enemyTick < 60) 
            { 
                chat1.setVisible(true);
                bomb1.setPos(rnd.Next(200,950), -100);
                bomb2.setPos(rnd.Next(200, 950), -100); 
                bomb3.setPos(rnd.Next(200, 950), -100); 
                bomb4.setPos(rnd.Next(200, 950), -100); 
            }
            else
            {
                chat1.setVisible(false);
                takeInput = true;
                if (enemyTick  > 60) 
                {
                    boss.setTexture(Game1.texBossMove, false);
                    if (boss.getPosX() > bossPos.X) { boss.setFlip(SpriteEffects.FlipHorizontally); } else { boss.setFlip(SpriteEffects.None); }
                    boss.moveTo(new Vector2(boss.getPosX(), -200), 6f, false);
                }
                if (enemyTick > 180 && bomb1.getPosY() < 900)
                {
                    bomb1.moveTo1(main.getPos(), bombSpeed, true, eGravity);    
                    if (playSound1) { limSound1.playSoundIfOk(); playSound1 = false; }
                }
                if (enemyTick > 300 && bomb2.getPosY() < 900)
                {
                    bomb2.moveTo1(main.getPos(), bombSpeed, true, eGravity);
                    if (playSound2) { limSound1.playSoundIfOk(); playSound2 = false; }
                }
                if (enemyTick > 420 && bomb3.getPosY() < 900)
                {
                    bomb3.moveTo1(main.getPos(), bombSpeed, true, eGravity);
                    if (playSound3) { limSound1.playSoundIfOk(); playSound3 = false; }
                }
                if (enemyTick > 540 && bomb4.getPosY() < 900)
                {
                    bomb4.moveTo1(main.getPos(), bombSpeed, true, eGravity);
                    if (playSound4) { limSound1.playSoundIfOk(); playSound4 = false; }
                }

                int hit1 = bomb1.bombCollision(enemy, ground, main, healthBar);
                Game1.score += hit1;
                if (hit1 == -1)
                {
                    main.setTexture(Game1.texHurt, false);
                    main.setTickBetweenFrame(8);
                    bombExplosion((int)bomb1.getPosX() - 17, (int)bomb1.getPosY() - 4);
                    
                }
                else if (hit1 == 0 || hit1 == 1) { bombExplosion((int)bomb1.getPosX()-17, (int)bomb1.getPosY()-4);  }
                int hit2 = bomb2.bombCollision(enemy, ground, main, healthBar);
                Game1.score += hit2;
                if (hit2 == -1)
                {
                    main.setTexture(Game1.texHurt, false);
                    main.setTickBetweenFrame(8);
                    bombExplosion((int)bomb2.getPosX()-17, (int)bomb2.getPosY()-4);
                }
                else if (hit2 == 0 || hit2 == 1) { bombExplosion((int)bomb2.getPosX() - 17, (int)bomb2.getPosY() - 4); }

                int hit3 = bomb3.bombCollision(enemy, ground, main, healthBar);
                Game1.score += hit3;
                if (hit3 == -1)
                {
                    main.setTexture(Game1.texHurt, false);
                    main.setTickBetweenFrame(8);
                    bombExplosion((int)bomb3.getPosX() - 17, (int)bomb3.getPosY() - 4);
                }
                else if (hit3 == 0 || hit3 == 1) { bombExplosion((int)bomb3.getPosX() - 17, (int)bomb3.getPosY() - 4); }

                int hit4 = bomb4.bombCollision(enemy, ground, main, healthBar);
                    Game1.score += hit4;
                if (hit4 == -1)
                {
                    main.setTexture(Game1.texHurt, false);
                    main.setTickBetweenFrame(8);
                    bombExplosion((int)bomb4.getPosX() - 17, (int)bomb4.getPosY() - 4);
                }
                else if (hit4 == 0 || hit4 == 1) { bombExplosion((int)bomb4.getPosX() - 17, (int)bomb4.getPosY() - 4); }


                if (enemyTick > 700)
                {                   
                    boss.moveTo(bossPos, 12f, false);
                    boss.setTexture(Game1.texBossStay, false);
                    boss.setFlip(SpriteEffects.None);
                    shieldActive = !shieldActive;
                    playSound1 = true;
                    playSound2 = true;
                    playSound3 = true;
                    playSound4 = true;
                }
                if (enemyTick > 900)
                {
                    moveEnemy = false;
                    enemyTick = 0;
                    enemyTurn++;
                    fire = true;
                    xFireOffset1 = 56;
                    yFireOffset1 = 113;
                    xFireOffset2 = 62;
                    yFireOffset2 = 115;
                    xFireOffset3 = 56;
                    yFireOffset3 = 113;
                    eFire.setVisible(true);
                    eFire3.setVisible(true);
                    eFire2.setVisible(true);                   
                }
            }
        }
        protected void bulletCollision()
        {
            Rectangle bulletBB = bullet.getBoundingBoxAA();
            int rc = enemy.collisionWithRect(bulletBB);
            bool gc = ground.collision(bullet);
            bool Bc = boss.collision(bullet);
            int bc = shield.collisionWithRect(bulletBB);

            if (bc != -1)
            {
                Sprite3 temp = shield.getSprite(bc);
                Rectangle rect = temp.Intersect(bulletBB);
                bulletExplosion(rect.X, rect.Y);
                bullet.setPosX(1300);
                bulletFired = true;
                limSound2.playSoundIfOk();
            }
            else if (rc != -1)
            {
                Sprite3 temp = enemy.getSprite(rc);
                bulletExplosion((int)bullet.getPosX(), (int)bullet.getPosY());
                temp.setActive(false);
                temp.setVisible(false);
                temp.setPosX(1300);
                bullet.setPosX(1300);
                bulletFired = true;
                Game1.score++;
                limSound2.playSoundIfOk();
            }
            else if (gc)
            {
                Rectangle rect = ground.Intersect(bulletBB);
                bulletExplosion(rect.X, rect.Y);
                bullet.setPosX(1300);
                bulletFired = true;
                limSound2.playSoundIfOk();
            }
            else if (Bc)
            {
                bulletExplosion((int)bullet.getPosX(), (int)bullet.getPosY());
                bossTick++;
                Game1.score = Game1.score + 2;
                bossHealth.currentHp -= 50;
                bullet.setPosX(1300);
                bulletFired = true;
                hitBoss = true;
                limSound2.playSoundIfOk();
            }
        }

        public void mainCollision()
        {
            Vector2[] tempPos = new Vector2[3] { new Vector2(-100, 450), new Vector2(550, -100), new Vector2(1300, 450) };
            Rectangle mainBB = main.getBoundingBoxAA();
            int ec = enemy.collisionWithRect(mainBB);

            if (ec != -1)
            {              
                Sprite3 temp = enemy.getSprite(ec);
                //Rectangle rect = temp.Intersect(mainBB);
                //enemyExplosion(rect.X, rect.Y);
                temp.setPos(tempPos[rnd.Next(0, 3)]);
                temp.visible = false;
                temp.active = false;                
                healthBar.currentHp -= 10;
                Game1.score -= 1;
                main.setTexture(Game1.texHurt, false);
                main.setTickBetweenFrame(8);
                limSound2.playSoundIfOk();
            }
        }

        public void showBoundingBoxes()
        {
            main.drawBB(spriteBatch, Color.White);
            main.drawHS(spriteBatch, Color.Black);
            enemy1.drawBB(spriteBatch, Color.White);
            enemy1.drawHS(spriteBatch, Color.Red);
            enemy2.drawBB(spriteBatch, Color.White);
            enemy2.drawHS(spriteBatch, Color.Red);
            enemy3.drawBB(spriteBatch, Color.White);
            enemy3.drawHS(spriteBatch, Color.White);
            bullet.drawBB(spriteBatch, Color.White);
            bullet.drawHS(spriteBatch, Color.Red);
            ground.drawBB(spriteBatch, Color.White);
            ground.drawHS(spriteBatch, Color.Red);
            boss.drawBB(spriteBatch, Color.White);
            boss.drawHS(spriteBatch, Color.Red);
            shield1.drawBB(spriteBatch, Color.White);
            shield1.drawHS(spriteBatch, Color.Red);
            shield2.drawHS(spriteBatch, Color.Red);
            shield2.drawBB(spriteBatch, Color.White);
            bomb1.drawBB(spriteBatch, Color.White);
            bomb1.drawHS(spriteBatch, Color.Red);
            bomb2.drawBB(spriteBatch, Color.White);
            bomb2.drawHS(spriteBatch, Color.Red);
            bomb3.drawBB(spriteBatch, Color.White);
            bomb3.drawHS(spriteBatch, Color.Red);
            bomb4.drawBB(spriteBatch, Color.White);
            bomb4.drawHS(spriteBatch, Color.Red);
        }
        public void bulletExplosion(int x, int y)
        {
            int xoffset = -20;
            int yoffset = -20;

            Sprite3 s = new Sprite3(true, Game1.texAnim1, x + xoffset, y + yoffset);
            s.setXframes(7);
            s.setYframes(3);
            s.setWidthHeight(50, 50);

            Vector2[] anim = new Vector2[21];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 0; anim[7].Y = 1;
            anim[8].X = 1; anim[8].Y = 1;
            anim[9].X = 2; anim[9].Y = 1;
            anim[10].X = 3; anim[10].Y = 1;
            anim[11].X = 4; anim[11].Y = 1;
            anim[12].X = 5; anim[12].Y = 1;
            anim[13].X = 6; anim[13].Y = 1;
            anim[14].X = 0; anim[14].Y = 2;
            anim[15].X = 1; anim[15].Y = 2;
            anim[16].X = 2; anim[16].Y = 2;
            anim[17].X = 3; anim[17].Y = 2;
            anim[18].X = 4; anim[18].Y = 2;
            anim[19].X = 5; anim[19].Y = 2;
            anim[20].X = 6; anim[20].Y = 2;
            s.setAnimationSequence(anim, 0, 20, 2);
            s.setAnimFinished(2); 
            s.animationStart();

            boom.addSpriteReuse(s); 
        }

        public void bombExplosion(int x, int y)
        {
            int xoffset = -20;
            int yoffset = -20;

            Sprite3 s = new Sprite3(true, Game1.texAnim2, x + xoffset, y + yoffset);
            s.setXframes(7);
            s.setYframes(3);
            s.setWidthHeight(50, 50);

            Vector2[] anim = new Vector2[21];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 0; anim[7].Y = 1;
            anim[8].X = 1; anim[8].Y = 1;
            anim[9].X = 2; anim[9].Y = 1;
            anim[10].X = 3; anim[10].Y = 1;
            anim[11].X = 4; anim[11].Y = 1;
            anim[12].X = 5; anim[12].Y = 1;
            anim[13].X = 6; anim[13].Y = 1;
            anim[14].X = 0; anim[14].Y = 2;
            anim[15].X = 1; anim[15].Y = 2;
            anim[16].X = 2; anim[16].Y = 2;
            anim[17].X = 3; anim[17].Y = 2;
            anim[18].X = 4; anim[18].Y = 2;
            anim[19].X = 5; anim[19].Y = 2;
            anim[20].X = 6; anim[20].Y = 2;
            s.setAnimationSequence(anim, 0, 20, 2);
            s.setAnimFinished(2);
            s.animationStart();

            boom.addSpriteReuse(s);
        }

        public void enemyExplosion(int x, int y)
        {
            Sprite3 s = new Sprite3(true, Game1.texAnim3, x - 20, y - 20);
            s.setXframes(10);
            s.setYframes(2);
            s.setWidthHeight(50, 50);

            Vector2[] anim = new Vector2[20];
            anim[0].X = 0; anim[0].Y = 0;
            anim[1].X = 1; anim[1].Y = 0;
            anim[2].X = 2; anim[2].Y = 0;
            anim[3].X = 3; anim[3].Y = 0;
            anim[4].X = 4; anim[4].Y = 0;
            anim[5].X = 5; anim[5].Y = 0;
            anim[6].X = 6; anim[6].Y = 0;
            anim[7].X = 7; anim[7].Y = 0;
            anim[8].X = 8; anim[8].Y = 0;
            anim[9].X = 9; anim[9].Y = 0;
            anim[10].X = 0; anim[10].Y = 1;
            anim[11].X = 1; anim[11].Y = 1;
            anim[12].X = 2; anim[12].Y = 1;
            anim[13].X = 3; anim[13].Y = 1;
            anim[14].X = 4; anim[14].Y = 1;
            anim[15].X = 5; anim[15].Y = 1;
            anim[16].X = 6; anim[16].Y = 1;
            anim[17].X = 7; anim[17].Y = 1;
            anim[18].X = 8; anim[18].Y = 1;
            anim[19].X = 9; anim[19].Y = 1;
            s.setAnimationSequence(anim, 0, 19, 2);
            s.setAnimFinished(2);
            s.animationStart();

            boom.addSpriteReuse(s);
        }
    }
}



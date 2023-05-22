using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System;
using RC_Framework;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.Xna.Framework.Audio;

namespace finalAssignment
{
    class Stage_1: RC_GameStateParent
    {
        Sprite3 main = null;
        Sprite3 bar = null;
        ImageBackground powerBar = null;
        ImageBackground powerBar1 = null;
        ImageBackground power = null;
        ImageBackground background = null;
        ImageBackground icon = null;
        HealthBar healthBar = null;
        Sprite3 enemy1 = null;
        Sprite3 enemy2 = null;
        Sprite3 enemy3 = null;
        Sprite3 enemy4 = null;
        Sprite3 eFire = null;
        Sprite3 eFire2 = null;
        Sprite3 eFire3 = null;
        Sprite3 eFire4 = null;
        Sprite3 ground = null;
        Sprite3 turn = null;
        Sprite3 coin1 = null;
        Sprite3 coin2 = null;
        Sprite3 bullet = null;
        SpriteList enemy = null;
        SpriteList coin = null;  
        SpriteList boom = null;
        SpriteList bC = null;
        SpriteFont Font;

        Random rnd = new Random();

        bool showBB = false;
        bool bulletFired = false;
        bool takeInput = true;
        bool moveEnemy = false;
        bool fire = true;
        bool animChanged = false;
        bool gameStart = true;
        bool drawString = false;
        static public bool startOver = false;

        int ballOffset;
        int yAngle = 1;
        int tick;
        int direction = 1;
        int a = 1;
        int enemyTick;
        int animTick;
        int enemyTurn = 1;
        int endTick;
        int hit = 0;

        float mainSpeed = 5.5f;
        float pushBackDist = 20f;
        float b = 1;
        float xVel = 0;
        float yVel = 0;
        float gravity;
        float bulletX;
        float bulletY;
        float powerStrenght;

        float xFireOffset1;
        float yFireOffset1;
        float xFireOffset2;
        float yFireOffset2;
        float xFireOffset3;
        float yFireOffset3;
        float xFireOffset4;
        float yFireOffset4; 

        Vector2 lineRoot = new Vector2();
        Vector2 linePeak = new Vector2();
        Vector2 enemy1Pos = new Vector2();
        Vector2 enemy2Pos = new Vector2();
        Vector2 enemy3Pos = new Vector2();
        Vector2 enemy4Pos = new Vector2();

        SoundEffect sound1;
        LimitSound limSound1;

        SoundEffect sound2;
        LimitSound limSound2;

        SoundEffect sound3;
        SoundEffectInstance limSound3;
        public override void LoadContent()
        {
            LineBatch.init(graphicsDevice);
            Font = Content.Load<SpriteFont>("Font");

            sound1 = Content.Load<SoundEffect>("ding");
            limSound1 = new LimitSound(sound1, 1);
            sound2 = Content.Load<SoundEffect>("flack");
            limSound2 = new LimitSound(sound2, 1);
            sound3 = Content.Load<SoundEffect>("backgroundMusic1");
            limSound3 = sound3.CreateInstance();

            background = new ImageBackground(Game1.texBack1, Color.White, graphicsDevice);
            ground = new Sprite3(true, Game1.texGround, 0, 795);
            ground.setWidthHeight(1200, 100);
            ground.setBB(0, 0, 1200, 100);

            icon = new ImageBackground(Game1.texIcon, null, new Rectangle(85, -20, 80, 80), Color.White);
            
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

            enemy = new SpriteList();
            coin = new SpriteList();
            boom = new SpriteList();

            turn = new Sprite3(false, Game1.texTurn, 250, 0);

            main = new Sprite3(true, Game1.texDefault, 100, 680);
            main.setXframes(5);
            main.setYframes(2);
            main.setWidthHeight(135, 115);
            main.setHSoffset(new Vector2(30, 0));
            main.setBB(30, 0, 350, 400);
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

            healthBar = new HealthBar(Color.Red, Color.Black, Color.White, true);
            healthBar.bounds = new Rectangle(10, 10, 100, 20);
            healthBar.currentHp = 100;
            healthBar.gapOfbar = 1;

            bullet = new Sprite3(false, Game1.texBullet, 10, 10);
            bullet.setWidthHeight(30, 30);
            bullet.setBBToTexture();

            coin1 = new Sprite3(true, Game1.texCoin, 600, 500);
            coin1.setXframes(8);
            coin1.setYframes(1);
            coin1.setWidthHeight(64, 64);
            coin1.setBB(5, 5, 50, 54);
            coin1.boundingSphereRadius = 50;
            Vector2[] seq = new Vector2[8];
            seq[0].X = 0; seq[0].Y = 0;
            seq[1].X = 1; seq[1].Y = 0;
            seq[2].X = 2; seq[2].Y = 0;
            seq[3].X = 3; seq[3].Y = 0;
            seq[4].X = 4; seq[4].Y = 0;
            seq[5].X = 5; seq[5].Y = 0;
            seq[6].X = 6; seq[6].Y = 0;
            seq[7].X = 7; seq[7].Y = 0;

            coin1.setAnimationSequence(seq, 0, 7, 5);
            coin1.animationStart();

            coin2 = new Sprite3(true, Game1.texCoin, 900, 300);
            coin2.setXframes(8);
            coin2.setYframes(1);
            coin2.setWidthHeight(64, 64);
            coin2.setBB(5, 5, 50, 54);
            coin2.boundingSphereRadius = 50;
            coin2.setAnimationSequence(seq, 0, 7, 15);
            coin2.animationStart();

            coin.addSpriteReuse(coin2);
            coin.addSpriteReuse(coin1);

            enemy1 = new Sprite3(false, Game1.texEnemy1Stay, 550, -100);
            enemy1.setHSoffset(new Vector2(enemy1.getWidth() / 2, enemy1.getHeight() / 2));
            enemy1.setBB(20, 15, 75, 90);
            enemy1.boundingSphereRadius = 60;

            enemy2 = new Sprite3(false, Game1.texEnemy1Stay, 550, -100);
            enemy2.setHSoffset(new Vector2(enemy2.getWidth() / 2, enemy2.getHeight() / 2));
            enemy2.setBB(20, 15, 75, 90);
            enemy2.boundingSphereRadius = 60;

            enemy3 = new Sprite3(false, Game1.texEnemy1Stay, 550, -100);
            enemy3.setHSoffset(new Vector2(enemy2.getWidth() / 2, enemy2.getHeight() / 2));
            enemy3.setBB(20, 15, 75, 90);
            enemy3.boundingSphereRadius = 60;

            enemy4 = new Sprite3(false, Game1.texEnemy1Stay, 550, -100);
            enemy4.setHSoffset(new Vector2(enemy2.getWidth() / 2, enemy2.getHeight() / 2));
            enemy4.setBB(20, 15, 75, 90);
            enemy4.boundingSphereRadius = 60;

            Vector2[] fireAnim = new Vector2[5];
            fireAnim[0].X = 0; fireAnim[0].Y = 0;
            fireAnim[1].X = 1; fireAnim[1].Y = 0;
            fireAnim[2].X = 2; fireAnim[2].Y = 0;
            fireAnim[3].X = 3; fireAnim[3].Y = 0;
            fireAnim[4].X = 4; fireAnim[4].Y = 0;

            eFire = new Sprite3(false, Game1.texEFire, -100, 0);
            eFire.setXframes(5);
            eFire.setYframes(1);
            eFire.setWidthHeight(62, 70);
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
            eFire3.setWidthHeight(62, 70);
            eFire3.setAnimationSequence(fireAnim, 0, 4, 10);
            eFire3.setAnimFinished(0);
            eFire3.animationStart();

            eFire4 = new Sprite3(false, Game1.texEFire, -100, 0);
            eFire4.setXframes(5);
            eFire4.setYframes(1);
            eFire4.setWidthHeight(62, 70);
            eFire4.setAnimationSequence(fireAnim, 0, 4, 10);
            eFire4.setAnimFinished(0);
            eFire4.animationStart();

            enemy.addSpriteReuse(enemy1);
            enemy.addSpriteReuse(enemy2);
            enemy.addSpriteReuse(enemy3);
            enemy.addSpriteReuse(enemy4);

            boom.addSpriteReuse(turn);
            boom.addSpriteReuse(eFire);
            boom.addSpriteReuse(eFire2);
            boom.addSpriteReuse(eFire3);
            boom.addSpriteReuse(eFire4);

            bC = new SpriteList();
            bC.addSpriteReuse(main);
            bC.addSpriteReuse(enemy1);
            bC.addSpriteReuse(enemy2);
            bC.addSpriteReuse(enemy3);
            bC.addSpriteReuse(enemy4);
            bC.addSpriteReuse(ground);

            xVel = (80 - yAngle) * 0.1f;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (gameStart || Game1.startOver)
            {
                enemy1.setVisible(true);
                enemy2.setVisible(true);
                enemy3.setVisible(true);
                enemy4.setVisible(true);

                eFire.setVisible(false);
                eFire3.setVisible(false);
                eFire4.setVisible(false);
                eFire2.setVisible(false);

                enemy2.bossMoveAndDodge(new Vector2(250, 250), 5f, coin, pushBackDist);
                enemy1.bossMoveAndDodge(new Vector2(700, 500), 5f, coin, pushBackDist);
                enemy4.bossMoveAndDodge(new Vector2(200, 500), 5f, coin, pushBackDist);
                enemy3.bossMoveAndDodge(new Vector2(700, 180), 5f, coin, pushBackDist);

                Game1.score = 0;
                coin1.setVisible(true); coin2.setVisible(true);
                coin1.setPos(300,600);
                coin2.setPos(900, 500);
                hit = 0;              
                healthBar.currentHp = 100;
                bulletFired = false;
                takeInput = true;
                moveEnemy = false;
                fire = true;
                animChanged = false;
                drawString = false;
                tick = 0;
                enemyTick = 0;
                animTick = 0;
                enemyTurn = 1;
                endTick = 0;
                hit = 0;
                if (Game1.startOver) { startOver = true; }
            }

            tick++;
            limSound3.Play();

            eFire.setPos(enemy1.getPosX() - xFireOffset1, enemy1.getPosY() - yFireOffset1);
            eFire2.setPos(enemy2.getPosX() - xFireOffset2, enemy2.getPosY() - yFireOffset2);
            eFire3.setPos(enemy3.getPosX() - xFireOffset3, enemy3.getPosY() - yFireOffset3);
            eFire4.setPos(enemy4.getPosX() - xFireOffset3, enemy4.getPosY() - yFireOffset3);

            if (RC_GameStateParent.keyState.IsKeyDown(Keys.B) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.B)) { showBB = !showBB; }

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

            bullet.setPosX(main.getPosX() + main.getWidth() / 2 + ballOffset * 1.5f);
            bullet.setPosY(main.getPosY() + main.getHeight() / 2);

            powerStrenght = bar.getPosX() + bar.getWidth();
            bool shot = false;

            if (RC_GameStateParent.prevKeyState.IsKeyDown(Keys.Space) && fire)
            {
                b = 1;
                if (powerStrenght < 1195) { bar.setPosX(bar.getPosX() + 4); }
                animChanged = true;
                gameStart = false;
                Game1.startOver = false;
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
                enemy1Pos = new Vector2(rnd.Next(100, 500), rnd.Next(100, 350));
                enemy2Pos = new Vector2(rnd.Next(700, 1000), rnd.Next(100, 350));
                enemy3Pos = new Vector2(rnd.Next(100, 500), rnd.Next(400, 650));
                enemy4Pos = new Vector2(rnd.Next(700, 1000), rnd.Next(400, 650));
                moveEnemy = true;
                bulletFired = false;
                a = a * -1;
            }

            if (moveEnemy)
            {
                takeInput = false;
                fire = false;
                enemyTick++;

                if (enemyTurn == 1) { moveMinions(); }
                else if (enemyTurn == 2) { moveMinions(); }
                else if (enemyTurn == 3) { moveMinions(); }
                else if ((enemyTurn == 4)) { moveMinions(); }

                else if ((enemyTurn == 5))
                {
                    endTick++;
                    if (enemy1.getPosX() > main.getPosX()) { enemy1.setFlip(SpriteEffects.FlipHorizontally); xFireOffset1 = 0; } else { enemy1.setFlip(SpriteEffects.None); }
                    if (enemy2.getPosX() > main.getPosX()) { enemy2.setFlip(SpriteEffects.FlipHorizontally); xFireOffset2 = 0; } else { enemy2.setFlip(SpriteEffects.None); }
                    if (enemy3.getPosX() > main.getPosX()) { enemy3.setFlip(SpriteEffects.FlipHorizontally); xFireOffset3 = 0; } else { enemy3.setFlip(SpriteEffects.None); }
                    if (enemy4.getPosX() > main.getPosX()) { enemy4.setFlip(SpriteEffects.FlipHorizontally); xFireOffset4 = 0; } else { enemy4.setFlip(SpriteEffects.None); }

                    if (enemy1.getVisible() == true) { enemy1.moveTo(main.getPos(), 5f, false); }
                    if (enemy2.getVisible() == true) { enemy2.moveTo(main.getPos(), 5f, false); }
                    if (enemy3.getVisible() == true) { enemy3.moveTo(main.getPos(), 5f, false); }
                    if (enemy4.getVisible() == true) { enemy4.moveTo(main.getPos(), 5f, false); }

                    mainCollision();

                    if (healthBar.currentHp == 0)
                    {
                        main.setTexture(Game1.texDie, false);
                        main.setTickBetweenFrame(30);
                        main.setAnimFinished(0);
                        takeInput = false;
                        fire = false;
                        limSound3.Stop();
                        //if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; }
                        if (endTick > 350) { Game1.levelManager.pushLevel(9);}
                    }
                    else if (healthBar.currentHp != 0 && endTick > 350)                    
                    {
                        //if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; }
                        Game1.levelManager.pushLevel(2);
                        limSound3.Stop();
                    }
                }            
            }
            if (fire)
            {
                if (tick % 45 == 0)
                {
                    if (turn.visible == false) { turn.setVisible(true); } else turn.setVisible(false);
                }
            }

            if (hit == 4 && tick % 120 == 0) { Game1.levelManager.pushLevel(2); limSound3.Stop(); }
                //if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; } 
            
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

            main.animationTick(gameTime);
            coin.animationTick(gameTime);
            coin2.animationTick(gameTime);
            boom.animationTick(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            ground.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
            icon.Draw(spriteBatch);
            spriteBatch.DrawString(Font, "SCORE " + (Game1.score * 100).ToString(), new Vector2(500, 10), Color.LightGoldenrodYellow);
            spriteBatch.DrawString(Font, "BEST SCORE " + (Game1.highestScore * 100).ToString(), new Vector2(800, 10), Color.LightGoldenrodYellow);
            if (takeInput )
            {
                LineBatch.drawLine(spriteBatch, Color.White, lineRoot, linePeak);
            }

            main.Draw(spriteBatch);
            bullet.Draw(spriteBatch);
            enemy.Draw(spriteBatch);
            boom.Draw(spriteBatch);
            coin.Draw(spriteBatch);
            powerBar.Draw(spriteBatch);
            bar.Draw(spriteBatch);
            powerBar1.Draw(spriteBatch);
            power.Draw(spriteBatch);
            
            if (showBB) { showBoundingBoxes(); };

            spriteBatch.End();
        }

        public void moveMinions()
        {
            enemy1.setTexture(Game1.texEnemy1Move, false);
            enemy2.setTexture(Game1.texEnemy1Move, false);
            enemy3.setTexture(Game1.texEnemy1Move, false);
            enemy4.setTexture(Game1.texEnemy1Move, false);

            if (enemy1.getPosX() > enemy1Pos.X) { enemy1.setFlip(SpriteEffects.FlipHorizontally); } else { enemy1.setFlip(SpriteEffects.None); }
            if (enemy2.getPosX() > enemy2Pos.X) { enemy2.setFlip(SpriteEffects.FlipHorizontally); } else { enemy2.setFlip(SpriteEffects.None); }
            if (enemy3.getPosX() > enemy3Pos.X) { enemy3.setFlip(SpriteEffects.FlipHorizontally); } else { enemy3.setFlip(SpriteEffects.None); }
            if (enemy4.getPosX() > enemy4Pos.X) { enemy3.setFlip(SpriteEffects.FlipHorizontally); } else { enemy4.setFlip(SpriteEffects.None); }

            if (enemy1.getVisible() == true) { enemy1.bossMoveAndDodge(enemy1Pos, 5.5f, coin, pushBackDist); }
            if (enemy2.getVisible() == true) { enemy2.bossMoveAndDodge(enemy2Pos, 5.5f, coin, pushBackDist); }
            if (enemy3.getVisible() == true) { enemy3.bossMoveAndDodge(enemy3Pos, 5.5f, coin, pushBackDist); }
            if (enemy4.getVisible() == true) { enemy4.bossMoveAndDodge(enemy4Pos, 5.5f, coin, pushBackDist); }

            if (enemyTick % 100 == 0)
            {
                moveEnemy = false;
                enemy1.setTexture(Game1.texEnemy1Stay, false);
                enemy2.setTexture(Game1.texEnemy1Stay, false);
                enemy3.setTexture(Game1.texEnemy1Stay, false);
                enemy4.setTexture(Game1.texEnemy1Stay, false);
                enemy1.setFlip(SpriteEffects.None);
                enemy2.setFlip(SpriteEffects.None);
                enemy3.setFlip(SpriteEffects.None);
                enemy4.setFlip(SpriteEffects.None);
                takeInput = true;
                enemyTick = 0;
                fire = true;
                enemyTurn++;
                if (enemyTurn == 5) 
                {
                    xFireOffset1 = 62;
                    yFireOffset1 = 115;
                    xFireOffset2 = 62;
                    yFireOffset2 = 115;
                    xFireOffset3 = 62;
                    yFireOffset3 = 115;
                    xFireOffset4 = 62;
                    yFireOffset4 = 115;
                    eFire.setVisible(true);
                    eFire3.setVisible(true);
                    eFire4.setVisible(true);
                    eFire2.setVisible(true);
                }
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
            enemy3.drawHS(spriteBatch, Color.Red);
            enemy3.drawBB(spriteBatch, Color.White);
            enemy4.drawBB(spriteBatch, Color.White);
            enemy4.drawHS(spriteBatch, Color.Red);
            bullet.drawBB(spriteBatch, Color.White);
            bullet.drawHS(spriteBatch, Color.Red);
            ground.drawBB(spriteBatch, Color.White);
            ground.drawHS(spriteBatch, Color.Red);
            coin1.drawBB(spriteBatch, Color.White);
            coin1.drawHS(spriteBatch, Color.Red);
            coin2.drawBB(spriteBatch, Color.White);
            coin2.drawHS(spriteBatch, Color.Red);
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

        protected void bulletCollision()
        {
            Rectangle bulletBB = bullet.getBoundingBoxAA();
            int rc = enemy.collisionWithRect(bulletBB);
            bool gc = ground.collision(bullet);         
            int cc = coin.collisionWithRect(bulletBB);

            if (rc != -1)
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
                hit++;
            }
            else if (gc)
            {
                Rectangle rect = ground.Intersect(bulletBB);
                bulletExplosion(rect.X, rect.Y);
                bullet.setPosX(1300);
                bulletFired = true;
                limSound2.playSoundIfOk();
            }
            else if(cc != -1) 
            {
                Sprite3 temp2 = coin.getSprite(cc);
                temp2.setActive(false);
                temp2.setVisible(false);
                temp2.setPosX(1300);
                limSound1.playSoundIfOk();
                Game1.score++;
            }
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
                healthBar.currentHp -= 25;
                Game1.score -= 1;
                main.setTexture(Game1.texHurt, false);
                main.setTickBetweenFrame(8);
                limSound2.playSoundIfOk();            
            }
        }      
    }
}

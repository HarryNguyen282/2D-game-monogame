using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalAssignment
{
    class Intro: RC_GameStateParent
    {
        ImageBackground background =  null;
        Sprite3 main =  null;
        Sprite3 boss = null;
        Sprite3 enemy1 = null;
        Sprite3 enemy2 = null;
        Sprite3 enemy3 = null;

        int tick;
        bool drawString = false;
        bool play = true;
        SpriteFont Fonty;

        SpriteList spriteList = null;

        SoundEffect introSound = null;
        LimitSound limSound;

        public override void LoadContent()
        {
            Fonty = Content.Load<SpriteFont>("Fonty");
            introSound = Content.Load<SoundEffect>("introSound");
            limSound = new LimitSound(introSound, 1);
            background = new ImageBackground(Game1.texIntro, Color.White, graphicsDevice);

            main = new Sprite3(true, Game1.texMove, -100, 680);
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
            main.setAnimationSequence(anim, 0, 9, 2);
            main.setAnimFinished(0);
            main.animationStart();

            boss = new Sprite3(true, Game1.texBossMove, -500, 300);
            enemy1 = new Sprite3(true, Game1.texEnemy2Move, -700, 450);
            enemy2 = new Sprite3(true, Game1.texEnemy1Move, -500, 550);
            enemy3 = new Sprite3(true, Game1.texEnemy2Move, -300, 450);

            spriteList = new SpriteList();
            spriteList.addSpriteReuse(enemy1);
            spriteList.addSpriteReuse(enemy2);
            spriteList.addSpriteReuse(enemy3);
            spriteList.addSpriteReuse(boss);
            spriteList.addSpriteReuse(main);

            base.LoadContent(); 
        }
        public override void Update(GameTime gameTime)
        {
            tick++;

            if (play)
            {
                limSound.playSoundIfOk();
                play = false;
            }
            
            main.moveTo(new Vector2(1400, 680), 5f, false);
            enemy1.moveTo(new Vector2(1400, 450), 5f, false);
            enemy2.moveTo(new Vector2(1400, 500), 5f, false);
            enemy3.moveTo(new Vector2(1400, 450), 5f, false);
            boss.moveTo(new Vector2(1400, 300), 5f, false);

            if (tick > 420)
            {
                main.setTexture(Game1.texDefault, false);
                main.setTickBetweenFrame(10);
                main.setPos(new Vector2(900, 580));
                main.setFlip(Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally);
                boss.setPos(new Vector2(200, 200));
                enemy1.setPos(new Vector2(150, 400));
                enemy2.setPos(new Vector2(200, 600));
                drawString = true;
                if (RC_GameStateParent.keyState.IsKeyDown(Keys.Enter) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.Enter)) // ***
                {
                    Game1.levelManager.pushLevel(1);
                }
            }
            
            main.animationTick(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            if (drawString)
            {
                spriteBatch.DrawString(Fonty, "Press Enter to Begin", new Vector2(400, 420), Color.White);
            }
            spriteList.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

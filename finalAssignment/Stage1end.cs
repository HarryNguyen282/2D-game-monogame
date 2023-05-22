using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalAssignment
{
    class Stage1end: RC_GameStateParent
    {
        Sprite3 main = null;
        SpriteFont Font;
        ImageBackground background = null;
        Sprite3 boss = null;
        Sprite3 enemy1 = null;
        Sprite3 enemy2 = null;
        Sprite3 enemy3 = null;
        SpriteList spriteList = null;

        int tick;
        bool drawString = false;
        bool play = true;
        public override void LoadContent()
        {
            background = new ImageBackground(Game1.texIntro, Color.White, graphicsDevice);
            Font = Content.Load<SpriteFont>("Font");
            main = new Sprite3(true, Game1.texMove, 100, 680);
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

            boss = new Sprite3(true, Game1.texBossMove, -150, 300);
            enemy1 = new Sprite3(true, Game1.texEnemy2Move, -350, 450);
            enemy2 = new Sprite3(true, Game1.texEnemy1Move, -150, 550);
            enemy3 = new Sprite3(true, Game1.texEnemy2Move, -550, 450);

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
            if (play || Stage_1.startOver)
            {
                drawString = true;
                main.setPos(100, 680);
                enemy1.setPos(-350, 450);
                enemy1.setPos(-150, 550);
                enemy1.setPos(-550, 450);
                boss.setPos(-150, 300);
                tick++;
                if (tick < 180) { drawString = true; }
                else if (tick < 420)
                {
                    drawString = false;
                    main.moveTo(new Vector2(1400, 680), 8f, false);
                    enemy1.moveTo(new Vector2(1400, 450), 8f, false);
                    enemy2.moveTo(new Vector2(1400, 500), 8f, false);
                    enemy3.moveTo(new Vector2(1400, 450), 8f, false);
                    boss.moveTo(new Vector2(1400, 300), 8f, false);
                }
                else { Game1.levelManager.pushLevel(3); tick = 0;
                    play = false;
                }               
            }          
            main.animationTick(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            graphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            if (drawString)
            {
                spriteBatch.DrawString(Font, "Stage 2", new Vector2(500, 420), Color.White);
            }
            spriteList.Draw(spriteBatch);
            spriteBatch.End();

        }
    }
}

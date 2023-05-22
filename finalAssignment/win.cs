using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;

namespace finalAssignment
{
    class win : RC_GameStateParent
    {
        ImageBackground successScreen;
        SpriteFont Font;
        SpriteFont Fonty;
        int hScore;

        public override void LoadContent()
        {
            successScreen = new ImageBackground(Game1.texWin, Color.White, graphicsDevice);
            Fonty = Content.Load<SpriteFont>("Fonty");
            Font = Content.Load<SpriteFont>("Font");

        }

        public override void Update(GameTime gameTime)
        {

            if (Game1.score > Game1.highestScore) { Game1.highestScore = Game1.score; }
            if (RC_GameStateParent.keyState.IsKeyDown(Keys.D2) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.D2)) // ***
            {
                Game1.levelManager.setLevel(0);
                Game1.startOver = true;
            }           
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            successScreen.Draw(spriteBatch);
            spriteBatch.DrawString(Fonty, "CONGRATZ, YOU WON!", new Vector2(350, 250), Color.DarkBlue);
            spriteBatch.DrawString(Font, "SCORE: " + (Game1.score*100).ToString(), new Vector2(300, 400), Color.Black);
            spriteBatch.DrawString(Font, "HIGHEST SCORE: " + (Game1.highestScore*100).ToString(), new Vector2(600, 400), Color.Black);
            spriteBatch.DrawString(Font, "Press 2 to Play Again or Press ESC to exit", new Vector2(140, 520), Color.Black);
            spriteBatch.End();
        }
    }
}

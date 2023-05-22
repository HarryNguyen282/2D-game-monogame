using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RC_Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalAssignment
{
    class pause: RC_GameStateParent
    {

        ImageBackground pauseScreen = null;
        //ColorField trans;

        public override void LoadContent()
        {
            pauseScreen = new ImageBackground(Game1.texPause, Color.White, graphicsDevice);
            //trans = new ColorField(new Color(255, 255, 255, 100), new Rectangle(0, 0, 800, 600));
        }

        public override void Update(GameTime gameTime)
        {
            if (RC_GameStateParent.keyState.IsKeyDown(Keys.O) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.O))
            {
                Game1.levelManager.popLevel();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Game1.levelManager.prevStatePlayLevel.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            pauseScreen.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

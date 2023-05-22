using Microsoft.Xna.Framework;
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
    class help: RC_GameStateParent
    {
        ImageBackground background;
        public override void LoadContent()
        {
            background = new ImageBackground(Game1.texHelp, Color.White, graphicsDevice);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (RC_GameStateParent.keyState.IsKeyDown(Keys.F2) && RC_GameStateParent.prevKeyState.IsKeyUp(Keys.F2))
            {
                Game1.levelManager.popLevel();
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Game1.levelManager.prevStatePlayLevel.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            background.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}

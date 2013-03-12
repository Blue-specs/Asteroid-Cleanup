using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace shooter
{
    public class Hline : gameEntity
    {
        public override void Draw()
        {
            Game1.instance.spriteBatch.Draw(sprite, pos, Color.White);

        }
        public override void LoadContent()
        {
            sprite = Game1.instance.Content.Load<Texture2D>("Hline");
            pos.X = 0;
            pos.Y = 0;



        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.L) == true))
            {
                if (pos.X>=0)
                    pos.X -= timeDelta*250;
                
            }
            if ((keyState.IsKeyUp(Keys.L) == true))
            {
                if (pos.X<Game1.instance.screenwidth)
                pos.X += timeDelta*250;
            }

        }
    }
}

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
    public class Vline:gameEntity
    {
        public override void Draw()
        {
            Game1.instance.spriteBatch.Draw(sprite, pos, Color.White);
            
        }
        public override void LoadContent()
        {
            sprite = Game1.instance.Content.Load<Texture2D>("line");
            pos.X = 1;
            pos.Y = 1;
            
           
            
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            KeyboardState keyState = Keyboard.GetState();
            if ((keyState.IsKeyDown(Keys.A)==true))

            {
                if (pos.Y>=0)
                pos.Y-=timeDelta*250;
            }
            if ((keyState.IsKeyUp(Keys.A)==true))

            {
                if (pos.Y<Game1.instance.screenheight)
                pos.Y+=timeDelta *250;
            }
            
        }
    }
}

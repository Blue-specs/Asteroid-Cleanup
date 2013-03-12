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
    public class block : gameEntity
    {
        public bool gonleft;
        public SoundEffect rebound;
        

        public override void Draw()
        {
           
                Game1.instance.spriteBatch.Draw(sprite, pos, Color.White);

        }
        public override void LoadContent()
        {
            sprite = Game1.instance.Content.Load<Texture2D>("tank");
            rec = new Rectangle((int)pos.X - (sprite.Width / 2), (int)pos.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
            



        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            rec = new Rectangle((int)pos.X - (sprite.Width / 2), (int)pos.Y - (sprite.Height / 2), sprite.Width, sprite.Height);

            if ((gonleft == false))
            {
                pos.X += 1;
                if (pos.X >= (Game1.instance.screenwidth - sprite.Width))
                    gonleft = true;
            }

            else if (gonleft == true)
            {
                pos.X--;
                if (pos.X < 0)
                    gonleft = false;
            }



        }
    }
}

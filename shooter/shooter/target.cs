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
    public class target:gameEntity
    {
        public SoundEffect shoot;
        bool fired;
        
        public override void Draw()
        {
            
            Game1.instance.spriteBatch.Draw(sprite, pos, Color.White);
            
        }
        public override void LoadContent()
        {
            sprite = Game1.instance.Content.Load<Texture2D>("target1");
            shoot = Game1.instance.Content.Load<SoundEffect>("shoot");   
            
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();
            rec = new Rectangle((int)pos.X - (sprite.Width / 2), (int)pos.Y - (sprite.Height / 2), sprite.Width, sprite.Height);


            if ((keyState.IsKeyDown(Keys.Space) == true) && (fired == false))
            {


                shoot.Play();
                for (int i = 0; i < Game1.instance.asteroids.Count; i++)
                {
                    if ((Game1.instance.asteroids[i].rec.Contains((int)pos.X, (int)pos.Y))&&Game1.instance.weaponflag==1)
                    {
                        Game1.instance.asteroids[i].isalive = false;
                        Game1.instance.asteroids[i].explode.Play();
                        break;
                    }
                    else if ((Game1.instance.asteroids[i].rec.Intersects(rec))&&(Game1.instance.weaponflag==2)&&(Game1.instance.asteroids[i].mass<40))
                    {
                        Game1.instance.asteroids[i].isalive = false;
                        Game1.instance.asteroids[i].explode.Play();
                        break;
                    }
                }
                fired = true;
            }

                    
                       
                
            
            if (keyState.IsKeyDown(Keys.Space) == false)
                fired = false; 


            pos.X = Game1.instance.hline.pos.X- (sprite.Width / 2);
           
            pos.Y = Game1.instance.vline.pos.Y - (sprite.Height / 2);;
        }
    }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace shooter
{
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public static Game1 instance;
        public int screenheight;
        public int screenwidth;
        public Vline vline;
        public Hline hline;
        public target Target;
        public asteroid ast;
        public block b;
        public List<block> junk = new List<block>();
        public List<asteroid> asteroids = new List<asteroid>();
        public enum gamestate { main, level1, level2, gameover,level3,level4, lost };
        public int weaponflag=1;
        Texture2D weapon;
        int countdown;
        
        Random rand;
        SoundEffect change;
        bool drawn;
        float timer = 0;
        
        public SpriteFont arial;
        Texture2D background;
        public gamestate currentGameState = gamestate.main;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            instance = this;
            screenheight = GraphicsDevice.Viewport.Height;
            screenwidth = GraphicsDevice.Viewport.Width;
            base.Initialize();
        }
        public void redraw(int a)
        {
            Random rand = new Random();
            int x = rand.Next(0, screenwidth - (Game1.instance.ast.sprite.Width));
            int y = rand.Next(0, screenheight - (Game1.instance.ast.sprite.Height));
            asteroids[a].pos.X = x;
            asteroids[a].pos.Y = y;
           
        }
        public void deljunk() {
            for (int i = 0; i < junk.Count; i++)
            {
                junk.Remove(junk[i]);
            }
        }
        public void  drawjunk(int a){
            int x;
            int y;
            int alive;
            
            Random rand = new Random();
            for (int i = 0; i < a; i++)
            {

                b = new block();
                b.LoadContent();
                x = rand.Next(0, screenwidth - (Game1.instance.ast.sprite.Width));
                y = rand.Next(0, screenheight - (Game1.instance.ast.sprite.Height));

                alive = rand.Next(0, 2);
                b.pos.Y = y;
                b.pos.X = x;

                if (alive == 1)
                    b.gonleft = true;
                else
                    b.gonleft = false;
                junk.Add(b);
            }
        
        }
        public void drawroids(int a)
        {
            int x;
            int y;
            int alive;
            int m;
            Random rand = new Random();
            for (int i = 0; i < a; i++)
            {
                       
                ast = new asteroid();
                ast.LoadContent();
                x = rand.Next(0, screenwidth - (Game1.instance.ast.sprite.Width ));
                y = rand.Next(0, screenheight - (Game1.instance.ast.sprite.Height));
                m = rand.Next(1, 100);
                alive = rand.Next(0, 2);
                ast.pos.Y = y;
                ast.pos.X = x;
                ast.mass = m;
                if (alive == 1)
                    ast.gonleft = true;
                else
                    ast.gonleft = false;
                asteroids.Add(ast);
            }
            
            /*for (int i = 0; i < asteroids.Count; i++) {
                for (int j = 0; j < asteroids.Count; j++) {
                    if (asteroids[i].rec.Intersects(asteroids[j].rec))
                        redraw(i);
                }
              }
             * */
            
             
            
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arial = Content.Load<SpriteFont>("arial");
            weapon= Content.Load<Texture2D>("weapon");
            change = Game1.instance.Content.Load<SoundEffect>("change");   
            
            
            vline = new Vline();
            vline.LoadContent();

            
                background = Content.Load<Texture2D>("space");
            
            hline = new Hline();
            hline.LoadContent();
            Target = new target();
            Target.LoadContent();
        

            // Create a new SpriteBatch, which can be used to draw textures.
            

            // TODO: use this.Content to load your game content here
        }

        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        { 
            // Allows the game to exit
            
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Escape) )
                this.Exit();
            
            float timeDelta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            switch (currentGameState)
            {
                case gamestate.main:
                    if (keyState.IsKeyDown(Keys.Enter))
                        currentGameState = gamestate.level4;
                    
                    break;
                case gamestate.level1:
                    if (drawn == false)
                    {
                        drawroids(5);
                        drawn = true;
                        
                    }
                    timer += timeDelta;
                    countdown = (int)(300000f-timer);
                        vline.Update(gameTime);
                        hline.Update(gameTime);
                        Target.Update(gameTime);
                    for (int i = 0; i < asteroids.Count; i++)
                    {
                        asteroids[i].Update(gameTime);
                        if (asteroids[i].isalive == false)
                            asteroids.Remove(asteroids[i]);
                        
                        }
                    
                    if ((countdown < 0))
                    {
                        currentGameState = gamestate.gameover;
                    }
                       
                        if (asteroids.Count==0){
                        
                        currentGameState = gamestate.level2;
                        drawn = false;
                        timer = 0;
                    }
                        break;
                case gamestate.level2:
                        if (drawn == false)
                        {
                            drawroids(10);
                            drawn = true;
                            background = Content.Load<Texture2D>("level3");
                            for (int i = 0; i < asteroids.Count; i++)
                            {
                                if (asteroids[i].mass > 50)
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("mass1");
                                }

                                else
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("asteroid");
                                }
                            }
                        }
                        
                        
                         timer += timeDelta;
                    countdown = (int)(30000f-timer);
                        vline.Update(gameTime);
                        hline.Update(gameTime);
                        Target.Update(gameTime);
                    for (int i = 0; i < asteroids.Count; i++)
                    {
                        asteroids[i].Update(gameTime);
                        if (asteroids[i].isalive == false)
                            asteroids.Remove(asteroids[i]);
                        
                        } 
                        if ((countdown < 0))
                        currentGameState = gamestate.gameover;
                        
                       
                        if ((asteroids.Count==0)){
                        
                        currentGameState = gamestate.level3;
                        drawn = false;
                        timer = 0;
                    }
                        break;
                case gamestate.level3:
                    if (keyState.IsKeyDown(Keys.F1)){
                        change.Play();
                    weaponflag = 1;
                    }
                    if (keyState.IsKeyDown(Keys.F2))

                    {
                    weaponflag = 2;
                    change.Play();
                    }
                
                    if (drawn == false)
                        {
                            drawroids(15);
                            drawn = true;
                            for (int i = 0; i < asteroids.Count; i++)
                            {
                                if (asteroids[i].mass > 89)
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("mass1");
                                }
                                else if (asteroids[i].mass > 40)
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("asteroid");
                                }
                                else
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("mass2");
                                }
                            }
                        }
                        timer += timeDelta;
                        countdown = (int)(300000f - timer);
                        vline.Update(gameTime);
                        hline.Update(gameTime);
                        Target.Update(gameTime);
                        for (int i = 0; i < asteroids.Count; i++)
                        {
                            asteroids[i].Update(gameTime);
                            if (asteroids[i].isalive == false)
                                asteroids.Remove(asteroids[i]);

                        }
                        if ((asteroids.Count == 0))
                        {
                            currentGameState = gamestate.level4;
                            drawn = false;
                            timer = 0;
                        }
                        break;

                case gamestate.level4:
                        if (keyState.IsKeyDown(Keys.F1))
                        {
                            change.Play();
                            weaponflag = 1;
                        }
                        if (keyState.IsKeyDown(Keys.F2))
                        {
                            weaponflag = 2;
                            change.Play();
                        }

                        if (drawn == false)
                        {
                            drawroids(20);
                            drawjunk(8);
                            drawn = true;
                            for (int i = 0; i < asteroids.Count; i++)
                            {
                                if (asteroids[i].mass > 89)
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("mass1");
                                }
                                else if (asteroids[i].mass > 40)
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("asteroid");
                                }
                                else
                                {
                                    asteroids[i].sprite = Content.Load<Texture2D>("mass2");
                                }
                            }
                        }
                        timer += timeDelta;
                        countdown = (int)(300000f - timer);
                        vline.Update(gameTime);
                        hline.Update(gameTime);
                        Target.Update(gameTime);
                        for (int i = 0; i < asteroids.Count; i++)
                        {
                            asteroids[i].Update(gameTime);
                            if (asteroids[i].isalive == false)
                                asteroids.Remove(asteroids[i]);

                        }
                        for (int i = 0; i < junk.Count; i++)
                        {
                            junk[i].Update(gameTime);
                        }

                        if ((asteroids.Count == 0))
                            deljunk();
                            
                        break;
                
                case gamestate.gameover:
                        this.Exit();
                  
                        break;

            }//end case
            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            switch (currentGameState){
                case gamestate.main:
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque);
            
           
            spriteBatch.Draw(background, new Rectangle(0, 0, screenwidth, screenheight), Color.White);
            spriteBatch.End();
                break;
           case gamestate.level1:
           GraphicsDevice.Clear(Color.White);
           
           spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
           
           spriteBatch.Draw(background, new Rectangle(0, 0, screenwidth, screenheight), Color.White);
           

           spriteBatch.DrawString(arial, ("Asteroids left " + asteroids.Count), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(arial, ("Time left " +countdown.ToString()), new Vector2((screenwidth-200),10), Color.Red);
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            vline.Draw();
            hline.Draw();
            Target.Draw();
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Draw();
            }
            spriteBatch.End();
            break;
           case gamestate.level2:
           GraphicsDevice.Clear(Color.White);
           
           spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            
           spriteBatch.Draw(background, new Rectangle(0, 0, screenwidth, screenheight), Color.White);
           spriteBatch.DrawString(arial, ("Asteroids left " + asteroids.Count), new Vector2(10, 10), Color.Red);
           spriteBatch.DrawString(arial, ("Time left " + countdown.ToString()), new Vector2((screenwidth - 100), 10), Color.Red);
             
           spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            vline.Draw();
            hline.Draw();
            Target.Draw();
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Draw();
            }
            spriteBatch.End();
           
            
           
            break;

           case gamestate.level3:
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(background, new Rectangle(0, 0, screenwidth, screenheight), Color.White);
           if (weaponflag==1)
           spriteBatch.Draw(weapon, (new Vector2(screenheight-300, 10)), Color.Blue);
           if(weaponflag ==2)
               spriteBatch.Draw(weapon, (new Vector2(screenheight - 300, 10)), Color.Red);
            spriteBatch.DrawString(arial, ("Asteroids left " + asteroids.Count), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(arial, ("Time left " + countdown.ToString()), new Vector2((screenwidth - 100), 10), Color.Red);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            vline.Draw();
            hline.Draw();
            Target.Draw();
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Draw();
            }
            spriteBatch.End();



            break;

           case gamestate.level4:
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            spriteBatch.Draw(background, new Rectangle(0, 0, screenwidth, screenheight), Color.White);
            if (weaponflag == 1)
                spriteBatch.Draw(weapon, (new Vector2(screenheight - 300, 10)), Color.Blue);
            if (weaponflag == 2)
                spriteBatch.Draw(weapon, (new Vector2(screenheight - 300, 10)), Color.Red);
            spriteBatch.DrawString(arial, ("Asteroids left " + asteroids.Count), new Vector2(10, 10), Color.Red);
            spriteBatch.DrawString(arial, ("Time left " + countdown.ToString()), new Vector2((screenwidth - 100), 10), Color.Red);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            vline.Draw();
            hline.Draw();
            Target.Draw();
            for (int i = 0; i < junk.Count; i++)
            {
                junk[i].Draw();
            }
            for (int i = 0; i < asteroids.Count; i++)
            {
                asteroids[i].Draw();
            }
            
            spriteBatch.End();



            break;
            
           
           
            
            
            }
            
           

            // TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}

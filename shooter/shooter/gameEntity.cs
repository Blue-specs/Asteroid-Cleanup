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
    public abstract class gameEntity
    {
        public Rectangle rec;
        public Vector2 pos;
        public Texture2D sprite; //needs to have property
        public Vector2 look;
        public float rot = 0;
        public bool isalive = true; //needs to have property
        public Vector2 center = new Vector2();
        //add bounding box using sprite proterty
        public abstract void Update(GameTime gameTime);
        public abstract void LoadContent();
        public abstract void Draw();

    }

}

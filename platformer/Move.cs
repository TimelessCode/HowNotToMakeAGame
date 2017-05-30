using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitsforGames.Engine;

namespace ZeldaLikeSpace
{
    class Move : IComponent
    {

       
        public override string name { get;set;}
        public Rectangle bb;
        public Vector2 bbl;
        public Vector2 size;
        public Vector2 pos;
        public bool canmove;
        public Vector2 vel;
        

        public void Change(float x,float y) {/*bbl = pos;*/

                vel = new Vector2( MathHelper.Clamp( vel.X + x,-10,10), MathHelper.Clamp(vel.Y + y, -10, 10));
            }
            

        public Rectangle getr() {
            
            bb = new Rectangle((int)pos.X, (int)pos.Y + 8, (int)size.X, (int)size.Y);

            
            return bb;
                }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitsforGames.Engine;

namespace ZeldaLikeSpace
{
    class DrawPlayer
    {
        public void draw(Entity player, SpriteBatch sb, Animation source)
        {

            Move m = (Move)player.getComponent("move");

            if (m != null && player != null)
            {
                sb.Draw(player.tex, new Rectangle((int)m.pos.X, (int)m.pos.Y, (int)player.size.X, (int)player.size.Y), source.rtngl[source.frame], Color.White);
 
                
            }
        }


    }
}

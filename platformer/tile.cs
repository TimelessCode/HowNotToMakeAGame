using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLikeSpace
{
   public class tile
    {
        public tile(Rectangle rf,type t) { r = rf;mtype = t; }

       public Rectangle r;
      public  enum type
        {
            door,
            norm,
            enemy,
        }

        public type mtype;
    }
}

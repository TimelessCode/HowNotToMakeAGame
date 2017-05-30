using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaLikeSpace
{
    class Animation
    {
     public   Rectangle[] rtngl;

       public int frame = 0;
        public int tf = 2;

        public void inc() {  if (frame < tf-1) { frame++; } else { frame = 0; } }

    }
}

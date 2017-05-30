using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitsforGames.Engine;

namespace ZeldaLikeSpace
{
    class DirSys : IComponent
    {
        public override string name { get; set; }
       public enum dir
        {
            up,
            down,
            left,
            right
        }

        public dir d = dir.right;

        public string getdir(Entity e) { return e.name + d.ToString(); }
    }
}

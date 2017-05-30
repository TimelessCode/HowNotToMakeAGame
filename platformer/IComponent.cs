using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitsforGames.Engine
{
    abstract class IComponent
    {
        Entity owner;
        abstract public String name { get; set; }
       

    }
}

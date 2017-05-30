using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitsforGames.Engine
{
    class Entity
    {
        public Texture2D tex;
        public Vector2 size;
        
        public String name;
      public void addComponent(IComponent c) { comp.Add(c); }
      public IComponent getComponent(string wow) { return comp.FirstOrDefault(k => k.name == wow); }
        public void removeComponent(string wow) { comp.Remove(comp.FirstOrDefault(k => k.name == wow)); }

    

     public List<IComponent> comp = new List<IComponent>();

      

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilitsforGames.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ZeldaLikeSpace
{
  static class Controller
    {
        
        private  static List<Entity> elist = new List<Entity>();
        public static void subscribe(Entity e, Vector2 sizze) { elist.Add(e); if (e.getComponent("move") == null) { Move m = new Move(); m.vel = new Vector2(0,0); m.pos = new Vector2(300, 200); m.size = sizze; ; m.name = "move"; e.addComponent(m); } }

        public static bool glue = false;

        public static void collision(List<Rectangle> r, List<Entity> ef)
        {
            foreach (Entity e in ef)
            {Move m = (Move)e.getComponent("move");
                foreach (Rectangle rt in r)
                {


                    
                    if (rt.Intersects(m.getr())) { Console.WriteLine("Collision"); }
                    
                    
                }
            }
        }

        public static void playercollision(List<tile> r, Entity e, TiledSharp.TmxMap curtmxlevel,Action<TiledSharp.TmxMap> nextlevel)
        {
                Move m = (Move)e.getComponent("move");
            foreach (tile t in r.ToList())
            {
                

                Rectangle rt = t.r;

                if (m.getr().Intersects(rt))
                {
                   Vector2 depth = RectExtensions.GetIntersectionDepth(rt, m.getr());

                    if (m.vel.Y > 0) { if ((m.pos.Y + m.size.Y )<rt.Top+10) { m.pos = new Vector2(m.pos.X, m.pos.Y - depth.Y); m.vel.Y = 0; } }

                    if (m.vel.X > 0) { if ((m.pos.X + m.size.X) < rt.Left + 10) { m.pos = new Vector2(m.pos.X - depth.X, m.pos.Y); m.vel.X = 0; } }

                    if (t.mtype == tile.type.door)
                    {
                        
                            Console.WriteLine("Frick Nu Level1");
                            m.pos = Vector2.Zero;
                            nextlevel(curtmxlevel);
                            Console.WriteLine(curtmxlevel.Width);

                            curlevel.iter = 0;
                       
                    }

                }
                else {
                    m.canmove = true;
                }
               
                  


                    


                
            }
        }

        public static void rectcollision(List<Rectangle> r,Rectangle m)
        {

            foreach (Rectangle rt in r)
            {




                if (m.Intersects(rt)) { Console.WriteLine("Unreliable"); } else { }


            }
           
        }


        public static void Control(Entity eff)
        {
            foreach (Entity e in elist)
            {
                DirSys d = null;
                if(e.getComponent("dir") != null)
                {
                    d = (DirSys)e.getComponent("dir");

                }


                Move m = (Move)e.getComponent("move");
                KeyboardState i = Keyboard.GetState();
               

                m.pos += m.vel;

                if (!i.IsKeyDown(Keys.Right) && !i.IsKeyDown(Keys.Left)) { m.vel = new Vector2(0,m.vel.Y); }
                if (!i.IsKeyDown(Keys.Up) && !i.IsKeyDown(Keys.Down)) { m.vel = new Vector2( m.vel.X,0); }

                if (i.IsKeyDown(Keys.Up)) {

                 
                    m.Change(0, -1f);


                    

                }

                if (i.IsKeyDown(Keys.Down))
                {
                   
                    m.Change(0, 1);
                   


                }

                if (i.IsKeyDown(Keys.Left))
                {
                    m.Change(-1, 0);
                 
                    d.d = DirSys.dir.left;

                }

                if (i.IsKeyDown(Keys.Right))
                {
                    
                    m.Change(1, 0);


                    d.d = DirSys.dir.right;
                }
            }
        }


    

    }
}

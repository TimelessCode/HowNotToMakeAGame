using FlightOfTheElements;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UtilitsforGames.Engine;
using TiledSharp;


namespace ZeldaLikeSpace
{

    public static class curlevel{

        public static List<tile> rl = new List<tile>();
        public static string name = "";
        public static int iter = 1;

        public static Action a;



    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {



        private FrameCounter _frameCounter = new FrameCounter();

        TmxMap map = new TmxMap("Data/TestMap.tmx");
        Texture2D tileset;
        

        float timer;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int frame = 0;
       public static int gtimer;
        Dictionary<String, Animation> anims = new Dictionary<string, Animation>();
        Entity hero;
     
        Camera c = new Camera();
        public Texture2D cr;
        DrawPlayer dr = new DrawPlayer();

         List<Entity> rivals = new List<Entity>();

        public Texture2D rect2;


        List<Entity> world;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



         string getdir(Entity e) { DirSys ds = (DirSys)e.getComponent("dir");
            return ds.getdir(e);

        }




        public Rectangle[] makerect(int tiles,int index,int h,Texture2D image)
        {
            Rectangle[] r;
           r = new Rectangle[tiles];

            for (int i = 0; i < tiles; i++)
            {

                r[i] = new Rectangle(image.Width/tiles*i, h * index, (image.Width / tiles), h);
                //++ ,X Y ,Width, Height
              
            }
            return r;
            

        }




       

        public void loaddata(string path) {
            StreamReader s = new StreamReader(path);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(s.ReadToEnd());

            XmlNodeList xnl = xml.SelectNodes("/Anims");

            foreach(XmlNode xn in xnl)
            {
                foreach(XmlNode h in xn)
                {
                    Animation a = new Animation();
                   
                    String name = "";
                    int index = 0;
                    int tiles = 0;
                    int hj = 0;
                    string image = "";
                    Texture2D tx2;
                    for (int i = 0; i < h.ChildNodes.Count; i++)
                    {

                        switch (h.ChildNodes[i].Name)
                        {
                            case "tf":
                                a.tf = Int32.Parse(  h.ChildNodes[i].InnerText);
                                
                                    break;

                            case "name":
                                name = h.ChildNodes[i].InnerText;
                               
                                break;

                            case "tiles":
                                tiles = Int32.Parse(h.ChildNodes[i].InnerText);
                                break;

                            case "index":
                                index = Int32.Parse(h.ChildNodes[i].InnerText);
                            
                                break;

                            case "h":
                                hj = Int32.Parse(h.ChildNodes[i].InnerText);
                               
                                break;


                            case "image":
                                image = h.ChildNodes[i].InnerText;
                                Console.WriteLine(h.ChildNodes[i].InnerText);
                                break;

                        }
                        //end switch
                        if (image != "")
                        {
                            tx2 = Content.Load<Texture2D>(image);
                            a.rtngl = makerect(tiles, index, hj, tx2);
                            anims.Add(name, a);
                        }
                    }
                     //end animloop


                }

            }


          
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        public void loadtiles() { }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1024;
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            world = new List<Entity>();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
         
         cr = Content.Load<Texture2D>("TestAnim");
            hero = new Entity();
            hero.name = "Hero";
         Controller.subscribe(hero,new Vector2(100,100));
         
            

            c.Position = new Vector2(0, 0);
            c.Zoom = 1;
            c.Rotation = 0;
            c.Origin = new Vector2(0, 0);

           DirSys ds = new DirSys();
            ds.name = "dir";
            hero.addComponent(ds);
            hero.size = new Vector2(128, 135);
            hero.tex = cr;


            loaddata("Data/Test.xml");
            tileset = Content.Load<Texture2D>("tile");


            drawtiile(8, 64, map);


            loaddata("Data/Ghost.xml");


            // TODO: use this.Content to load your game content here
        }



        public void nextlevel(TmxMap level)
        {

            TmxMap loclevel = level;
            map = new TmxMap("Data/level1.tmx");
            drawtiile(8, 64, map);
       /*     if (level == loclevel)
            {


                throw  new NotImplementedException("It really isn't changing the input");
            }*/

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Controller.playercollision(curlevel.rl, hero,map,nextlevel);
            Controller.Control(hero);
            Controller.playercollision(curlevel.rl, hero,map,nextlevel);



            Move m =(Move)hero.getComponent("move");
            c.Position = Vector2.Lerp(c.Position, new Vector2(m.pos.X - GraphicsDevice.Viewport.Width/2,m.pos.Y - GraphicsDevice.Viewport.Height / 1.5f), 0.1f);


            
            // TODO: Add your update logic here
            if(timer < 1) { timer += (float)gameTime.ElapsedGameTime.TotalSeconds; }
            if(timer >= 1) { 
                foreach(Animation a in anims.Values) { a.inc(); }
                timer = 0;
              
            }
          
        
          
        

        
           
            base.Update(gameTime);
        }

        public void loadent(TmxMap map) { }

        public void drawtile(int tilesetsize,int x2,Texture2D tileset,TmxMap map)
        {
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;
               
                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {
               
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetsize;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetsize);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(x2 * column, x2 * row, x2, x2);

                

                    spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, x2, x2), tilesetRec, Color.White);
                }
            }
        }



        public void drawtiile(int tilesetsize, int x2, TmxMap map)
        {
            curlevel.rl.Clear();
            for (var i = 0; i < map.Layers[1].Tiles.Count; i++)
            {
                //see layers interator if confused
                int gid = map.Layers[1].Tiles[i].Gid;
                if (gid == 0)
                {

                }
                else
                {
                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;


                    curlevel.rl.Add(new tile(new Rectangle((int)x, (int)y, x2, x2
                         ), tile.type.door)
                         );
                }

            }

            
            
            for (var i = 0; i < map.Layers[2].Tiles.Count; i++)
            {
                int gid = map.Layers[2].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {

                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetsize;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetsize);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(x2 * column, x2 * row, x2, x2);
                    String s = map.ObjectGroups["EnemyAreCool"].Objects["gol"].Type;
                    //supposed to be s but for testing it's something else.
                    Entity riv = new Entity();
                    
                    Move rivpos = new Move();
                    rivpos.canmove = true;
                    rivpos.name = "move";
                    rivpos.pos = new Vector2((float)map.ObjectGroups["EnemyAreCool"].Objects["gol"].X, (float)map.ObjectGroups["EnemyAreCool"].Objects["gol"].Y);
                    riv.addComponent(rivpos);
                    addenemy("green",riv);
               

                   

                    // Console.WriteLine("  TTTTT  " + x + " " + y + " " + x2);




                }
            }
            
            for (var i = 0; i < map.Layers[0].Tiles.Count; i++)
            {
                int gid = map.Layers[0].Tiles[i].Gid;

                // Empty tile, do nothing
                if (gid == 0)
                {

                }
                else
                {

                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetsize;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetsize);

                    float x = (i % map.Width) * map.TileWidth;
                    float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;
                              
                    Rectangle tilesetRec = new Rectangle(x2 * column, x2 * row, x2, x2);


                        curlevel.rl.Add(new tile(new Rectangle((int)x, (int)y, x2, x2
                             ), tile.type.norm)
                             );
                    
                    // Console.WriteLine("  TTTTT  " + x + " " + y + " " + x2);


                  

                }
            }
        }

         void addenemy(string type,Entity en)
        {
            en.tex = Content.Load<Texture2D>("GhoulTry");
            en.size = new Vector2(200, 200);
            rivals.Add(en);
           

        }

        public void drawenemy(string type)
        {
            foreach(Entity e in rivals)
            {
                Move m = (Move)e.getComponent("move");
               switch(type)
                {
                    case "green":

                        spriteBatch.Draw(e.tex, new Rectangle((int)m.pos.X, (int)m.pos.Y, (int)e.size.X, (int)e.size.Y),anims["Ghoulleft"].rtngl[anims["Ghoulleft"].frame], Color.White);
                        break;



                }
            }


        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var viewMatrix = c.GetViewMatrix();
            //  spriteBatch.Begin(transformMatrix: viewMatrix);
            spriteBatch.Begin(transformMatrix: viewMatrix,samplerState:SamplerState.PointClamp);
            dr.draw(hero, spriteBatch,anims[getdir(hero)] );

            drawenemy("green");


            foreach (Entity e in world)
            {
                if(anims[getdir(e)] != null)
                {

//TODO get the animation display it
                }

            }
            // TODO: Add your drawing code here
            drawtile(8, 64, tileset, map);



            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _frameCounter.Update(deltaTime);

            var fps = string.Format("FPS: {0}", _frameCounter.AverageFramesPerSecond);
            spriteBatch.DrawString(Content.Load<SpriteFont>("ArialFont"), fps, new Vector2(1, 1), Color.Black);





            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MittSpel_Sebastian
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // variabler
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D myship;
        Vector2 myship_pos;
        Vector2 myship_speed;
        Texture2D coin;
        Texture2D healthpack;
        Texture2D dragon;
        Vector2 dragonPos;
        Vector2 dragon_speed;
        Vector2 coin_pos;
        Texture2D tripod;
        Texture2D bullet;
        Vector2 tripod_pos;
        Vector2 tripod_speed;
        Rectangle rec_myship;
        Rectangle rec_coin;
        Rectangle rec_tripod;
        Rectangle rec_bullet;
        bool hit;
        SoundEffect myshout;
        bool shout = false;
        bool shoot = false;
        SpriteFont gameFont;
        int health = 100;
        int fire_rate = 0;
        int poang = 0;
        Vector2 bullet_speed;

        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
    
        List<Vector2> ammo = new List<Vector2>();
        List<Vector2> healthpacks_pos = new List<Vector2>();
        List<Rectangle> healthpack_hitbox = new List<Rectangle>();
        List<Rectangle> bullets_col = new List<Rectangle>();
        SpriteEffect minEffekt; 


        // Funktion som kontrollerar kollision mellan 2 objekt
        public bool CheckCollision(Rectangle player, Rectangle mynt)
        {
            return player.Intersects(mynt);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            int windowWidth = Window.ClientBounds.Width;
            int windowHeight = Window.ClientBounds.Height;

            myship_pos.X = windowWidth / 2;
            myship_pos.Y = windowHeight - 100;
            myship_speed.X = 5f;
            myship_speed.Y = 5f;
            // tripod_speed.X = 1f;
            tripod_speed.Y = 1f;

            bullet_speed.Y = -5f;

            
            Random random = new Random();

            dragonPos.X = random.Next(0, windowWidth - 64);
            dragonPos.Y = random.Next(0, windowHeight / 2);

            for (int i = 0; i < 5; i++)
            {
                coin_pos.X = random.Next(0, windowWidth - 50);
                coin_pos.Y = random.Next(0, windowHeight - 50);
                coin_pos_list.Add(coin_pos);
            }

            for (int i = 0; i < 5; i++)
            {
                tripod_pos.X = random.Next(0, windowWidth - 50);
                tripod_pos.Y = random.Next(0, windowHeight / 2);
                tripod_pos_list.Add(tripod_pos);
            }

            for (int i = 0; i < 3; i++)
            {
                ammo.Add(myship_pos);
            }

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myship = Content.Load<Texture2D>("ship");
            coin = Content.Load<Texture2D>("Sprites/coin");
            tripod = Content.Load<Texture2D>("Sprites/tripod");
            myshout = Content.Load<SoundEffect>("Sounds/yehaw");
            bullet = Content.Load<Texture2D>("Sprites/bullet");
            gameFont = Content.Load<SpriteFont>("Utskrift/gamefont");
            dragon = Content.Load<Texture2D>("Sprites/dragon");
            healthpack = Content.Load<Texture2D>("Sprites/Healthpack");
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

            // TODO: Add your update logic here
            int windowWidth = Window.ClientBounds.Width;
            int windowHeight = Window.ClientBounds.Height;
            /*
            if (myship_pos.X > windowWidth - myship.Width || myship_pos.X < 0)
            {
                myship_speed.X *= -1;
            }
            if (myship_pos.Y > windowHeight - myship.Height || myship_pos.Y  < 0)
            {
                myship_speed.Y*= -1;
            }*/
            //Begränsar skeppet till spelytan
            if (myship_pos.X > windowWidth - myship.Width)
            {
                myship_pos.X = windowWidth - myship.Width;
            }

            if (myship_pos.X < 0 )
            {
                myship_pos.X = 0;
            }
            if (myship_pos.Y > windowHeight - myship.Height)
            {
                myship_pos.Y = windowHeight - myship.Height;
            }

            if (myship_pos.Y < 0)
            {
                myship_pos.Y = 0;
            }
            // Spelar input

            KeyboardState Key = Keyboard.GetState();

            if (Key.IsKeyDown(Keys.Right))
            {
                myship_pos.X += myship_speed.X;
            }
            if (Key.IsKeyDown(Keys.Left))
            {
                myship_pos.X -= myship_speed.X;
            }
            if (Key.IsKeyDown(Keys.Up))
            {
                myship_pos.Y -= myship_speed.Y;
            }
            if (Key.IsKeyDown(Keys.Down))
            {
                myship_pos.Y += myship_speed.Y;
            }

            if (Key.IsKeyDown(Keys.Space))
            {

            }
            // myship_pos += myship_speed;
            for (int i = 0; i < tripod_pos_list.Count; i++)
            {
                
                tripod_pos_list[i] += tripod_speed;
                

                
                
            }
            // kollisioner
            rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X), Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);

            
            foreach(Vector2 cn in coin_pos_list.ToList())
            {
                rec_coin =new Rectangle(Convert.ToInt32(cn.X),Convert.ToInt32(cn.Y), coin.Width,coin.Height);
                hit = CheckCollision(rec_myship, rec_coin);

                

                
                if(hit==true)
                {
                    coin_pos_list.Remove(cn);
                    poang += 10;
                    hit = false;
                }
            }

            Random r = new Random();

            foreach (Vector2 en in tripod_pos_list.ToList())
            {
                rec_tripod = new Rectangle(Convert.ToInt32(en.X), Convert.ToInt32(en.Y), tripod.Width, tripod.Height);
                for (int i = 0; i < bullets_col.Count; i++)
                {
                    
                    hit = CheckCollision(bullets_col[i], rec_tripod);
                    
                    if (hit == true)
                    {
                        int dropHealth = 1;
                        int dropGen = r.Next(0, 10);
                        if (dropGen == dropHealth)
                        {
                            Vector2 hp_pos = new Vector2(en.X, en.Y);
                            Rectangle hp_rec = new Rectangle(Convert.ToInt32(en.X), Convert.ToInt32(en.Y), healthpack.Width, healthpack.Height);
                            healthpacks_pos.Add(hp_pos);
                            healthpack_hitbox.Add(hp_rec);

                        }
                        tripod_pos_list.Remove(en);
                        poang += 20;
                        hit = false;
                    }
                }
                if (en.Y > windowHeight)
                {
                    tripod_pos_list.Remove(en);
                }

            }

            foreach (Vector2 en in tripod_pos_list.ToList())
            {
                rec_tripod = new Rectangle(Convert.ToInt32(en.X), Convert.ToInt32(en.Y), tripod.Width, tripod.Height);
                hit = CheckCollision(rec_tripod, rec_myship);
                if (hit == true)
                {
                    tripod_pos_list.Remove(en);
                    health -= 20;
                    hit = false;
                }
            }

            if (coin_pos_list.Count == 0 && shout == false)
            {
                myshout.Play();
                shout = true;
            }
            // skjutkod
            if (Key.IsKeyDown(Keys.Space))
            {
                
            for (int i = 0; i < 3; i++)
            {
                    
                    if (fire_rate==0)
                    {

                        for (int g = 0; g < ammo.Count; g++)
                        {
                            Rectangle bullet_rec = new Rectangle(Convert.ToInt32(ammo[g].X), Convert.ToInt32(ammo[g].Y), bullet.Width, bullet.Height);
                            bullets_col.Add(bullet_rec);
                        }
                        fire_rate = 5;
                        ammo.Add(myship_pos);
                    }
                 


                
            }
            }

            for (int i = 0; i < ammo.Count; i++)
                {
                    ammo[i] += bullet_speed;

                     if (ammo[i].Y < 0)
                    {
                        ammo.RemoveAt(i);
                       
                    }

                

            }
            bullets_col.RemoveRange(0, bullets_col.Count);
            for (int i = 0; i < ammo.Count; i++)
            {
                Rectangle bullet_col = new Rectangle(Convert.ToInt32(ammo[i].X), Convert.ToInt32(ammo[i].Y), bullet.Width, bullet.Height);
                bullets_col.Add(bullet_col);
            }

            if (fire_rate!=0)
            {
                fire_rate--;
            }
            Random rand = new Random();
            if (tripod_pos_list.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                  

                    Vector2 newTripod = new Vector2();

                    newTripod.X = rand.Next(0, windowWidth - tripod.Width);
                    newTripod.Y = rand.Next(-200, 0);

                    tripod_pos_list.Add(newTripod);

                }
            }
            base.Update(gameTime);
           
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (health > 0)
            {
                spriteBatch.Draw(myship, myship_pos, Color.White);
            }
            else
            {
                spriteBatch.DrawString(gameFont, "Spelet är slut!", new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2), Color.White);
            }
            
            
            spriteBatch.DrawString(gameFont, "Poäng: " + poang, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(gameFont, "Liv: " + health, new Vector2(10, 30), Color.White);
            //  spriteBatch.Draw(dragon, dragonPos, null, Color.White, 0.0f, Vector2.Zero, 1f, minEffekt, 0);
            foreach (Vector2 c in coin_pos_list){
                spriteBatch.Draw(coin, c, Color.White);
            }

            foreach (Vector2 hp in healthpacks_pos)
            {
                spriteBatch.Draw(healthpack, hp, Color.White);
            }
            foreach (Vector2 t_pos in tripod_pos_list)
            {
                spriteBatch.Draw(tripod, t_pos, Color.White);
            }

            foreach (Vector2 bullets in ammo)
            {
                spriteBatch.Draw(bullet, bullets, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

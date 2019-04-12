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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D myship;
        Vector2 myship_pos;
        Vector2 myship_speed;
        Texture2D coin;
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

        Vector2 bullet_speed;

        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        List<Vector2> tripod_speed_list = new List<Vector2>();
        List<Vector2> ammo = new List<Vector2>();


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
            myship_speed.X = 2.5f;
            myship_speed.Y = 2.5f;
            // tripod_speed.X = 1f;
            tripod_speed.Y = 1f;

            bullet_speed.Y = -5f;

            for (int i = 0; i < 5; i++)
            {
                tripod_speed_list.Add(tripod_speed);
            }
            Random random = new Random();

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

            if (myship_pos.X > windowWidth - myship.Width || myship_pos.X < 0)
            {
                myship_speed.X = 0;
            }
            if (myship_pos.Y > windowHeight - myship.Height || myship_pos.Y < 0)
            {
                myship_speed.Y = 0;
            }

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
                
                tripod_pos_list[i] += tripod_speed_list[i];
                

                
                
            }

            rec_myship = new Rectangle(Convert.ToInt32(myship_pos.X), Convert.ToInt32(myship_pos.Y), myship.Width, myship.Height);

            
            foreach(Vector2 cn in coin_pos_list.ToList())
            {
                rec_bullet=new Rectangle(Convert.ToInt32(cn.X),Convert.ToInt32(cn.Y), coin.Width,coin.Height);
                hit = CheckCollision(rec_myship, rec_coin);
                if(hit==true)
                {
                    coin_pos_list.Remove(cn);
                    hit = false;
                }
            }



            foreach (Vector2 en in tripod_pos_list.ToList())
            {
                rec_bullet = new Rectangle(Convert.ToInt32(en.X), Convert.ToInt32(en.Y), bullet.Width, bullet.Height);
                
                hit = CheckCollision(rec_myship, rec_bullet);
                if (hit == true)
                {
                    tripod_pos_list.Remove(en);
                    hit = false;
                }
            }

            if (coin_pos_list.Count == 0 && shout == false)
            {
                myshout.Play();
                shout = true;
            }

            if (Key.IsKeyDown(Keys.Space))
            {
                
            for (int i = 0; i < 3; i++)
            {
                
                 ammo.Add(myship_pos);
                 

                
            }
            }

            for (int i = 0; i < ammo.Count; i++)
                {
                    ammo[i] += bullet_speed;
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
            spriteBatch.Draw(myship, myship_pos, Color.White);
            foreach(Vector2 c in coin_pos_list){
                spriteBatch.Draw(coin, c, Color.White);
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

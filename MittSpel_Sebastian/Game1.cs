using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            myship_pos.X = 100;
            myship_pos.Y = 100;
            myship_speed.X = 2.5f;
            myship_speed.Y = 2.5f;
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
            // myship_pos += myship_speed;
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
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

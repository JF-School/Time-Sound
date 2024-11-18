using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time___Sound
{
    public class Game1 : Game
    {
        Texture2D bombTexture, pliersTexture, boomTexture;
        Rectangle bombRect, pliersRect, window;
        SpriteFont timeFont;
        float seconds;
        MouseState mouseState;
        SoundEffect explode;
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            this.Window.Title = "bomb";

            bombRect = new Rectangle(50, 50, 700, 400);
            seconds = 16;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            pliersTexture = Content.Load<Texture2D>("pliers");
            boomTexture = Content.Load<Texture2D>("boom");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 16f;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

            seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (seconds <= 0)
            {
                seconds = 0f;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();

            if (seconds >= 0)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                if (seconds == 0)
                    _spriteBatch.Draw(boomTexture, bombRect, Color.White);
            }
			_spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);

            _spriteBatch.End();
        }
    }
}

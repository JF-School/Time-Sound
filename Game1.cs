using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Time___Sound
{
    public class Game1 : Game
    {
        Texture2D bombTexture, pliersTexture, boomTexture, trophyTexture;
        Rectangle bombRect, pliersRect, wiresRect, trophyRect, window;
        SpriteFont timeFont;
        float seconds;
        MouseState mouseState, prevMouseState;
        SoundEffect explode;
        SoundEffectInstance explodeInstance;
        enum Screen
        {
            Bomb,
            Explosion,
            Diffused
        }
        Screen screen;
        
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

            screen = Screen.Bomb;

            window = new Rectangle(0, 0, 800, 500);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            this.Window.Title = "PLEASE HELP ME DIFFUSE THE BOMB";

            bombRect = new Rectangle(50, 50, 700, 400);
            seconds = 16;

            wiresRect = new Rectangle(487, 157, 266, 105);
            pliersRect = new Rectangle(mouseState.X, mouseState.Y, 45, 45);
            trophyRect = new Rectangle(280, 175, 150, 150);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            pliersTexture = Content.Load<Texture2D>("pliers");
            boomTexture = Content.Load<Texture2D>("boom");
            trophyTexture = Content.Load<Texture2D>("trophy");
            timeFont = Content.Load<SpriteFont>("Time");
            explode = Content.Load<SoundEffect>("explosion");
            explodeInstance = explode.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            // wires switch to screen diffused
            if (screen == Screen.Bomb)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (wiresRect.Contains(mouseState.Position))
                        screen = Screen.Diffused;
                }
                seconds -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (seconds <= 0)
                {
                    screen = Screen.Explosion;
                    explodeInstance.Play();
                }
            }
            // code works on both bomb and diffused
            if (screen == Screen.Bomb || screen == Screen.Diffused)
            {
                if (wiresRect.Contains(mouseState.Position))
                {
                    pliersRect.X = mouseState.X;
                    pliersRect.Y = mouseState.Y;
                    IsMouseVisible = false;
                }
                if (!wiresRect.Contains(mouseState.Position))
                {
                    IsMouseVisible = true;
                }
            }
            else if (screen == Screen.Explosion)
            {
                if (explodeInstance.State == SoundState.Stopped)
                    Exit();
            }
            
            // this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);

            _spriteBatch.Begin();

            // screen bomb draws bomb, pliers (over wires), and the time
            if (screen == Screen.Bomb)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                if (wiresRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
                }
                _spriteBatch.DrawString(timeFont, seconds.ToString("00.0"), new Vector2(270, 200), Color.Black);
            }

            // screen explosion draws the explosion
            else if (screen == Screen.Explosion)
            {
                _spriteBatch.Draw(boomTexture, bombRect, Color.White);
                
            }

            // when pliers diffuse bomb, same screen but with no timer, and a trophy!
            else if (screen == Screen.Diffused)
            {
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                if (wiresRect.Contains(mouseState.Position))
                {
                    _spriteBatch.Draw(pliersTexture, pliersRect, Color.White);
                }
                _spriteBatch.Draw(trophyTexture, trophyRect, Color.White);
            }

            _spriteBatch.End();
        }
    }
}

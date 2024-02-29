using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Reflection.Metadata;

namespace PirategameUnleashed
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D seablip;
        private Texture2D landblip;
        private Texture2D background;
        private Texture2D borderblip;
        private SpriteFont systemFont;

        private int rowCount = 200;
        private int columnCount = 200;

        public GridHandler gridHandler;
        public Painterboy painterBoy;

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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("background");
            seablip = Content.Load<Texture2D>("seablip");
            landblip = Content.Load<Texture2D>("landblip");
            borderblip = Content.Load<Texture2D>("borderblip");
            systemFont = Content.Load<SpriteFont>("SystemFont");

            painterBoy = Painterboy.Instance;
            gridHandler = GridHandler.Instance;
            gridHandler.initializeGrid(rowCount, columnCount, seablip, landblip, borderblip, systemFont);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(background, new Rectangle(0, 0, 800, 500), Color.White);

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    gridHandler.getGrid()[i][j].Draw(gameTime, _spriteBatch);
                }
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
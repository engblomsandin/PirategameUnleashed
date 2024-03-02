using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace PirategameUnleashed
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private int columnCount = DataVariables.columnCount;
        private int rowCount = DataVariables.rowCount;
        

        public GridHandler gridHandler;
        public Painterboy painterBoy;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
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

            Texture2D background = Content.Load<Texture2D>("background");
            Texture2D seablip = Content.Load<Texture2D>("seablip");
            Texture2D landblip = Content.Load<Texture2D>("landblip");
            Texture2D cityblip = Content.Load<Texture2D>("cityblip");
            Texture2D companyblip = Content.Load<Texture2D>("companyblip");
            Texture2D shipblip = Content.Load<Texture2D>("shipblip");

            SpriteFont systemFont = Content.Load<SpriteFont>("SystemFont");

            List<Texture2D> blipList = new List<Texture2D>();
            blipList.Add(background);
            blipList.Add(seablip);
            blipList.Add(landblip);
            blipList.Add(cityblip);
            blipList.Add(companyblip);
            blipList.Add(shipblip);


            painterBoy = Painterboy.Instance;
            gridHandler = GridHandler.Instance;
            gridHandler.initializeGrid(rowCount, columnCount, blipList, systemFont);

            Queue<Blip> miniRoute = gridHandler.getRoute();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    gridHandler.getGrid()[i][j].Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            //_spriteBatch.Draw(background, new Rectangle(0, 0, 800, 500), Color.White);

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
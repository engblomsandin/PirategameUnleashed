using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{
    public class Blip
    {
        private int rowNumber = 0;
        private int columnNumber = 0;

        private int xPosition = 0;
        private int yPosition = 0;

        private bool navigatable = false;

        public event EventHandler LeftClick;
        public event EventHandler RightClick;

        private MouseState _currentMouse;
        private MouseState _previousMouse;

        private Texture2D seablip;
        private Texture2D landblip;
        private Texture2D cityblip;
        private Texture2D shipblip;
        private Texture2D companyblip;
        private SpriteFont systemFont;

        private blipType blipState;

        private GridHandler gridHandler = GridHandler.Instance;

        enum blipType
        {
            seablip,
            landblip,
            cityblip,
            companyblip,
            shipblip,
        }


        public Blip(int x, int y, List<Texture2D> blipList, SpriteFont systemFont, int rowCount, int columnCount)
        {
            this.xPosition = x;
            this.yPosition = y;
            this.rowNumber = y / DataVariables.tileHeight;
            this.columnNumber = x / DataVariables.tileWidth;

            this.seablip = blipList.ElementAt(1);
            this.landblip = blipList.ElementAt(2);
            this.cityblip = blipList.ElementAt(3);
            this.companyblip = blipList.ElementAt(4);
            this.shipblip = blipList.ElementAt(5);
            this.systemFont = systemFont;

            if (this.columnNumber <= 3 || this.columnNumber >= 60)
            {
                this.blipState = blipType.landblip;                
            }
            else
            {
                this.blipState = blipType.seablip;
                this.navigatable = true;
            }

            if (this.columnNumber == 3 && this.rowNumber == 15)
            {
                this.blipState = blipType.cityblip;
            }

            this.LeftClick += this.onLeftClick;
            this.RightClick += this.onRightClick;
        }

        public void onLeftClick(object sender, System.EventArgs e)
        {
            setState(blipType.companyblip);
        }

        public void onRightClick(object sender, System.EventArgs e)
        {
            //getRoute
        }

        public int getxPosition()
        {
            return this.xPosition;
        }
        public int getyPosition()
        {
            return this.yPosition;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.getBlipTexture(), new Rectangle(this.xPosition, this.yPosition, DataVariables.tileWidth, DataVariables.tileHeight), Color.White);
            //spriteBatch.DrawString(systemFont, columnNumber + "," + rowNumber, new Vector2(this.xPosition, this.yPosition), Color.Black);
        }


        private Texture2D getBlipTexture()
        {
            switch (this.blipState)
            {
                case blipType.seablip:
                    return this.seablip;

                case blipType.landblip:
                    return this.landblip;

                case blipType.cityblip:
                    return this.cityblip;

                case blipType.companyblip:
                    return this.companyblip;

                case blipType.shipblip:
                    return this.shipblip;

                default:
                    return this.seablip;
            }
        }

        private void setState(blipType type)
        {
            this.blipState = type;
        }
        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            if (mouseRectangle.Intersects(new Rectangle(this.xPosition, this.yPosition, DataVariables.tileWidth, DataVariables.tileHeight)))
            {
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    LeftClick?.Invoke(this, new EventArgs());
                }
                if (_currentMouse.RightButton == ButtonState.Released && _previousMouse.RightButton == ButtonState.Pressed)
                {
                    RightClick?.Invoke(this, new EventArgs());
                }
            }
        }

        public int getColumn()
        {
            return this.columnNumber;
        }
        public int getRow()
        {
            return this.rowNumber;
        }

        public bool isNavigatable()
        {
            return this.navigatable;
        }

    }
}

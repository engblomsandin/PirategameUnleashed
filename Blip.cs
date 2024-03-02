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

        private Texture2D seaBlip;
        private Texture2D landBlip;
        private Texture2D cityBlip;
        private Texture2D shipBlip;
        private Texture2D companyBlip;
        private SpriteFont systemFont;

        private BlipType blipState;

        private GridHandler gridHandler = GridHandler.Instance;

        enum BlipType
        {
            seaBlip,
            landBlip,
            cityBlip,
            companyBlip,
            shipBlip,
        }


        public Blip(int x, int y, List<Texture2D> blipList, SpriteFont systemFont, int rowCount, int columnCount)
        {
            this.xPosition = x;
            this.yPosition = y;
            this.rowNumber = y / DataVariables.tileHeight;
            this.columnNumber = x / DataVariables.tileWidth;

            this.seaBlip = blipList.ElementAt(1);
            this.landBlip = blipList.ElementAt(2);
            this.cityBlip = blipList.ElementAt(3);
            this.companyBlip = blipList.ElementAt(4);
            this.shipBlip = blipList.ElementAt(5);
            this.systemFont = systemFont;

            if (this.columnNumber <= 3 || this.columnNumber >= 60)
            {
                this.blipState = BlipType.landBlip;                
            }
            else
            {
                this.blipState = BlipType.seaBlip;
                this.navigatable = true;
            }

            if (this.columnNumber == 3 && this.rowNumber == 15)
            {
                this.blipState = BlipType.cityBlip;
            }

            this.LeftClick += this.onLeftClick;
            this.RightClick += this.onRightClick;
        }

        public void onLeftClick(object sender, System.EventArgs e)
        {
            switch (blipState)
            {
                case BlipType.seaBlip:
                    setState(BlipType.shipBlip);
                    break;
                default:
            setState(BlipType.companyBlip);
                    break;
            }
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
                case BlipType.seaBlip:
                    return this.seaBlip;

                case BlipType.landBlip:
                    return this.landBlip;

                case BlipType.cityBlip:
                    return this.cityBlip;

                case BlipType.companyBlip:
                    return this.companyBlip;

                case BlipType.shipBlip:
                    return this.shipBlip;

                default:
                    return this.seaBlip;
            }
        }

        private void setState(BlipType type)
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

        public string getPos()
        {
            string pos = this.columnNumber + "," + this.rowNumber;
            return pos;
        }

    }
}

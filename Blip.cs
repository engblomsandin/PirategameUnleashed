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



        public event EventHandler LeftClick;

        private MouseState _currentMouse;
        private MouseState _previousMouse;

        private Texture2D seablip;
        private Texture2D landblip;
        private Texture2D cityblip;
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
            this.systemFont = systemFont;

            if (this.columnNumber <= 3 || this.columnNumber >= 60)
            {
                this.blipState = blipType.landblip;
            }
            else
            {
                this.blipState = blipType.seablip;
            }

            if(this.columnNumber == 3 && this.rowNumber == 15)
            {
                this.blipState = blipType.cityblip;
            }

            this.LeftClick += this.onLeftClick;
        }

        public void onLeftClick(object sender, System.EventArgs e)
        {
            Debug.WriteLine("click");
            setState(blipType.cityblip);
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
            switch(this.blipState){
                case blipType.seablip:
                    return this.seablip;

                case blipType.landblip:
                    return this.landblip;

                case blipType.cityblip:
                    return this.cityblip;

                case blipType.companyblip:
                    return this.companyblip;

                default:
                    return this.seablip;
            }
        }

        private void setState(blipType type)
        {
            this.blipState = type;
        }
    }
}

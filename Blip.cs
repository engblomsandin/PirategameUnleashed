using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{
    public class Blip
    {
        private bool isBorder = false;

        private int xPosition = 0;
        private int yPosition = 0;

        public event EventHandler LeftClick;

        private MouseState _currentMouse;
        private MouseState _previousMouse;

        private Texture2D seablip;
        private Texture2D landblip;
        private Texture2D borderblip;
        private SpriteFont systemFont;

        private blipType blipState;

        private GridHandler gridHandler = GridHandler.Instance;

        enum blipType
        {
            seablip,
            landblip,
            borderblip
        }


        public Blip(int x, int y, Texture2D seablip, Texture2D landblip, Texture2D borderblip, SpriteFont systemFont, int rowCount, int columnCount)
        {
            if (x == 0 || y == 0 || x == columnCount - 1 || y == rowCount - 1)
            {
                this.isBorder = true;
            }
            this.xPosition = x;
            this.yPosition = y;

            this.seablip = seablip;
            this.landblip = landblip;
            this.borderblip = borderblip;
            this.systemFont = systemFont;

            this.blipState = blipType.seablip;

            this.LeftClick += this.onLeftClick;
        }

        public void onLeftClick(object sender, System.EventArgs e)
        {

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
            spriteBatch.Draw(this.getBlipTexture(), new Rectangle(this.xPosition, this.yPosition, 20, 20), Color.Gray);
        }


        private Texture2D getBlipTexture()
        {
            switch(this.blipState){
                case blipType.seablip:
                    return this.seablip;

                case blipType.landblip:
                    return this.landblip;

                case blipType.borderblip:
                    return this.borderblip;

                default:
                    return this.borderblip;
            }
        }

    }


}

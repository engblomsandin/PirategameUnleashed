using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{


    public class Ship
    {
        private string name = "";

        private Blip destination = null;
        private Blip startPoint = null;

        private int capacity = 0;
        private int speed = 0;

        private Blip currentBlip;
        public Ship(Blip currentBlip)
        {
            this.currentBlip = currentBlip;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void setCurrentBlip(Blip newBlip)
        {
            this.currentBlip.setState(BlipType.seaBlip);
            this.currentBlip = newBlip;
            this.currentBlip.setState(BlipType.shipBlip);
        }
    }
}

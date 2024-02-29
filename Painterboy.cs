using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{
    public class Painterboy
    {

        public static Painterboy instance = null;
        public static Painterboy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Painterboy();
                }
                return instance;
            }
        }
  

        private GridHandler gridHandler = GridHandler.Instance;

        public void DrawAllblips()
        {

        }

        public void DrawIndividualBlip()
        {
            
        }
    }
}

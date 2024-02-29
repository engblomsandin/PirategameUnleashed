using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{
    internal class Company
    {

        private int income = 0;
        private int treasury = 0;

        private Blip location;

        List<Ship> shipList = new List<Ship>();
        List<Blip> tradingPostList = new List<Blip>();



        public Company()
        {

        }

    }
}

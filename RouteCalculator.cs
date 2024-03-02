using Microsoft.Xna.Framework.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{


    public class RouteCalculator
    {
        public List<List<Blip>> blipGrid;

        private bool finished = false;

        private int destinationX = 0;
        private int destinationY = 0;

        private int startX = 0;
        private int startY = 0;

        private HorizontalDirection horDir;
        private VerticalDirection verDir;

        Queue route = new Queue();

        public static RouteCalculator instance = null;

        private enum HorizontalDirection
        {
            west,
            east,
        }

        private enum VerticalDirection
        {
            south,
            north,
        }

        public static RouteCalculator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RouteCalculator();
                }
                return instance;
            }
        }

        private RouteCalculator()
        {

        }

        public void initializeRouteCalculator(List<List<Blip>> blipGrid)
        {
            this.blipGrid = blipGrid;
        }

        public Queue calculate(Blip start, Blip Destination)
        {
            this.route.Clear();
            this.destinationX = Destination.getColumn();
            this.destinationY = Destination.getRow();

            this.startX = start.getColumn();
            this.startY = start.getRow();

            if(this.destinationX > startX)
            {
                this.horDir = HorizontalDirection.east;
            }
            else
            {
                this.horDir = HorizontalDirection.west;
            }
            if (this.destinationY > startY)
            {
                this.verDir = VerticalDirection.south;
            }
            else
            {
                this.verDir = VerticalDirection.north;
            }

            Debug.WriteLine(this.route.Count);

            return this.route;
        }

        public void explore(int x, int y)
        {
            Debug.WriteLine("Went to " + x + "," + y);

            if (x < 0 || y < 0 || x == DataVariables.columnCount + 1 || y == DataVariables.rowCount + 1)
                return;

            if (finished)
                return;

            route.Enqueue(blipGrid.ElementAt(y).ElementAt(x));

            if(x == destinationX && y == destinationY)
                this.finished= true;

            if(this.horDir == HorizontalDirection.west)
            {
                if (blipGrid.ElementAt(y).ElementAt(x - 1).isNavigatable())
                    explore(x - 1, y);
            }
            if (this.verDir == VerticalDirection.north)
            {
                if (blipGrid.ElementAt(y - 1).ElementAt(x).isNavigatable())
                    explore(x, y - 1);
            }
            else
            {
                if (blipGrid.ElementAt(y).ElementAt(x + 1).isNavigatable())
                    explore(x + 1, y);
                if (blipGrid.ElementAt(y + 1).ElementAt(x).isNavigatable())
                    explore(x, y + 1);
                if (blipGrid.ElementAt(y).ElementAt(x - 1).isNavigatable())
                    explore(x - 1, y);
                if (blipGrid.ElementAt(y - 1).ElementAt(x).isNavigatable())
                    explore(x, y - 1);
            }

            if (finished)
                return;
        }

    }
}

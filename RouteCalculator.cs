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

        Queue<Blip> route = new Queue<Blip>();

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

        public Queue<Blip> calculate(Blip start, Blip Destination)
        {
            Debug.WriteLine("Start Calculation");
            this.route.Clear();
            this.destinationX = Destination.getColumn();
            this.destinationY = Destination.getRow();

            this.startX = start.getColumn();
            this.startY = start.getRow();

            getDirection();
            
            explore(startX, startY);

            Debug.WriteLine(this.route.Count);

            return this.route;
        }

        public void explore(int x, int y)
        {
            Debug.WriteLine("Went to " + x + "," + y);
            Debug.WriteLine("Destination is " + destinationX + "," + destinationY);

            getDirection();

            if (x < 0 || y < 0 || x == DataVariables.columnCount -1 || y == DataVariables.rowCount - 1)
                return;

            route.Enqueue(blipGrid.ElementAt(y).ElementAt(x));

            if(x == destinationX && y == destinationY)
                this.finished= true;

            if (finished)
                return;

            if (this.horDir == HorizontalDirection.west)
            {
                Debug.WriteLine("the destination is to the west, going west");
                if (blipGrid.ElementAt(y).ElementAt(x - 1).isNavigatable())
                    explore(x - 1, y);
            }
            if (this.verDir == VerticalDirection.north)
            {
                Debug.WriteLine("the destination is to the north, going north");
                if (blipGrid.ElementAt(y - 1).ElementAt(x).isNavigatable())
                    explore(x, y - 1);
            }
            if (this.horDir == HorizontalDirection.east)
            {
                Debug.WriteLine("the destination is to the east, going east");
                if (blipGrid.ElementAt(y).ElementAt(x - 1).isNavigatable())
                    explore(x - 1, y);
            }
            if (this.verDir == VerticalDirection.south)
            {
                Debug.WriteLine("the destination is to the south, going south");
                if (blipGrid.ElementAt(y + 1).ElementAt(x).isNavigatable())
                    explore(x, y + 1);
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

        public void getDirection()
        {
            if (this.destinationX > startX)
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
        }

    }
}

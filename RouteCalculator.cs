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

            getDirection(startX,startY);
            
            explore(startX, startY);

            Debug.WriteLine("The route is " +this.route.Count + " Steps");

            return this.route;
        }

        public void explore(int x, int y)
        {
            Debug.WriteLine("Went to " + x + "," + y);
            Debug.WriteLine("Destination is " + destinationX + "," + destinationY);

            getDirection(x,y);

            Debug.WriteLine("I should head " + this.verDir + this.horDir);

            if (x < 1 || y < 1 || x == DataVariables.columnCount -1 || y == DataVariables.rowCount - 1)
                return;

            route.Enqueue(blipGrid.ElementAt(y-1).ElementAt(x-1));

            if(x == destinationX && y == destinationY)
            {
                Debug.WriteLine("i found it!");
                this.finished = true;
            }

            if (finished)
                return;

            int dice = getBias();

            switch (dice)
            {
                case 1:
                    if (blipGrid.ElementAt(y).ElementAt(x + 1).isNavigatable()) //Go East
                        explore(x + 1, y);
                    break;
                case 2:
                    if (blipGrid.ElementAt(y + 1).ElementAt(x).isNavigatable()) //Go South
                            explore(x, y + 1);
                    break;
                case 3:
                    if (blipGrid.ElementAt(y).ElementAt(x - 1).isNavigatable()) //Go West
                            explore(x - 1, y);
                    break;
                case 4:
                    if (blipGrid.ElementAt(y - 1).ElementAt(x).isNavigatable()) //Go North
                            explore(x, y - 1);
                    break;
            }   
            if (finished)
                return;
        }

        public void getDirection(int x, int y)
        {
            if (this.destinationX > x)
            {
                this.horDir = HorizontalDirection.east;
            }
            else
            {
                this.horDir = HorizontalDirection.west;
            }
            if (this.destinationY > y)
            {
                this.verDir = VerticalDirection.south;
            }
            else
            {
                this.verDir = VerticalDirection.north;
            }
        }
        public int getBias()
        {

            Random rnd = new Random();
            if (this.horDir == HorizontalDirection.east)
            {
                if (rnd.Next(1, 11) > 5)
                    return 1;
            }
            else
            {
                if (rnd.Next(1, 11) > 5)
                    return 3;
            }
            if (this.verDir == VerticalDirection.north)
            {
                if (rnd.Next(1, 11) > 5)
                    return 4;
            }
            else
            {
                if (rnd.Next(1, 11) > 5)
                    return 2;
            }
            return rnd.Next(1, 5);
        }
    }
}

using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PirategameUnleashed
{
    public class GridHandler
    {
        public static GridHandler instance = null;
        public static GridHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GridHandler();
                }
                return instance;
            }
        }

        public int rowCount { get; private set; }
        public int columnCount { get; private set; }
        public SpriteFont systemFont { get; private set; }
        public List<List<Blip>> blipGrid { get; private set; }

        public RouteCalculator routeCalculator;

        private GridHandler()
        {

        }

        public void initializeGrid(int rowCount, int columnCount, List<Texture2D> blipList, SpriteFont systemfont)
        {
            routeCalculator = RouteCalculator.Instance;

            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.systemFont = systemfont;
            this.blipGrid = new List<List<Blip>>();
            for (int i = 0; i < rowCount; i++)
            {
                this.blipGrid.Add(new List<Blip>());
                for (int j = 0; j < columnCount; j++)
                {
                    this.blipGrid[i].Add(new Blip(
                        DataVariables.tileWidth + j * DataVariables.tileWidth,
                        DataVariables.tileHeight + i * DataVariables.tileHeight,
                        blipList,
                        systemfont,
                        rowCount,
                        columnCount));
                }
            }
            routeCalculator.initializeRouteCalculator(this.blipGrid);
        }



        public List<List<Blip>> getGrid()
        {
            return this.blipGrid;
        }

    }
}

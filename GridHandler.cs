using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public Texture2D seablip { get; private set; }
        public Texture2D landblip { get; private set; }
        public Texture2D borderblip { get; private set; }
        public SpriteFont systemFont { get; private set; }
        public List<List<Blip>> blipGrid { get; private set; }

        private GridHandler()
        {

        }

        public void initializeGrid(int rowCount, int columnCount, Texture2D seablip, Texture2D landblip, Texture2D borderblip, SpriteFont systemfont)
        {

            this.rowCount = rowCount;
            this.columnCount = columnCount;
            this.seablip = seablip;
            this.landblip = landblip;
            this.systemFont = systemfont;
            this.blipGrid = new List<List<Blip>>();
            for (int i = 0; i < rowCount; i++)
            {
                this.blipGrid.Add(new List<Blip>());
                for (int j = 0; j < columnCount; j++)
                {
                    Console.WriteLine(j);
                    Console.WriteLine(i);
                    this.blipGrid[i].Add(new Blip(j, i, seablip, landblip, borderblip, systemfont, rowCount, columnCount));
                }
            }
        }

        public List<List<Blip>> getGrid()
        {
            return this.blipGrid;
        }

    }
}

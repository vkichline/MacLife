using System;

namespace MacLife.Models
{
    public class World
    {
        public Cell[,] table;   // Table is stored as [height, width]
        public int width { get; internal set; }  // Size of the world grid
        public int height { get; internal set; } // Size of the world grid
        public int generation { get; internal set; }
        public string elapsed { get; internal set; }
        public bool phase = true; // Toggles with each generation so cells can alternated data

        public World(int h, int v)
        {
            width = h;
            height = v;
            phase = false;
            generation = 1;
            elapsed = "";
            CreateAllCells();
            SetPattern();
        }

        ~World()
        {
            DeleteAllCells();
        }

        private void CreateAllCells()
        {
            table = new Cell[height, width];
            for (int iv = 0; iv < height; iv++)
            {
                for (int ih = 0; ih < width; ih++)
                {
                    table[iv, ih] = new Cell(this, ih, iv);
                }
            }
            for (int iv = 0; iv < height; iv++)
            {
                for (int ih = 0; ih < width; ih++)
                {
                    table[iv, ih].Init();
                }
            }
        }

        private void DeleteAllCells()
        {
            for (int iv = 0; iv < height; iv++)
            {
                for (int ih = 0; ih < width; ih++)
                {
                    table[iv, ih] = null;
                }
            }
            table = null;
        }

        // Create a r-pentomino, centered
        public void SetPattern()
        {
            int v = height / 2;
            int h = width / 2;
            table[v - 1, h].value = 1;
            table[v - 1, h + 1].value = 1;
            table[v, h].value = 1;
            table[v, h - 1].value = 1;
            table[v + 1, h].value = 1;
        }

        public void Regenerate()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int iv = 0; iv < height; iv++)
            {
                for (int ih = 0; ih < width; ih++)
                {
                    table[iv, ih].Regenerate();
                }
            }
            stopwatch.Stop();
            elapsed = stopwatch.Elapsed.ToString();
            generation++;
            phase = !phase;
        }

        public void Reset()
        {
            DeleteAllCells();
            CreateAllCells();
            SetPattern();
            generation = 1;
        }
    }
}

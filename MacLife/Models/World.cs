using System;

namespace MacLife.Models
{
    public class World
    {
        public Cell[,] table;
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
            table = new Cell[width, height];
            for (int ih = 0; ih < width; ih++)
            {
                for (int iv = 0; iv < height; iv++)
                {
                    table[ih, iv] = new Cell(this, ih, iv);
                }
            }
            for (int ih = 0; ih < width; ih++)
            {
                for (int iv = 0; iv < height; iv++)
                {
                    table[ih, iv].Init();
                }
            }
        }

        private void DeleteAllCells()
        {
            for (int ih = 0; ih < width; ih++)
            {
                for (int iv = 0; iv < height; iv++)
                {
                    table[ih, iv] = null;
                }
            }
            table = null;
        }

        // Create a r-pentomino, centered
        public void SetPattern()
        {
            int v = height / 2;
            int h = width / 2;
            table[h, v - 1].value = 1;
            table[h + 1, v - 1].value = 1;
            table[h, v].value = 1;
            table[h - 1, v].value = 1;
            table[h, v + 1].value = 1;
        }

        public void Regenerate()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for (int ih = 0; ih < width; ih++)
            {
                for (int iv = 0; iv < height; iv++)
                {
                    table[ih, iv].Regenerate();
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

using System;

namespace MacLife.Models
{
    public class Cell
    {
        private enum Position { TopCenter = 0, TopRight, Right, BottomRight, Bottom, BottomLeft, Left, TopLeft }
        private Cell[] neighbors = new Cell[Enum.GetNames(typeof(Position)).Length];
        private World world;
		private int value_a { get; set; }
		private int value_b { get; set; }
        private int h_pos;
        private int v_pos;

		public Cell(World w, int h, int v)
		{
			world = w;
            h_pos = h;
            v_pos = v;
			value = 0;
            private_value = 0;
		}

        ~Cell()
        {
            for (int i = 0; i < Enum.GetNames(typeof(Position)).Length; i++)
            {
                neighbors[i] = null;
            }
            world = null;
        }

		public int value
		{
			get
			{
				return (world.phase) ? value_a : value_b;
			}
			set
			{
				if (world.phase)
				{
					value_a = value;
				}
				else
				{
					value_b = value;
				}
			}
		}

		private int private_value
		{
			get
			{
				return (!world.phase) ? value_a : value_b;
			}
			set
			{
				if (!world.phase)
				{
					value_a = value;
				}
				else
				{
					value_b = value;
				}
			}
		}

        private int FixH(int h)
        {
            if (h < 0) { return world.width - 1; }
            if (h >= world.width) { return 0; }
            return h;
        }

        private int FixV(int v)
        {
            if (v < 0) { return world.height - 1; }
            if (v >= world.height) { return 0; }
            return v;
        }

        public void Init()
        {
            neighbors[(int)Position.TopCenter] = world.table[h_pos, FixV(v_pos - 1)];
            neighbors[(int)Position.TopRight] = world.table[FixH(h_pos + 1), FixV(v_pos - 1)];
            neighbors[(int)Position.Right] = world.table[FixH(h_pos + 1), v_pos];
            neighbors[(int)Position.BottomRight] = world.table[FixH(h_pos + 1), FixV(v_pos + 1)];
            neighbors[(int)Position.Bottom] = world.table[h_pos, FixV(v_pos + 1)];
            neighbors[(int)Position.BottomLeft] = world.table[FixH(h_pos - 1), FixV(v_pos + 1)];
            neighbors[(int)Position.Left] = world.table[FixH(h_pos - 1), v_pos];
            neighbors[(int)Position.TopLeft] = world.table[FixH(h_pos - 1), FixV(v_pos - 1)];
			value = 0;
			private_value = 0;
		}

        // Calculate the value of surroundings by value and set private_value.
        // If value = 1 and total = 2 or 3, private_value <- 1
        // If value = 0 and total = 3, private_value = 1
        // Else, private_value = 0
        public void Regenerate()
        {
            int total = 0;
            private_value = 0;

            if(value == 1) {
                private_value = 0;
            }

            foreach(Cell c in neighbors)
            {
                total += c.value;
            }
            if(value == 1 && (total == 2 || total == 3))
            {
                private_value = 1;
            }
            else if(value == 0 && total == 3)
            {
                private_value = 1;
            }
        }
    }
}

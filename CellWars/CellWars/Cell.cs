using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CellWars
{
    public class Cell
    {
        public Cell(string username, Point location, Size size, Color color)
        {
            Username = username;
            Location = location;
            Size = size;
            Color = color;
        }

        public string Username { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}

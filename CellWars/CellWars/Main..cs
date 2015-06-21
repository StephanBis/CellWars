using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace CellWars
{
    public partial class Main : Telerik.WinControls.UI.RadForm
    {
        private List<Cell> computerCells = new List<Cell>();
        private List<Cell> dormentCells = new List<Cell>();
        private Cell playerCell;
        private Random random;
        private string[] colors = { "Red", "Orange", "Green", "Blue", "Purple", "Brown", "Black" };
        private bool firstTime = true;

        public Main(string username, string color)
        {
            InitializeComponent();

            this.random = new Random();

            Point location = new Point(random.Next(0, gamePanel.Width), random.Next(0, gamePanel.Height));
            Size size = new Size(30, 30);

            this.playerCell = new Cell(username, location, size, Color.FromName(color));

            gameTimer.Start();
        }

        private void GenerateComputers()
        {
            for (int i = 1; i <= 15; i++)
            {
                Point location = new Point(random.Next(0, gamePanel.Width), random.Next(0, gamePanel.Height));
                Size size = new Size(25, 25);

                Cell cell = new Cell("", location, size, Color.FromName(colors[random.Next(0, colors.Length - 1)]));

                dormentCells.Add(cell);
            }
        }

        private void GenerateDorments()
        {
            for (int i = 1; i <= 250; i++)
            {
                Point location = new Point(random.Next(0, gamePanel.Width), random.Next(0, gamePanel.Height));
                Size size = new Size(10, 10);

                Cell cell = new Cell("PC " + i, location, size, Color.FromName(colors[random.Next(0, colors.Length - 1)]));

                computerCells.Add(cell);
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (firstTime == true)
            {
                GenerateComputers();

                GenerateDorments();

                firstTime = false;
            }
            else
            {

            }

            gamePanel.Invalidate();
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {
            // Paint the background since we specified ControlStyles.AllPaintingInWmPaint and ControlStyles.Opaque.
            e.Graphics.Clear(Color.White);

            foreach (Cell cell in computerCells)
            {
                SolidBrush brush = new SolidBrush(cell.Color);

                e.Graphics.FillEllipse(brush, cell.Location.X, cell.Location.Y, cell.Size.Width, cell.Size.Height);
            }

            foreach (Cell cell in dormentCells)
            {
                SolidBrush brush = new SolidBrush(cell.Color);

                e.Graphics.FillEllipse(brush, cell.Location.X, cell.Location.Y, cell.Size.Width, cell.Size.Height);
            }

            SolidBrush playerBrush = new SolidBrush(playerCell.Color);

            e.Graphics.FillEllipse(playerBrush, playerCell.Location.X, playerCell.Location.Y, playerCell.Size.Width, playerCell.Size.Height);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

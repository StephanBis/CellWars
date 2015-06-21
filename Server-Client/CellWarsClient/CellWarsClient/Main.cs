using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace CellWarsClient
{
    public partial class Main : Telerik.WinControls.UI.RadForm
    {
        Server server;

        public Main(Server server, string color)
        {
            InitializeComponent();

            this.server = server;

            //random location
            Random random = new Random();

            server.WriteServer("~X:" + random.Next(0, 10).ToString());
            System.Threading.Thread.Sleep(500);
            server.WriteServer("~Y:" + random.Next(0, 10).ToString());
            System.Threading.Thread.Sleep(500);
            server.WriteServer("~Size:20");
            System.Threading.Thread.Sleep(500);
            server.WriteServer("~Color:" + color);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void checkTimer_Tick(object sender, EventArgs e)
        {
            server.WriteServer("~Ouput:");
            
        }
    }
}

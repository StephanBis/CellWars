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
using Telerik.WinControls.UI;

namespace CellWarsClient
{
    public partial class Start : Telerik.WinControls.UI.RadForm
    {
        Server server;

        public Start()
        {
            InitializeComponent();

            server = new Server();

            if (!server.Connect("127.0.0.1", 8888))
            {
                MessageBox.Show("Could not connect to master server!");

                usernameTextbox.Enabled = false;
                colorDropDownList.Enabled = false;
                startButton.Enabled = false; 
            }
            else
            {
                string[] colors = { "Red", "Orange", "Green", "Blue", "Purple", "Brown", "Black" };

                for (int i = 0; i <= colors.Length - 1; i++)
                {
                    RadListDataItem item = new RadListDataItem();
                    item.ForeColor = Color.FromName(colors[i]);
                    item.Text = colors[i];
                    colorDropDownList.Items.Add(item);
                }

                colorDropDownList.SelectedIndex = 0;
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            server.WriteServer("~Username:" + usernameTextbox.Text);

            Main main = new Main(server, colorDropDownList.SelectedItem.ToString());
            main.Show();
            this.Hide();
        }
    }
}

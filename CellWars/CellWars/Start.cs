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
    public partial class Start : Telerik.WinControls.UI.RadForm
    {
        public Start()
        {
            InitializeComponent();

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

        private void startButton_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            Main main = new Main(usernameTextbox.Text, colorDropDownList.SelectedItem.ToString());
            main.Show();
            this.Hide();
        }
    }
}

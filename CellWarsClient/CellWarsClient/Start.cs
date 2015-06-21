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
    public partial class Start : Telerik.WinControls.UI.RadForm
    {
        TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        public Start()
        {
            InitializeComponent();

            clientSocket.Connect("127.0.0.1", 8888);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("~Username:" + usernameTextbox.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            MessageBox.Show(returndata);
        }
    }
}

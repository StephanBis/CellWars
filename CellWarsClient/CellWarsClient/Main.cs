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
        TcpClient clientSocket = new System.Net.Sockets.TcpClient();

        public Main()
        {
            InitializeComponent();

            msg("Client Started");
            clientSocket.Connect("127.0.0.1", 8888);
            msg("Client Socket Program - Server Connected ...");
        }

        public void msg(string mesg)
        {
            outputListbox.Items.Add(" >> " + mesg);
        } 

        private void connectButton_Click(object sender, EventArgs e)
        {
            outputListbox.Items.Clear();
            connectButton.Enabled = false;

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            //serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            serverStream.Read(inStream, 0, inStream.Length);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + returndata);
        }
    }
}

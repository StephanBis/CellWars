using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CellWarsClient
{
    public class Server
    {
        TcpClient clientSocket;
        NetworkStream serverStream;

        public bool Connect(string ip, int port)
        {
            try
            {
                clientSocket = new TcpClient(ip, port);
                serverStream = clientSocket.GetStream();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void WriteServer(string message)
        {
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(message + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();
        }

        public string ReadServer()
        {
            byte[] inStream = new byte[clientSocket.ReceiveBufferSize];
            serverStream.Read(inStream, 0, inStream.Length);

            string returndata = System.Text.Encoding.ASCII.GetString(inStream);

            return returndata;
        }

        public List<Cell> GetCells()
        {
            List<Cell> cells = new List<Cell>();



            return cells;
        }
    }
}

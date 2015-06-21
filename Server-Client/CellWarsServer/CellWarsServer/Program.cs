using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CellWarsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            List<handleClient> clients = new List<handleClient>();
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine(" >> " + "Server Started");

            counter = 0;
            while (true)
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine(" >> " + "Client ID:" + Convert.ToString(counter) + " connected!");
                handleClient client = new handleClient();
                client.startClient(clientSocket, Convert.ToString(counter));
                clients.Add(client);
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> " + "exit");
            Console.ReadLine();
        }
    }

    //Class to handle each client request separatly
    public class handleClient
    {
        TcpClient clientSocket;
        Thread ctThread;

        //properties
        public string username;
        public int x, y, size;
        public string color;
        public string id;

        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.id = clineNo;
            ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            while ((true))
            {
                try
                {
                    byte[] bytesFrom = new byte[clientSocket.ReceiveBufferSize];
                    string dataFromClient = null;
                    Byte[] sendBytes = null;
                    string serverResponse = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));

                    if (dataFromClient.StartsWith("~"))
                    {
                        string commando = dataFromClient.Substring(1, dataFromClient.IndexOf(":") - 1);
                        string value = dataFromClient.Substring(dataFromClient.IndexOf(":") + 1);

                        switch (commando)
                        {
                            case "Username" :
                                this.username = value;
                                break;
                            case "X":
                                this.x = Convert.ToInt32(value);
                                break;
                            case "Y":
                                this.y = Convert.ToInt32(value);
                                break;
                            case "Color":
                                this.color = value;
                                break;
                            case "Size":
                                this.size = Convert.ToInt32(value);
                                break;
                            case "Output":
                                serverResponse = "Id:" + id + ";Username:" + username + ";X:" + x.ToString() + ";Y:" + y.ToString() + ";Color:" + color + ";Size:" + size.ToString();
                                sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                                networkStream.Write(sendBytes, 0, sendBytes.Length);
                                networkStream.Flush();
                                Console.WriteLine(" >> Broadcast: " + serverResponse);
                                break;

                                
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host.")
                    {
                        Console.WriteLine(" >> " + "Client ID:" + Convert.ToString(id) + " disconnected!");
                        ctThread.Abort();
                    }
                    else
                    {
                        Console.WriteLine(" >> " + ex.Message);
                    }
                }
            }
        }
    } 
}

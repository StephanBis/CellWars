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
                Console.WriteLine(" >> " + "Client No:" + Convert.ToString(counter) + " started!");
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
        string clNo;
        Thread ctThread;
        public string username;

        public void startClient(TcpClient inClientSocket, string clineNo)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    //networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "Client(" + clNo + ") - " + dataFromClient);

                    if (dataFromClient.StartsWith("~"))
                    {
                        string commando = dataFromClient.Substring(1, dataFromClient.IndexOf(":") - 1);
                        string value = dataFromClient.Substring(dataFromClient.IndexOf(":"));

                        switch (commando)
                        {
                            case "Username" :
                                this.username = value;
                                break;
                        }
                    }

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Your username is: " + username;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.Message);
                    Console.WriteLine(" >> " + "Client No:" + Convert.ToString(clNo) + " disconnected!");
                    ctThread.Abort();
                }
            }
        }
    } 
}

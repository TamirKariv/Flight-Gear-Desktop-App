using System.IO;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Models
{
    //The Server
   public class Server
    {
        public bool IsConnected { get; set; }
        public bool Terminate { get; set; }
        private TcpListener server;
        private TcpClient tcpClient;
        private BinaryReader binaryReader;

       public Server()
        {
            IsConnected = false;
            Terminate = false;

        }


        //Open the server
        public void OpenServer(string IP, int port)
        {
            if (server != null)
            {
                TerminateConnection();

            }
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
        }

        //Recive the data
        public string[] ReciveData()
        {
            if (!IsConnected)
            {
                IsConnected = true;
                tcpClient = server.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();
                binaryReader = new BinaryReader(stream);
            }
            string data = "";
            char r;
            while ((r = binaryReader.ReadChar()) != '\n')
            {
                data += r;
            }
            string[] splitted = data.Split(',');
            string[] values = { splitted[0], splitted[1] };
            return values;

        }


        //terminate the connction
        public void TerminateConnection()
        {
            tcpClient.Close();
            server.Stop();
            IsConnected = false;
        }
    }
}

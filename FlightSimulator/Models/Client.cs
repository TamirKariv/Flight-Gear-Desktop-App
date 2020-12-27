using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Windows;

namespace FlightSimulator.Models
{
    //The Client
    public class Client
    {
        private TcpClient tcpClient;

        private BinaryWriter binaryWriter;

        public bool IsRuninning
        {
            get; set;
        }

        private static Client sender_Instance = null;

        public Client()
        {
            IsRuninning = false;
        }
        public static Client Instance
        {
            get
            {
                if (sender_Instance == null)
                {
                    sender_Instance = new Client();
                }
                return sender_Instance;
            }
        }

        //send data to the server
        public void SendData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }
            string[] splitted = data.Split('\n');
            for (int i = 0; i < splitted.Length; ++i)
            {
                string command = splitted[i] + "\r\n";
                binaryWriter.Write(System.Text.Encoding.ASCII.GetBytes(command));
                System.Threading.Thread.Sleep(2000);
            }
        }


        //recconect to the server
        public void Reconnect()
        {
            if (sender_Instance != null)
            {
                sender_Instance = null;
            }
        }


        //Establish connection with the server
        public void ConnectToServer(string IP, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(IP), port);
            tcpClient = new TcpClient();
            while (!tcpClient.Connected)
            {
                try
                {
                    tcpClient.Connect(ep);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error Establishing Connection!");
                }
            }
            IsRuninning = true;
            NetworkStream stream = tcpClient.GetStream();
            binaryWriter = new BinaryWriter(stream);
        }


    }
}

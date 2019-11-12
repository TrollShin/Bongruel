using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace Bongruel.Helper
{
    public class BNetwork
    {
        //server address: 10.80.163.138 port: 8000
        public Socket socket = null;

        private byte[] buffer;

        public const string ip = "10.80.163.138";
        public const int port = 80;



        public void Create()
        {
            if (socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Debug.WriteLine("Create");
            }
        }


        public void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                //Socket client = (Socket) ar.AsyncState;
                buffer = new byte[socket.ReceiveBufferSize];
                Debug.WriteLine("ConnectCallback");
            }
            catch(Exception)
            {
                return;
            }
        }
/*
        internal bool CheckServer()
        {
            throw new NotImplementedException();
        }*/

        public void Connect(string ip, int port)
        {
            if (socket == null)
            {
                Create();
            }

            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint endpoint = new IPEndPoint(ipAddress, port);
            socket.BeginConnect(endpoint, ConnectCallback, null);
            Debug.WriteLine("Connect");
        }


        public void Send(string message)
        {
            if (socket == null)
            {
                Create();
            }

            if(socket.Connected == false)
            {
                return;
            }

            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            socket.BeginSend(messageBuffer, 0, messageBuffer.Length, SocketFlags.None, SendCallback, null);
            Debug.WriteLine("Send");
        }


        public void SendCallback(IAsyncResult ar)
        {
            //Socket client = (Socket) ar.AsyncState;
            socket.EndSend(ar);
            Debug.WriteLine("SendCallback");

        }

        public void Receive(Socket socket)
        {
                      
            byte[] buffer = new byte[255];
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
            Debug.WriteLine("Receive");
        }


        public void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                //socket client = (socket) ar.AsyncState;
                socket.EndReceive(ar);
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
                Debug.WriteLine("ReceiveCallback");
            }
            catch(Exception)
            {
                return;
            }
    
        }

        public bool CheckServer(string ip, int port)
        {
            try
            {
                if (socket == null)
                {
                    Create();
                }

                IPAddress ipAddress = IPAddress.Parse(ip);
                IPEndPoint endpoint = new IPEndPoint(ipAddress, port);

                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();

                options.DontFragment = true;

                string data = "aaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = 120;
                PingReply reply = pingSender.Send(ipAddress, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    Connect(ip, port);
                }
                else
                {
                    throw new Exception();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

    }
}

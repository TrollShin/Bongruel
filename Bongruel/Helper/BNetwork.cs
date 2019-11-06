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


        public void Create()
        {
            if (socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Debug.WriteLine("소켓 생성");
            }
        }


        public void ConnectCallback(IAsyncResult ar)
        {
            //Socket client = (Socket) ar.AsyncState;
            socket.EndConnect(ar);
            buffer = new byte[socket.ReceiveBufferSize];
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
            Debug.WriteLine("ConnectCallback");
        }

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

#if false
        public void Receive(Socket socket)
        {
                      
            byte[] buffer = new byte[255];
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);

        }
#endif

        public void ReceiveCallback(IAsyncResult ar)
        {
            //socket client = (socket) ar.AsyncState;
            socket.EndReceive(ar);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCallback, null);
            Debug.WriteLine("ReceiveCallback");
        }

        public static bool CheckServer(string hostName)
        {
            try
            {
                Ping pingsender = new Ping();
                PingReply reply = pingsender.Send(hostName, 8000);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
            {
                return false;
            }
        }
            catch
            {
                return false;
            }
        }
            
        }

    }

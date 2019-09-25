using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bongruel.Helper
{
    public class BNetwork
    {
        //server address: 10.80.163.138 port: 8000
        public Socket socket = null; 

        public void Create()
        {
            if(socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
        }

        public void Connect(string ip, int port)
        {
            if(socket == null)
            {
                Create();
            }

            IPAddress ipAddress = IPAddress.Parse(ip);
            IPEndPoint endpoint = new IPEndPoint(ipAddress, port);
            socket.Connect(endpoint);
        }

        //실험 중인 코드
        public void Send(string message)
        {
            if(socket == null)
            {
                 Create();   
            }               
                       
            byte[] messageBuffer = Encoding.UTF8.GetBytes(message);
            socket.Send(messageBuffer, 0, messageBuffer.Length, 0);
                
            byte[] buffer = new byte[255];
            int recieve = socket.Receive(buffer, 0, buffer.Length, 0);

            Array.Resize(ref buffer, recieve);


        }

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;


namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.StartServer();
            Console.ReadKey();
        }

        void StartServer() //服务器异步连接
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse("192.168.19.97"); //设置IP
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 3355); //设置IP和端口号
            socket.Bind(ipEndPoint); //绑定IP和端口号
            socket.Listen(0); //开始监听客户端
            socket.BeginAccept(AcceptCallBack, socket); //通过BeginAccept进行异步连接
            Console.ReadKey();
        }

        void AcceptCallBack(IAsyncResult ar)
        {
            Console.WriteLine("有一个客户端连接了");
            Socket socket = ar.AsyncState as Socket;
            Socket clientSocket = socket.EndAccept(ar);
            //向客户端发送一条消息
            string date = "服务器连接成功";
            byte[] msg = System.Text.Encoding.UTF8.GetBytes(date);
            clientSocket.Send(msg);
            clientSocket.BeginReceive(dateBuffer, 0, 1024, SocketFlags.None, ReceiveCallBcak, clientSocket);
            socket.BeginAccept(AcceptCallBack, socket);
        }

        static byte[] dateBuffer = new byte[1024];
        private int i=1;
        void ReceiveCallBcak(IAsyncResult ar) //事件，ar传递值
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);
                string msg = Encoding.UTF8.GetString(dateBuffer, 0, count);//获取客户端发来数据
                Console.WriteLine("客户端发送消息：" + msg);
                i++;
                Thread.Sleep(2000);//等待2秒再次发送数据
                byte[] date = Encoding.UTF8.GetBytes(i.ToString());
                clientSocket.Send(date);//发送数据
                Console.WriteLine("服务器发送数据：" + Encoding.UTF8.GetString(date));
                clientSocket.BeginReceive(dateBuffer, 0, 1024, SocketFlags.None, ReceiveCallBcak, clientSocket);
            }
            catch (Exception)
            {
                Console.WriteLine("客户端断开连接");
            }
        }
    }
}
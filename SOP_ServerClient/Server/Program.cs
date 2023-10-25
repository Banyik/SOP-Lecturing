using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    internal class Program
    {
        static bool listen = true;
        static TcpListener server;

        private const string usersFile = "Users.xml";
        private const string resourcesFile = "Resources.xml";

        static void Main(string[] args)
        {
            User.LoadUsers(usersFile);
            Resource.LoadResources(resourcesFile);
            IPAddress ip = IPAddress.Parse(ConfigurationManager.AppSettings["ip"].ToString());
            int port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());

            server = new TcpListener(ip, port);
            server.Start();

            new Thread(WaitForClients).Start();

            Console.WriteLine("Press enter to shut down the server...");
            Console.ReadLine();

            listen = false;
            Client.KillAllClient();
            server.Stop();
            User.SaveUsers(usersFile);
            Resource.SaveResources(resourcesFile);

        }

        static void WaitForClients()
        {
            while (listen)
            {
                if (server.Pending())
                {
                    TcpClient client = server.AcceptTcpClient();
                    new Client(client);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    internal class Client
    {
        public static List<Client> clients = new List<Client>();
        public static void KillAllClient()
        {
            foreach (var client in new List<Client>(Client.clients))
            {
                client.Close();
            }
        }

        StreamReader reader;
        StreamWriter writer;
        TcpClient client;
        User currentUser = null;
        Thread clientFetch;

        public Client(TcpClient client)
        {
            this.client = client;

            reader = new StreamReader(client.GetStream());
            writer = new StreamWriter(client.GetStream());

            clients.Add(this);

            clientFetch = new Thread(FetchCommands);
            clientFetch.Start();

            Console.WriteLine("A new client has been started...");
        }

        private void FetchCommands()
        {
            try
            {
                while (true)
                {
                    string command = reader.ReadLine();
                    Console.WriteLine($"Received command from client: {command}");
                    ProcessCommand(command);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Close();
                Console.WriteLine("Error occured while listening to the client...");
            }
        }

        private void ProcessCommand(string command)
        {
            string[] processedData = command.Split(' ');
            processedData[0] = processedData[0].ToLower();
            switch (processedData[0])
            {
                case "exit":
                    Close();
                    break;
                case "login":
                    Login(processedData);
                    break;
                case "logout":
                    Logout();
                    break;
                case "register":
                    Register(processedData);
                    break;
                case "list":
                    List();
                    break;
                case "add":
                    AddResource(processedData);
                    break;
                case "search":
                    Search(processedData);
                    break;
                default:
                    SendInformationToClient($"Unknown command: {command}");
                    break;
            }
        }

        private void Login(string[] data)
        {
            if(data.Length != 3)
            {
                SendInformationToClient("Wrong parameter count!");
                return;
            }
            if(currentUser != null)
            {
                SendInformationToClient("You are already logged in!");
                return;
            }
            currentUser = User.TryLogin(data[1], data[2]);
            if(currentUser == null)
            {
                SendInformationToClient("Wrong username or password!");
            }
            else
            {
                SendInformationToClient($"User {currentUser.Username} logged in...");
            }
        }
        private void Logout()
        {
            if(currentUser != null)
            {
                currentUser = null;
                SendInformationToClient("You are now logged out!");
            }
            else
            {
                SendInformationToClient("You are not logged in!");
            }
        }

        private void Register(string[] data)
        {
            if(data.Length != 3)
            {
                SendInformationToClient("Wrong parameter count!");
                return;
            }
            if(currentUser != null)
            {
                SendInformationToClient("You are already logged in!");
                return;
            }

            if (User.TryRegister(data[1], data[2]))
            {
                SendInformationToClient($"Successfully registered user {data[1]}!");
                Login(data);
            }
            else
            {
                SendInformationToClient("Username already exists!");
            }
        }

        private void List()
        {
            if(currentUser == null)
            {
                SendInformationToClient("You are not logged in!");
                return;
            }
            foreach (var resource in Resource.resources)
            {
                SendInformationToClient($"Resource\n Name:{resource.Name}\n Value:{resource.Value}");
            }
        }

        private void Search(string[] data)
        {
            if(data.Length != 2)
            {
                SendInformationToClient("Wrong parameter count!");
                return;
            }
            if(currentUser == null)
            {
                SendInformationToClient("You are not logged in!");
                return;
            }
            foreach (var resource in Resource.resources.FindAll(r => r.Name == data[1]))
            {
                SendInformationToClient($"Resource\n Name:{resource.Name}\n Value:{resource.Value}");
            }
        }

        private void AddResource(string[] data)
        {
            if (currentUser == null)
            {
                SendInformationToClient("You are not logged in!");
                return;
            }
            if (data.Length != 3)
            {
                SendInformationToClient("Wrong parameter count!");
                return;
            }
            int number;
            if (!int.TryParse(data[2], out number))
            {
                SendInformationToClient("Value must be integer type!");
                return;
            }

            Resource.resources.Add(new Resource(data[1], number));
            SendInformationToClient("Resource added!");
        }

        private void SendInformationToClient(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }


        public void Close()
        {
            reader.Close();
            writer.Close();
            client.Close();
            clients.Remove(this);
        }
    }
}

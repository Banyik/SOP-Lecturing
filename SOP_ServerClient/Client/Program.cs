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

namespace Client
{
    internal class Program
    {
        static StreamReader reader;
        static StreamWriter writer;
        static TcpClient client;
        static Thread rInfoThread = new Thread(ReadInfo);
        static void Main(string[] args)
        {
            string ip = ConfigurationManager.AppSettings["ip"].ToString();
            int port = int.Parse(ConfigurationManager.AppSettings["port"].ToString());
            try
            {
                client = new TcpClient(ip, port);
                writer = new StreamWriter(client.GetStream());
                reader = new StreamReader(client.GetStream());
                rInfoThread.Start();
                Console.WriteLine($"Connected to {ip}:{port}");
                Send();
            }
            catch (Exception)
            {

                Console.WriteLine("Something went wrong...");
            }
        }

        static void ReadInfo()
        {
            try
            {
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Console.WriteLine("Timed out...");
            }
        }
        static void Send()
        {
            try
            {
                while (true)
                {
                    Write(Console.ReadLine());
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Close();
            }
        }

        static void Write(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
        }

        static void Close()
        {
            reader.Close();
            writer.Close();
            client.Close();
        }
    }
}

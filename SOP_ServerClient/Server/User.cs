using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Server
{
    internal class User
    {
        string username;
        string password;
        public static List<User> users = new List<User>();

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        
        public static User TryLogin(string username, string password)
        {
            return users.Find(u => u.username == username && u.password == password);
        }
        public static bool TryRegister(string username, string password)
        {
            if(users.Find(u => u.username == username) != null)
            {
                return false;
            }

            users.Add(new User(username, password));
            return true;
        }

        public static void LoadUsers(string fileName)
        {
            users.Clear();
            if (File.Exists(fileName))
            {
                XDocument xml = XDocument.Load(fileName);
                foreach (var user in xml.Descendants("user"))
                {
                    User u = new User((string)user.Attribute("username"), (string)user.Attribute("password"));
                    users.Add(u);
                }
            }
        }

        public static void SaveUsers(string fileName)
        {
            XElement root = new XElement("users");
            foreach (var u in users)
            {
                root.Add(
                    new XElement(
                        "user",
                        new XAttribute((XName)"username", u.username),
                        new XAttribute((XName)"password", u.password)
                    )
                );
            }
            XDocument xml = new XDocument(root);
            xml.Save(fileName);
        }

    }
}

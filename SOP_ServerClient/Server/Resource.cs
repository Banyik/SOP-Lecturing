using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server
{
    internal class Resource
    {
        string name;
        int value;

        public static List<Resource> resources = new List<Resource>();

        public Resource(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name { get => name; set => name = value; }
        public int Value { get => value; set => this.value = value; }

        public static void LoadResources(string fileName)
        {
            resources.Clear();
            if (File.Exists(fileName))
            {
                XDocument xml = XDocument.Load(fileName);
                foreach (var resource in xml.Descendants("resource"))
                {
                    Resource r = new Resource((string)resource.Attribute("name"), (int)resource.Attribute("value"));
                    resources.Add(r);
                }
            }
        }

        public static void SaveResources(string fileName)
        {
            XElement root = new XElement("resources");
            foreach (var r in resources)
            {
                root.Add(
                    new XElement(
                        "resource",
                        new XAttribute((XName)"name", r.name),
                        new XAttribute((XName)"value", r.value)
                    )
                );
            }
            XDocument xml = new XDocument(root);
            xml.Save(fileName);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrochipFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller.CreateProducers(50, 500, ChipType.TYPE1, 1);
            Controller.CreateProducers(50, 1000, ChipType.TYPE2, 1);
            Controller.CreateConsumers(2);
            Controller.StartThreads();
            Console.ReadLine();
        }
    }
}

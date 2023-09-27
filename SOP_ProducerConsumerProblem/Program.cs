using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_ProducerConsumerProblem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller.CreateProducers(10, 10);
            Controller.CreateConsumers(10);
            Controller.StartThreads();
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_Reactor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Controller.CreateCores();
            Controller.StartCores();
            Controller.CreateHouseholds();
            Controller.StartHouseholds();
            Controller.StartLogger();
            Console.ReadLine();
        }
    }
}

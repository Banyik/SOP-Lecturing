using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_ProducerConsumerProblem
{
    internal class Product
    {
        static uint ID;
        uint id;

        public Product()
        {
            id = ID++;
        }

        public override string ToString()
        {
            return $"Product {id}";
        }
    }
}

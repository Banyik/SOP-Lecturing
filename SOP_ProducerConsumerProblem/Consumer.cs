using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_ProducerConsumerProblem
{
    internal class Consumer
    {
        static uint ID;
        uint id;

        public Consumer()
        {
            id = ID++;
        }

        public void Work()
        {
            Product product = null;
            while (Controller.products.Count > 0 || Controller.areProducersWorking)
            {
                if(Controller.TryConsume(this, out product))
                {
                    Console.WriteLine($"{this} has consumed {product}...");
                    product = null;
                }
            }
        }

        public override string ToString()
        {
            return $"Consumer {id}";
        }
    }
}

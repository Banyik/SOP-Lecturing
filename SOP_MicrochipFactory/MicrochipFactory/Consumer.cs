using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrochipFactory
{
    internal class Consumer
    {
        static uint ID;
        uint id;
        int packCount;
        ChipType packType;

        public uint Id { get => id; set => id = value; }
        internal ChipType PackType { get => packType; set => packType = value; }

        public Consumer()
        {
            id = ID++;
        }

        public void Work()
        {
            Product product = null;
            packCount = 0;
            while (Controller.products.Count > 0 || Controller.areProducersWorking)
            {
                if (Controller.TryConsume(this, out product))
                {
                    Console.WriteLine($"{this} has consumed {product}...");
                    packCount++;
                }
                if(packCount == 5)
                {
                    Console.WriteLine($"{this} has packed a box of {product.ChipType} chips...");
                    packCount = 0;
                    packType = ChipType.NONE;
                }
                product = null;
            }
            Console.WriteLine($"{this} has stopped consuming...");
        }

        public override string ToString()
        {
            return $"Consumer {id}";
        }
    }
}

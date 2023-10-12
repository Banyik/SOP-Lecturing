using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicrochipFactory
{
    internal class Producer
    {
        static uint ID;
        uint id;
        int amountToProduce;
        int productCount;
        int sleepTime;
        ChipType produceType;
        bool isWorking;

        public bool IsWorking { get => isWorking; set => isWorking = value; }

        public Producer(int amountToProduce, int sleepTime, ChipType produceType)
        {
            id = ID++;
            this.amountToProduce = amountToProduce;
            this.sleepTime = sleepTime;
            isWorking = false;
            this.produceType = produceType;
        }

        public void Work()
        {
            productCount = 0;
            isWorking = true;
            Product product = null;
            while (productCount < amountToProduce)
            {
                if (product == null)
                {
                    product = new Product(produceType);
                }
                if (Controller.TryProduce(this, product))
                {
                    productCount++;
                    Console.WriteLine($"{this} has produced {product}...");
                    product = null;
                }
                Thread.Sleep(sleepTime);
            }
            isWorking = false;
            Controller.CheckProducerWorkingState();
            Console.WriteLine($"{this} has stopped producing...");
        }

        public override string ToString()
        {
            return $"Producer {id}";
        }
    }
}

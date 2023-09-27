using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP_ProducerConsumerProblem
{
    internal static class Controller
    {
        public static List<Product> products = new List<Product>();
        static List<Producer> producers = new List<Producer>();
        static List<Consumer> consumers = new List<Consumer>();
        public static bool areProducersWorking = false;
        static int maxProductCount = 100;
        public static bool TryProduce(Producer producer, Product product)
        {
            bool success = false;

            Monitor.Enter(products);
            while (products.Count == maxProductCount)
            {
                Console.WriteLine($"{producer} is waiting...");
                Monitor.Wait(products);
            }
            if(products.Count < maxProductCount)
            {
                products.Add(product);
                success = true;
            }
            Monitor.PulseAll(products);
            Monitor.Exit(products);

            return success;
        }

        public static bool TryConsume(Consumer consumer, out Product product)
        {
            bool success = false;
            product = null;

            Monitor.Enter(products);
            while(products.Count == 0 && areProducersWorking)
            {
                Console.WriteLine($"{consumer} is waiting...");
                Monitor.Wait(products);
            }
            if(products.Count > 0)
            {
                product = products.First();
                products.Remove(product);
                success = true;
            }
            Monitor.PulseAll(products);
            Monitor.Exit(products);

            return success;
        }

        public static void CheckProducerWorkingState()
        {
            foreach (Producer producer in producers)
            { 
                if(producer.IsWorking)
                {
                    return;
                }
            }
            areProducersWorking = false;
        }

        public static void CreateConsumers(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                consumers.Add(new Consumer());
            }
        }
        public static void CreateProducers(int amountToProduce, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                producers.Add(new Producer(amountToProduce));
            }
        }

        public static void StartThreads()
        {
            areProducersWorking = true;
            foreach (var producer in producers)
            {
                new Thread(producer.Work).Start();
            }
            foreach (var consumer in consumers)
            {
                new Thread(consumer.Work).Start();
            }
        }
    }
}

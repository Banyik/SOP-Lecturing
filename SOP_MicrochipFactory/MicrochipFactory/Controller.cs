using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicrochipFactory
{
    internal class Controller
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
            if (products.Count < maxProductCount)
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
            foreach (var _consumer in consumers)
            {
                if(_consumer.Id != consumer.Id)
                {
                    switch (_consumer.PackType)
                    {
                        case ChipType.TYPE1:
                            consumer.PackType = ChipType.TYPE2;
                            break;
                        case ChipType.TYPE2:
                            consumer.PackType = ChipType.TYPE1;
                            break;
                    }
                }
            }
            while ((consumer.PackType == ChipType.NONE && products.Count == 0 && areProducersWorking) ||
                    consumer.PackType != ChipType.NONE && products.Count > 0 && areProducersWorking && products[0].ChipType != consumer.PackType)
            {
                Console.WriteLine($"{consumer} is waiting...");
                Monitor.Wait(products);
            }
            if (products.Count > 0 && consumer.PackType == ChipType.NONE)
            {
                product = products.First();
                consumer.PackType = product.ChipType;
                products.Remove(product);
                success = true;
            }
            else if (products.Count > 0 && consumer.PackType != ChipType.NONE && products.First().ChipType == consumer.PackType)
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
                if (producer.IsWorking)
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
        public static void CreateProducers(int amountToProduce, int sleepTime, ChipType produceType, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                producers.Add(new Producer(amountToProduce, sleepTime, produceType));
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

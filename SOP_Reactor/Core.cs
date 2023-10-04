using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SOP_Reactor
{
    internal class Core
    {
        static uint ID;
        uint id;
        Random rnd = new Random();
        int min = 3;
        int max = 10;
        public Core()
        {
            this.id = ID++;
        }

        public void IncreaseProduction()
        {
            min += 2;
            max += 5;
        }

        public void DecreaseProduction()
        {
            int x = rnd.Next(1, 4);
            if(min - x > 1)
            {
                min -= x;
            }

            if(max - x > 1)
            {
                max -= x;
            }
        }

        public void Work()
        {
            while (true)
            {
                Controller.ProducePower(this, rnd.Next(min, max));
                Thread.Sleep(1000);
            }
        }
    }
}

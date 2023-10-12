using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrochipFactory
{
    internal class Product
    {
        static uint ID;
        uint id;
        ChipType chipType;

        public Product(ChipType chipType)
        {
            id = ID++;
            this.chipType = chipType;
        }

        internal ChipType ChipType { get => chipType; set => chipType = value; }

        public override string ToString()
        {
            return $"Product {id}; Type: {chipType}";
        }
    }
}

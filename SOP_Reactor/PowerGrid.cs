using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOP_Reactor
{
    internal class PowerGrid
    {
        double mWh;
        double powerNeed;

        public PowerGrid()
        {
            mWh = 0;
            powerNeed = 0;
        }

        public double MWh { get => mWh; set => mWh = value; }
        public double PowerNeed { get => powerNeed; set => powerNeed = value; }

        public void AddPower(double mWh)
        {
            this.mWh += mWh;
            powerNeed -= mWh;
        }
        public void GetPower(double mWh)
        {
            this.mWh -= mWh;
            powerNeed += mWh;
        }

        public bool HasEnoughPower(double mWh)
        {
            double x = this.mWh - mWh;
            if(x > 0)
            {
                return true;
            }
            else
            {
                if(powerNeed < 0)
                {
                    powerNeed = 0;
                }
                powerNeed -= x;
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.model
{
    class Gram : AUnit
    {

        public Gram(float val, Units unit) : base(val, unit)
        {
            
        }
        
        public override string ToString()
        {
            return Trans(Quantity, Units.None, Units.Killo).ToString() + "kg";
        }
        public string ToString(Units unit)
        {
            string unitName;
            switch (unit)
            {
                case Units.Killo:
                    unitName = "kg";
                    break;
                case Units.None:
                    unitName = "g";
                    break;
                case Units.Milli:
                    unitName = "mg";
                    break;
                default:
                    unitName = "!!!not Supported!!!";
                    break;
            }
            return Trans(Quantity, Units.None, unit).ToString() + unitName;
        }
    }
}

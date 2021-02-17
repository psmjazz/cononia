using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.model
{
    class Liter : AUnit
    {
        public Liter(float val, Units unit) : base(val, unit)
        { }

        public override string ToString()
        {
            return Quantity + "L";
        }

        public string ToString(Units unit)
        {
            string unitName;
            switch (unit)
            {
                case Units.None:
                    unitName = "L";
                    break;
                case Units.Milli:
                    unitName = "mL";
                    break;
                default:
                    unitName = "!!!not Supported!!!";
                    break;
            }
            return Trans(Quantity, Units.None, unit).ToString() + unitName;
        }
    }
}

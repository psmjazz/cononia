using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.model
{
    abstract class AUnit
    {
        public enum Units
        {
            Milli,
            None,
            Killo
        }

        public float Quantity { get; set; }
        public Units Unit { get; }


        protected AUnit(float val, Units unit)
        {
            Unit = unit;

            Quantity = TransToNone(val, unit);
        }

        protected float Trans(float val, Units before, Units after)
        {
            int power = (int)before - (int)after;
            return val * (float)Math.Pow(1000, power);
        }

        protected float TransToNone(float val, Units unit)
        {
            return Trans(val, unit, Units.None);
        }

        public void Mult(float val)
        {
            Quantity *= val;
        }
        public void Div(float val)
        {
            if (val == 0f)
                throw new DivideByZeroException();
            Quantity /= val;
        }

        public static AUnit operator +(AUnit a) => a;
        public static AUnit operator - (AUnit a)
        {
            a.Quantity = -a.Quantity;
            return a;
        }
        public static AUnit operator +(AUnit a, AUnit b)
        {
            a.Quantity += b.Quantity;
            return a;
        }
        public static AUnit operator -(AUnit a, AUnit b)
            => a + (-b);
        
    }
}

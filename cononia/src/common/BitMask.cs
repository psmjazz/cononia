using cononia.src.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.common
{
    class BitMask<T> where T: Enum
    {
        private int _bit;
        private int _limit;

        public int Bit
        {
            get
            {
                return _bit;
            }
        }

        public BitMask(int bit = 0)
        {
            _bit = bit;
            _limit = 32;
        }

        public void SetValue(int value)
        {
            if(value > _limit)
            {
                return;
            }
            _bit |= 1 << value;
        }
        public void SetValue(T value)
        {
            SetValue(Convert.ToInt32(value));
        }

        public void UnsetValue(int value)
        {
            if (value > _limit)
            {
                return;
            }
            _bit &= ~(1 << value);
        }
        public void UnsetValue(Enum value)
        {
            UnsetValue(Convert.ToInt32(value));
        }

        public void SetValues(List<int> values)
        {
            foreach(int value in values){
                SetValue(value);
            }
        }
        public void SetValues(List<T> values)
        {
            foreach (Enum value in values)
            {
                SetValue(Convert.ToInt32(value));
            }
        }

        public void UnsetValues(List<int> values)
        {
            foreach (int value in values)
            {
                UnsetValue(value);
            }
        }
        public void UnsetValues(List<T> values)
        {
            foreach(Enum value in values)
            {
                UnsetValue(Convert.ToInt32(value));
            }
        }



        public bool HasValue(int value)
        {
            if (value > _limit)
            {
                return false;
            }
            return (_bit & 1 << value ) != 0;
        }
        public bool HasValue(Enum value)
        {
            return HasValue(Convert.ToInt32(value));
        }

        public void ParseValues(out List<int> values)
        {
            values = new List<int>();
            for(int i = 0; i< _limit; i++)
            {
                if (HasValue(i))
                    values.Add(i);
            }
        }


        public void ParseValues(out List<T> values)
        {
            values = new List<T>();
            for (int i = 0; i < _limit; i++)
            {
                if (HasValue(i))
                    values.Add((T)(Object)i);
            }
        }
    }
}

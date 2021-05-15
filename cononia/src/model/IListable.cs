using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.model
{
    interface IListable<T>
    {
        public void GetAll(out List<T> items);
    }
}

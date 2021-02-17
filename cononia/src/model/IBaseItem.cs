using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.model
{
    interface IBaseItem
    {
        long GetId();
        string GetName();
    }
}

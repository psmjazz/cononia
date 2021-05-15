using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;
using System.Text;

namespace cononia.src.model
{
    interface IBaseItem
    {
        //long GetId();
        //string GetName();
        long Id { get; set; }
        string Name { get; set; }
        
    }
}

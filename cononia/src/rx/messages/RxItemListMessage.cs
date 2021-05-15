using cononia.src.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace cononia.src.rx.messages
{
    class RxItemListMessage : MessageBase
    {
        public List<IBaseItem> ItemList { get; set; }
    }
}

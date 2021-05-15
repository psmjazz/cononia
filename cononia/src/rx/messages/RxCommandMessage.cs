using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace cononia.src.rx.messages
{
    class RxCommandMessage : MessageBase
    {
        public ECommand Command {get; set;}
    }
}

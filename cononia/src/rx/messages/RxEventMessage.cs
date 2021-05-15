using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace cononia.src.rx.messages
{
    class RxEventMessage : MessageBase
    {  
        public string RxMessage { get; set; }

        public ERxNodeName PublishingNode { get; set; }
        public ERxEventType EventType { get; set; } 

        public RxEventMessage(ERxNodeName publishingName, ERxEventType eventType)
        {
            this.PublishingNode = publishingName;
            this.EventType = eventType;
        }
    }
}

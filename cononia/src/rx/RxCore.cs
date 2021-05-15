using cononia.src.rx.messages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Subjects;
using System.Text;
using System.Windows.Forms;

namespace cononia.src.rx
{

    class RxCore
    {
        static RxCore instance = null;

        private RxNode _rxEventNode;

        private Dictionary<ERxNodeName, IObservable<MessageBase>> IObservables;


        private RxCore()
        {
            IObservables = new Dictionary<ERxNodeName, IObservable<MessageBase>>();
            _rxEventNode = new RxNode();
        }
        public static RxCore Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RxCore();
                }
                return instance;
            }
        }

        public IObservable<MessageBase> RxEvent
        {
            get
            {
                return _rxEventNode.AsObservable;
            }
        }
        private void PublishRxEvent(ERxNodeName publishingNode, ERxEventType eventType)
        {
            _rxEventNode.Publish(new RxEventMessage(publishingNode, eventType));
        }

        public RxNode CreateNode(ERxNodeName nodeName)
        {
            if (IObservables.ContainsKey(nodeName))
            {
                return null;
            }
            else
            {
                RxNode node = new RxNode();
                IObservables[nodeName] = node.AsObservable;
                PublishRxEvent(nodeName, ERxEventType.EventCreateNode);
                return node;
            }
        }

        public bool DeleteNode(ERxNodeName nodeName)
        {
            if (!IObservables.ContainsKey(nodeName))
            {
                return false;
            }
            else
            {
                IObservables.Remove(nodeName);
                return true;
            }
        }

        public IObservable<MessageBase> GetNode(ERxNodeName nodeName)
        {
            if (!IObservables.ContainsKey(nodeName))
            {
                return null;
            }
            else
            {
                return IObservables[nodeName];
            }
        }
    }
}

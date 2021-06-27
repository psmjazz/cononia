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

        private Dictionary<Enum, RxNode> _rxNodeDic;
        private Dictionary<Enum, RxMessage> _reservingMessage;
        

        private RxCore()
        {
            _rxNodeDic = new Dictionary<Enum, RxNode>();
            _reservingMessage = new Dictionary<Enum, RxMessage>();
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

        public void RegisterListener(Enum eventName, Action<RxMessage> listener)
        {
            if(_rxNodeDic.ContainsKey(eventName))
            {
                _rxNodeDic[eventName].RegisterEventListener(listener);
            }
            else
            {
                _rxNodeDic[eventName] = new RxNode();
                _rxNodeDic[eventName].RegisterEventListener(listener);
            }
        }

        public void SendMessage(Enum eventName, RxMessage message)
        {
            if(_rxNodeDic.ContainsKey(eventName))
            {
                _rxNodeDic[eventName].Publish(message);
            }
        }

        //public IObservable<RxMessage> GetNode(Enum nodeName)
        //{
        //    if (!_iObservables.ContainsKey(nodeName))
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        return _iObservables[nodeName];
        //    }
        //}

        //public bool ExistsNode(Enum nodeName)
        //{
        //    return _iObservables.ContainsKey(nodeName);
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace cononia.src.rx
{
    public class RxNode
    {
        private Subject<RxMessage> subject;
        //private Dictionary<Enum, >
        private List<IDisposable> _listenerDisposables;

        public RxNode()
        {
            subject = new Subject<RxMessage>();
            _listenerDisposables = new List<IDisposable>();
        }
        ~RxNode()
        {
            foreach(IDisposable listenerDisposable in _listenerDisposables)
            {
                listenerDisposable.Dispose();
            }
        }
        private IObservable<RxMessage> AsObservable
        {
            get
            {
                return subject;
            }
        }
        public void Publish(RxMessage message)
        {
            subject.OnNext(message);
        }

        public void RegisterEventListener(Action<RxMessage> listener)
        {
            // 리스너 추가 후 disposible 리스트에 등록
            IDisposable disposable = AsObservable.Subscribe(listener);
            _listenerDisposables.Add(disposable);
        }
    }
}

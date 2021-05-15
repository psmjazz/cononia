using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace cononia.src.rx
{
    public class RxNode
    {
        private Subject<MessageBase> subject;

        public RxNode()
        {
            subject = new Subject<MessageBase>();
        }
        public IObservable<MessageBase> AsObservable
        {
            get
            {
                return subject;
            }
        }
        public virtual void Publish(MessageBase message)
        {
            subject.OnNext(message);
        }

    }
}

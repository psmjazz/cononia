using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.common
{
    class Singleton<T> where T:  Singleton<T>, new()
    {
        private static T _instance;
        private bool _initialized = false;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new T();
                }
                _instance.Initialize();
                return _instance;
            }
        }

        public virtual void Initialize()
        {
            Debug.WriteLine("singletone init");
            _initialized = true;
            
        }

        //public bool IsInitialized()
        //{
        //    return _initialized;
        //}

        public bool Initialized
        {
            get
            {
                return _initialized;
            }
            protected set
            {
                _initialized = value;
            }
        }
    }
}

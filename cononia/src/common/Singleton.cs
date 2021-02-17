using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cononia.src.common
{
    abstract class Singleton<T> where T: class, new()
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

                return _instance;
            }
        }

        public abstract void Initialize();

        public bool IsInitialized()
        {
            return _initialized;
        }

        protected bool Initialized { get; set; }
    }
}

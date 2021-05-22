using cononia.src.common;
using System;
using System.Collections.Generic;
using System.Printing;
using System.Text;
using System.Windows.Controls;

namespace cononia.src.ui
{
    class PageLoader : Singleton<PageLoader>
    {
        private Dictionary<string, Page> Pages = null;
        public override void Initialize()
        {
            if(Initialized)
            {
                return;
            }
            base.Initialize();

            Pages = new Dictionary<string, Page>();
        }

        public Page GetPage<T>() where T : Page, new()
        {
            string typeName = typeof(T).Name;
            if(Pages.ContainsKey(typeName))
            {
                return Pages[typeName];
            }
            else
            {
                Pages[typeName] = new T();
                return Pages[typeName];
            }
        }
    }
}

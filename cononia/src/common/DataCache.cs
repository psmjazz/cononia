using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace cononia.src.common
{
    public class DataCache<TKey, TValue> where TKey: notnull
    {
        Dictionary<TKey, TValue> _cache;
        Queue<TKey> _cachedId;
        int _maxItem;

        public DataCache(int maximun)
        {
            _maxItem = maximun;
            _cache = new Dictionary<TKey, TValue>();
            _cachedId = new Queue<TKey>();
            
        }

        public bool InsertData(TKey id, TValue data)
        {
            if (_cache.ContainsKey(id))
                return false;
            else if(_maxItem > _cache.Count)
            {
                _cache[id] = data;
                _cachedId.Enqueue(id);
                return true;
            }
            else // _maxItem <= _cache.Count
            {
                _cache.Remove(_cachedId.Dequeue());

                _cache[id] = data;
                _cachedId.Enqueue(id);
                return true;
            }
        }

        public bool RemoveData(TKey id)
        {
            if (_cache.ContainsKey(id))
                return false;
            else
            {
                _cache.Remove(id);
                return true;
            }
        }


        public TValue GetData(TKey id)
        {
            if (_cache.ContainsKey(id))
                return _cache[id];
            else
                return default;
        }

        public bool HasData(TKey id)
        {
            return _cache.ContainsKey(id);
        }

    }
}

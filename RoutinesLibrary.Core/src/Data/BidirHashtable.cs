using System;
using System.Collections;

namespace RoutinesLibrary.Core.Data
{
    public class BidirHashtable
    {
        private Hashtable _htFwd = new Hashtable();
        private Hashtable _htBkwd = new Hashtable();

        static readonly object _locker = new object();


        public void Clear()
        {
            lock (_locker)
            {
                _htFwd.Clear();
                _htBkwd.Clear();
            }
        }

        public void Add(object key, object val)
        {
            lock (_locker)
            {
                _htFwd.Add(key, val);
                _htBkwd.Add(val, key);
            }
        }

        public void Remove(object key)
        {
            lock (_locker)
            {
                object val = _htFwd[key];
                _htFwd.Remove(key);
                _htBkwd.Remove(val);
            }
        }

        public void RemoveValue(object val)
        {
            lock (_locker)
            {
                object key = _htBkwd[val];
                _htFwd.Remove(key);
                _htBkwd.Remove(val);
            }
        }

        public dynamic Get(object key)
        {
            lock (_locker)
            {
                return _htFwd[key];
            }
        }

        public dynamic GetKey(object val)
        {
            lock (_locker)
            {
                return _htBkwd[val];
            }
        }

        public void Set(object key, object val)
        {
            lock (_locker)
            {
                _htFwd[key] = val;
                _htBkwd[val] = key;
            }
        }

        public bool Contains(object key)
        {
            lock (_locker)
            {
                return _htFwd.Contains(key);
            }
        }

        public bool ContainsValue(object val)
        {
            lock (_locker)
            {
                return _htBkwd.Contains(val);
            }
        }
    }
}
using System;
using System.Collections;

namespace RoutinesLibrary.Data
{
    public class TwoKeyHashtable
    {
        private Hashtable _internalTable = new Hashtable();


        public void Add(object key, object key2, object val)
        {
            Hashtable htRow = default(Hashtable);

            if (_internalTable.Contains(key))
            {
                htRow = (Hashtable) (_internalTable[key]);
            }
            else
            {
                htRow = new Hashtable();
                _internalTable[key] = htRow;
            }

            htRow[key2] = val;
        }

        public void Clear()
        {
            _internalTable.Clear();
        }

        public void Remove(object key, object key2)
        {
            if (_internalTable.Contains(key))
            {
                Hashtable htRow = (Hashtable) (_internalTable[key]);
                htRow.Remove(key2);

                //delete empty row
                if (htRow.Count == 0)
                {
                    _internalTable.Remove(key);
                }
            }
        }

        public void RemoveRow(object key)
        {
            _internalTable.Remove(key);
        }

        public int Count(object key)
        {
            if (_internalTable.Contains(key))
            {
                return ((Hashtable)_internalTable[key]).Count;
            }

            return 0;
        }

        public int CountRow()
        {
            return _internalTable.Count;
        }

        public dynamic Item(object key, object key2)
        {
            if (_internalTable.Contains(key))
            {
                if (((Hashtable) (_internalTable[key])).Contains(key2))
                {
                    return ((Hashtable) (_internalTable[key]))[key2];
                }
            }

            return null;
        }

        public bool Contains(object key, object key2)
        {
            if (_internalTable.Contains(key))
            {
                if (((Hashtable) (_internalTable[key])).Contains(key2))
                {
                    return true;
                }
            }

            return false;
        }

        public bool ContainsRow(object key)
        {
            return _internalTable.Contains(key);
        }

        public ICollection Keys()
        {
            return _internalTable.Keys;
        }

        public ICollection RowKeys(object key)
        {
            if (_internalTable.Contains(key))
            {
                return ((Hashtable) (_internalTable[key])).Keys;
            }

            return null;
        }
    }
}
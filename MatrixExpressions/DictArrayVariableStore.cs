using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    public class DictArrayVariableStore : IVariableStore
    {
        private Dictionary<string, int> _dictionary;
        private List<string> _ids;

        public DictArrayVariableStore()
        {
            _dictionary = new Dictionary<string, int>();
            _ids = new List<string>();
        }

        public int GetID(string name)
        {
            int id;
            if (_dictionary.TryGetValue(name, out id))
                return id;
            return -1;
        }

        public string GetName(int id)
        {
            if (id >= 0 && id < _ids.Count)
                return _ids[id];
            return "";
        }

        public int GetOrCreateID(string name)
        {
            int id;
            if (_dictionary.TryGetValue(name, out id))
                return id;
            id = _ids.Count;
            _ids.Add(name);
            _dictionary.Add(name, id);
            return id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    class Wrapper<T>
    {
        public T Value { get; set; }

        public Wrapper(T value)
        {
            Value = value;
        }
    }

    class ComparableWrapper<T> : Wrapper<T>, IComparable<ComparableWrapper<T>>
    {
        private static readonly Comparer<T> _comparer = Comparer<T>.Default;

        public ComparableWrapper(T value) : base(value) { }

        public int CompareTo(ComparableWrapper<T> other)
        {
            return _comparer.Compare(Value, other.Value);
        }
    }

    class KeyValuePairWrapper<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValuePairWrapper(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    class KeyValuePairComparableWrapper<TKey, TValue> : KeyValuePairWrapper<TKey, TValue>, IComparable<KeyValuePairComparableWrapper<TKey, TValue>>
    {
        private static readonly Comparer<TKey> _comparer = Comparer<TKey>.Default;

        public KeyValuePairComparableWrapper(TKey key, TValue value) : base(key, value) { }

        public int CompareTo(KeyValuePairComparableWrapper<TKey, TValue> other)
        {
            return _comparer.Compare(Key, other.Key);
        }
    }
}

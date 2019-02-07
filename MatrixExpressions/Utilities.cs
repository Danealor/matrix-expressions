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
}

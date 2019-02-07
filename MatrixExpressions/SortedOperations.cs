using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    static class SortedOperations
    {
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Func<T, T, T> mergeFunc) where T : IComparable<T>
        {
            return Merge(lhs, rhs, mergeFunc, (a, b) => a.CompareTo(b));
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Func<T, T, T> mergeFunc, IComparer<T> comparer)
        {
            return Merge(lhs, rhs, mergeFunc, comparer.Compare);
        }

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Func<T, T, T> mergeFunc, Comparison<T> comparison)
        {
            var lhsEnum = lhs.GetEnumerator();
            var rhsEnum = rhs.GetEnumerator();

            bool left = lhsEnum.MoveNext();
            bool right = rhsEnum.MoveNext();

            while (left || right)
            {
                if (left && right)
                {
                    T leftVal = lhsEnum.Current;
                    T rightVal = rhsEnum.Current;

                    int comp = comparison(leftVal, rightVal);
                    if (comp == 0)
                    {
                        yield return mergeFunc(leftVal, rightVal);
                        lhsEnum.MoveNext(); // arbitrary
                    }
                    else if (comp < 0)
                    {
                        yield return leftVal;
                        lhsEnum.MoveNext();
                    }
                    else
                    {
                        yield return rightVal;
                        rhsEnum.MoveNext();
                    }
                }
                else if (left)
                {
                    T leftVal = lhsEnum.Current;
                    yield return leftVal;
                    lhsEnum.MoveNext();
                }
                else
                {
                    T rightVal = rhsEnum.Current;
                    yield return rightVal;
                    rhsEnum.MoveNext();
                }
            }
        }
        
        public static IEnumerable<T> MergeMany<T>(IEnumerable<IEnumerable<T>> lists, Func<T, T, T> mergeFunc) where T : IComparable<T>
        {
            var enumerators = lists.Select(list => list.GetEnumerator()).ToArray();
            bool[] hasCur = new bool[enumerators.Length];
            int numRunning = enumerators.Length;
            var BST = new SortedSet<KeyValuePairComparableWrapper<T, List<int>>>();

            while (numRunning > 0)
            {
                var top = BST.First();
                BST.Remove(top);
                yield return top.Key;

                foreach (int i in top.Value)
                {
                    if (enumerators[i].MoveNext())
                    {
                        var val = enumerators[i].Current;
                        BST.
                    }
                }

                if (left && right)
                {
                    T leftVal = lhsEnum.Current;
                    T rightVal = rhsEnum.Current;

                    int comp = comparison(leftVal, rightVal);
                    if (comp == 0)
                    {
                        yield return mergeFunc(leftVal, rightVal);
                        lhsEnum.MoveNext(); // arbitrary
                    }
                    else if (comp < 0)
                    {
                        yield return leftVal;
                        lhsEnum.MoveNext();
                    }
                    else
                    {
                        yield return rightVal;
                        rhsEnum.MoveNext();
                    }
                }
                else if (left)
                {
                    T leftVal = lhsEnum.Current;
                    yield return leftVal;
                    lhsEnum.MoveNext();
                }
                else
                {
                    T rightVal = rhsEnum.Current;
                    yield return rightVal;
                    rhsEnum.MoveNext();
                }
            }
        }

        public static int CompareTo<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs) where T : IComparable<T>
        {
            return CompareTo(lhs, rhs, (a, b) => a.CompareTo(b));
        }

        public static int CompareTo<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, IComparer<T> comparer)
        {
            return CompareTo(lhs, rhs, comparer.Compare);
        }

        public static int CompareTo<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Comparison<T> comparison)
        {
            var lhsEnum = lhs.GetEnumerator();
            var rhsEnum = rhs.GetEnumerator();
            
            bool left = lhsEnum.MoveNext();
            bool right = rhsEnum.MoveNext();

            while (left && right)
            {
                int comp = comparison(lhsEnum.Current, rhsEnum.Current);
                if (comp != 0)
                    return comp;

                left = lhsEnum.MoveNext();
                right = rhsEnum.MoveNext();
            }

            if (right)
                return -1;
            if (left)
                return 1;
            return 0;
        }

        public static int CompareToNullDense<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, IComparer<T?> comparer, IComparer<T> positionalComparer) where T : struct
        {
            return CompareToNullDense(lhs, rhs, comparer.Compare, positionalComparer.Compare);
        }

        public static int CompareToNullDense<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Comparison<T?> comparison, IComparer<T> positionalComparer) where T : struct
        {
            return CompareToNullDense(lhs, rhs, comparison, positionalComparer.Compare);
        }

        public static int CompareToNullDense<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, IComparer<T?> comparer, Comparison<T> positionalComparison) where T : struct
        {
            return CompareToNullDense(lhs, rhs, comparer.Compare, positionalComparison);
        }

        private static int CompareToNullDense<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Comparison<T?> comparison, Comparison<T> positionalComparison) where T : struct
        {
            var lhsEnum = lhs.GetEnumerator();
            var rhsEnum = rhs.GetEnumerator();

            bool left = lhsEnum.MoveNext();
            bool right = rhsEnum.MoveNext();

            while (left || right)
            {
                T leftVal = lhsEnum.Current;
                T rightVal = rhsEnum.Current;

                int comp = 0;
                if (left && right)
                    comp = positionalComparison(leftVal, rightVal);

                if (comp == 0)
                {
                    comp = comparison(left ? (T?)leftVal : null, right ? (T?)rightVal : null);
                    if (comp != 0)
                        return comp;

                    if (left)
                        left = lhsEnum.MoveNext();
                    if (right)
                        right = rhsEnum.MoveNext();
                }
                else if (comp < 0)
                    return comparison(leftVal, null);
                else
                    return comparison(null, rightVal);
            }
            
            return 0;
        }
    }
}

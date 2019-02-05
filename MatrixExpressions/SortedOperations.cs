using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    static class SortedOperations
    {
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> lhs, IEnumerable<T> rhs, Func<T, T, T> mergeFunc) where T : IComparable<T>
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

                    int comp = lhsEnum.Current.CompareTo(rhsEnum.Current);
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
    }
}

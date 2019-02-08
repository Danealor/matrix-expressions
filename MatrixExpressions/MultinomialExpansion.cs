using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    static class MultinomialExpansion
    {
        public static IEnumerable<int[]> Combinations(int m, int n)
        {
            int[] counts = new int[n+1];
            for (int i = 0; i <= n; i++)
                counts[i] = 0;

            int occurences = 0;
            int outstanding = n;
            int marker = n + 1;
            Stack<int> markers = new Stack<int>();
            while (true)
            {
                while (markers.Count > 0)
                {
                    marker = markers.Peek();

                    if (marker == 1 || counts[marker] == 1)
                    {
                        markers.Pop();
                        outstanding += marker * counts[marker];
                        occurences -= counts[marker];
                        counts[marker] = 0;
                    }
                    else
                    {
                        outstanding += marker;
                        occurences--;
                        counts[marker]--;
                        break;
                    }
                }

                if (marker == 1) break;

                while (outstanding > 0)
                {
                    marker--;
                    if (outstanding > marker)
                    {
                        int count = outstanding / marker;
                        outstanding -= count * marker;
                        occurences += count;
                        counts[marker] = count;
                    }
                    else
                    {
                        marker = outstanding;
                        outstanding = 0;
                        occurences++;
                        counts[marker] = 1;
                    }
                    markers.Push(marker);
                }

                if (occurences <= m) // this can maybe be done better (how far to backtrack?)
                {
                    counts[0] = m - occurences;
                    yield return counts;
                }
            }
        }
    }
}

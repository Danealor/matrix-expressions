using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    static class MultinomialExpansion
    {
        public IEnumerable<int[]> Combinations(int m, int n)
        {
            int[] counts = new int[n+1];
            for (int i = 0; i < n; i++)
                counts[i] = 0;
            counts[n] = 1;

            int occurences = 0;
            Stack<int> markers = new Stack<int>();
            markers.Push(n);
            while (true)
            {
                yield return counts;

                int marker = markers.Peek();
                counts[marker]--;

                if (counts[marker] == 0)
                    markers.Pop();

                int outstanding = marker--; // amount we have to make up
                counts[marker] = marker == 1 ? 2 : 1;

                while (true)
                {
                    
                    if (marker == 0 || n / marker)
                }


                while (outstanding > 0)
                {
                    marker = outstanding / 
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    static class MultinomialExpansion
    {
        // Switch to optimized Atkin's sieve when that is fixed
        private static LimitedPrimeSieve _primes = new EratosthenesSieve();

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

        // N choose K - O(k)
        public static long BinomialCoefficient(int n, int k)
        {

            long r = 1;
            if (k > n / 2) return BinomialCoefficient(n, n - k);
            if (k > n) return 0;
            for (uint d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }

        private static IEnumerable<int> Digits(int num, int pBase)
        {
            while (num > 0)
            {
                yield return num % pBase;
                num /= pBase;
            }
        }

        public static int MultinomialKummerValuation(int p, int n, IReadOnlyCollection<int> m)
        {
            return (m.Select(mi => Digits(mi, p).Sum()).Sum() - Digits(n, p).Sum()) / (p - 1);
        }

        public static double MultinomialCoefficientKummer(int n, IReadOnlyCollection<int> m)
        {
            double result = 1;
            foreach (int p in _primes.TakeUpTo(n))
                result *= Math.Pow(p, MultinomialKummerValuation(p, n, m));
            return result;
        }

        public static double MultinomialCoefficient(int n, IReadOnlyCollection<int> m)
        {
            double result = 1;
            int sum = 0;
            foreach (int mi in m)
                result *= BinomialCoefficient(sum += mi, mi);
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    public class Matrix
    {
        // NOTE: NOT Read-Only
        protected Expression[,] Elements { get; }

        public Expression this[int row, int column]
        {
            get => Elements[row, column];
            set => Elements[row, column] = value;
        }
        
        public Matrix(int rows, int columns)
        {
            Elements = new Expression[rows, columns];
        }

        public static Matrix Identity(int size)
        {
            Matrix matrix = new Matrix(size, size);
            for (int i = 0; i < size; i++)
                matrix[i, i] = 1;
            return matrix;
        }

        public string ToString(IVariableStore store)
        {
        }

        public override string ToString()
        {
        }

        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            int di = lhs.Elements.GetLength(0);
            int dj = rhs.Elements.GetLength(1);
            int dr = lhs.Elements.GetLength(1);
            if (dr != rhs.Elements.GetLength(0))
                throw new ArgumentException("Matrix dimensions don't match.");

            Matrix result = new Matrix(di, dj);
            for (int i = 0; i < di; i++)
                for (int j = 0; j < dj; j++)
                    result[i, j] = Enumerable.Range(0, dr).Select(r => lhs.Elements[i, r] * rhs.Elements[r, j]).Sum();

            return result;
        }

        public static Matrix operator +(Matrix lhs, Matrix rhs)
        {
            int di = lhs.Elements.GetLength(0);
            int dj = rhs.Elements.GetLength(1);
            return new Expression(SortedOperations.Merge(lhs.Terms, rhs.Terms, MergeTerm).Where(term => term.Coefficient != 0).ToArray());
        }

        public static Expression operator -(Expression lhs, Term rhs)
        {
            return lhs - (Expression)rhs;
        }

        public static Expression operator -(Term lhs, Expression rhs)
        {
            return (Expression)lhs - rhs;
        }

        public static Expression operator -(Expression lhs, Expression rhs)
        {
            return new Expression(SortedOperations.Merge(lhs.Terms, rhs.Terms.Select(term => -term), MergeTerm).Where(term => term.Coefficient != 0).ToArray());
        }

        public static Expression operator +(Expression expression)
        {
            return expression; // no need for deep (or even shallow) copy because this is read-only, by definition!
        }

        public static Expression operator -(Expression expression)
        {
            return new Expression(expression.Terms.Select(term => -term).ToArray());
        }

        public static Expression Sum(IEnumerable<Expression> expressions)
        {
            return new Expression(SortedOperations.MergeMany(expressions.Select(expr => expr.Terms), MergeTerm).Where(term => term.Coefficient != 0).ToArray());
        }
    }
}

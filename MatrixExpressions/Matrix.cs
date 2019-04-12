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
            int di = Elements.GetLength(0);
            int dj = Elements.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < di; i++)
            {
                for (int j = 0; j < dj; j++)
                {
                    string sExp = Elements[i, j].ToString(store);
                    sb.Append(sExp);
                    if (j < dj - 1)
                        sb.Append(',');
                }
                if (i < di - 1)
                    sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            int di = Elements.GetLength(0);
            int dj = Elements.GetLength(1);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < di; i++)
            {
                for (int j = 0; j < dj; j++)
                {
                    string sExp = Elements[i, j].ToString();
                    sb.Append(sExp);
                    if (j < dj - 1)
                        sb.Append(',');
                }
                if (i < di - 1)
                    sb.Append(Environment.NewLine);
            }
            return sb.ToString();
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
            int dj = lhs.Elements.GetLength(1);

            if (di != rhs.Elements.GetLength(0) || dj != rhs.Elements.GetLength(1))
                throw new ArgumentException("Matrix dimensions don't match.");
            
            Matrix result = new Matrix(di, dj);
            for (int i = 0; i < di; i++)
                for (int j = 0; j < dj; j++)
                    result[i, j] = lhs[i, j] + rhs[i, j];

            return result;
        }

        public static Matrix operator -(Matrix lhs, Matrix rhs)
        {
            int di = lhs.Elements.GetLength(0);
            int dj = lhs.Elements.GetLength(1);

            if (di != rhs.Elements.GetLength(0) || dj != rhs.Elements.GetLength(1))
                throw new ArgumentException("Matrix dimensions don't match.");

            Matrix result = new Matrix(di, dj);
            for (int i = 0; i < di; i++)
                for (int j = 0; j < dj; j++)
                    result[i, j] = lhs[i, j] - rhs[i, j];

            return result;
        }

        public static Matrix operator +(Matrix matrix)
        {
            int di = matrix.Elements.GetLength(0);
            int dj = matrix.Elements.GetLength(1);

            Matrix result = new Matrix(di, dj);
            for (int i = 0; i < di; i++)
                for (int j = 0; j < dj; j++)
                    result[i, j] = matrix[i, j];

            return result;
        }

        public static Matrix operator -(Matrix matrix)
        {
            int di = matrix.Elements.GetLength(0);
            int dj = matrix.Elements.GetLength(1);

            Matrix result = new Matrix(di, dj);
            for (int i = 0; i < di; i++)
                for (int j = 0; j < dj; j++)
                    result[i, j] = -matrix[i, j];

            return result;
        }
    }
}

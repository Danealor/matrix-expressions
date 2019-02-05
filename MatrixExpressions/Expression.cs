using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    public class Expression
    {
        public static Expression operator *(Term lhs, Expression rhs)
        {
            return rhs * lhs; // Commutative
        }

        public static Expression operator *(Expression lhs, Term rhs)
        {
        }

        public static Expression operator *(Expression lhs, Expression rhs)
        {
        }

        public static Expression operator /(Expression lhs, Term rhs)
        {
        }

        public static Expression operator +(Expression lhs, Expression rhs)
        {
        }

        public static Expression operator -(Expression lhs, Expression rhs)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    public struct Expression
    {
        public IReadOnlyCollection<Term> Terms { get; }

        public Expression(IReadOnlyCollection<Term> terms)
        {
            Terms = terms;
        }

        public static implicit operator Expression(Term term)
        {
            return new Expression(new Term[] { term });
        }

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

        public static Expression operator +(Expression expression)
        {
            return expression; // no need for deep (or even shallow) copy because this is read-only, by definition!
        }

        public static Expression operator -(Expression expression)
        {
            return new Expression(expression.Terms.Select(term => -term).ToArray());
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    public struct Expression
    {
        public IReadOnlyCollection<Term> Terms { get; }

        // CONDITION: No duplicates in 'terms' under CompareTo
        public Expression(IReadOnlyCollection<Term> terms)
        {
            Terms = terms;
        }

        public static implicit operator Expression(Term term)
        {
            return new Expression(new Term[] { term });
        }

        public static implicit operator Expression(Variable variable)
        {
            return variable;
        }

        private static Term MergeTerm(Term lhs, Term rhs)
        {
            return new Term(lhs.Coefficient + rhs.Coefficient, lhs.Variables);
        }

        public static Expression operator *(Term lhs, Expression rhs)
        {
            return rhs * lhs; // Commutative
        }

        public static Expression operator *(Expression lhs, Term rhs)
        {
            return new Expression(lhs.Terms.Select(term => term * rhs).ToArray());
        }

        public static Expression operator *(Expression lhs, Expression rhs)
        {
            if (rhs.Terms.Count < lhs.Terms.Count) return rhs * lhs; // minimize parallel width (minimize r)
            // return rhs.Terms.Select(term => lhs * term).Aggregate((a,b) => a + b); // Simple but inefficient
            return new Expression(lhs.Terms.Select(lterm => rhs.Terms.Select(rterm => lterm * rterm)).MergeMany(MergeTerm).ToArray());
        }

        public static Expression operator /(Expression lhs, Term rhs)
        {
        }

        public static Expression operator +(Expression lhs, Term rhs)
        {
            return lhs + rhs;
        }

        public static Expression operator +(Term lhs, Expression rhs)
        {
            return lhs + rhs;
        }

        public static Expression operator +(Expression lhs, Expression rhs)
        {
            return new Expression(SortedOperations.Merge(lhs.Terms, rhs.Terms, MergeTerm).Where(term => term.Coefficient != 0).ToArray());
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
    }
}

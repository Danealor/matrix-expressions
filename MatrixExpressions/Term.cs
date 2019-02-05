using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    public struct Term
    {
        public double Coefficient { get; }
        public IReadOnlyCollection<Variable> Variables { get; }

        public Term(double constant)
        {
            Coefficient = constant;
            Variables = new Variable[] { };
        }

        public Term(IReadOnlyCollection<Variable> variables)
        {
            Coefficient = 1.0;
            Variables = variables;
        }

        public Term(double coefficient, IReadOnlyCollection<Variable> variables)
        {
            Coefficient = coefficient;
            Variables = variables;
        }

        public static implicit operator Term(double constant)
        {
            return new Term(constant);
        }

        public static implicit operator Term(Variable variable)
        {
            return new Term(new Variable[] { variable });
        }

        private static Variable MergeVariable(Variable lhs, Variable rhs)
        {
            return new Variable(lhs.ID, lhs.Exponent + rhs.Exponent);
        }

        public static Term operator *(Term lhs, Term rhs)
        {
            return new Term(lhs.Coefficient * rhs.Coefficient, SortedOperations.Merge(lhs.Variables, rhs.Variables, MergeVariable).ToArray());
        }

        public static Term operator /(Term lhs, Term rhs)
        {
            return new Term(lhs.Coefficient / rhs.Coefficient, SortedOperations.Merge(lhs.Variables, rhs.Variables.Select(var => ~var), MergeVariable).ToArray());
        }

        public static Term operator +(Term term)
        {
            return term; // no need for deep (or even shallow) copy because this is read-only, by definition!
        }

        public static Term operator -(Term term)
        {
            return new Term(-term.Coefficient, term.Variables); // no need for deep copy because this is read-only, by definition!
        }

        public static Term operator ~(Term term)
        {
            return 1.0 / term;
        }
    }
}

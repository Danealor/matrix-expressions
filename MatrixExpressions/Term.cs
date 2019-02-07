using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrixExpressions
{
    public struct Term : IComparable<Term>
    {
        public double Coefficient { get; }
        public IReadOnlyCollection<Variable> Variables { get; }

        private static Variable.BaseComparer _baseComparer = new Variable.BaseComparer();
        private static Variable.NullableComparer _nullComparer = new Variable.NullableComparer();

        public Term(double constant)
        {
            Coefficient = constant;
            Variables = new Variable[] { };
        }
        
        // CONDITION: No duplicates in 'variables' under Variable.BaseComparer
        public Term(IReadOnlyCollection<Variable> variables)
        {
            Coefficient = 1.0;
            Variables = variables;
        }

        // CONDITION: No duplicates in 'variables' under Variable.BaseComparer
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

        public int CompareTo(Term other)
        {
            return Variables.CompareToNullDense(other.Variables, _nullComparer, _baseComparer);
        }

        public string ToString(IVariableStore store)
        {
            if (Variables.Count == 0) return Coefficient.ToString();
            StringBuilder sb = new StringBuilder();
            if (Coefficient != 1)
            {
                if (Coefficient == -1)
                    sb.Append('-');
                else
                    sb.Append(Coefficient);
            }
            foreach (var variable in Variables)
                sb.Append(variable.ToString(store));
            return sb.ToString();
        }

        public override string ToString()
        {
            if (Variables.Count == 0) return Coefficient.ToString();
            StringBuilder sb = new StringBuilder();
            if (Coefficient != 1)
            {
                if (Coefficient == -1)
                    sb.Append('-');
                else
                    sb.Append(Coefficient);
            }
            foreach (var variable in Variables)
                sb.Append(variable.ToString());
            return sb.ToString();
        }

        private static Variable MergeVariable(Variable lhs, Variable rhs)
        {
            return new Variable(lhs.ID, lhs.Exponent + rhs.Exponent);
        }

        public static Term operator *(Term lhs, Variable rhs)
        {
            return lhs * (Term)rhs;
        }

        public static Term operator *(Variable lhs, Term rhs)
        {
            return (Term)lhs * rhs;
        }

        public static Term operator *(Term lhs, Term rhs)
        {
            return new Term(lhs.Coefficient * rhs.Coefficient, SortedOperations.Merge(lhs.Variables, rhs.Variables, MergeVariable, _baseComparer).Where(var => var.Exponent != 0).ToArray());
        }

        public static Term operator /(Term lhs, Variable rhs)
        {
            return lhs / (Term)rhs;
        }

        public static Term operator /(Variable lhs, Term rhs)
        {
            return (Term)lhs / rhs;
        }

        public static Term operator /(Term lhs, Term rhs)
        {
            return new Term(lhs.Coefficient / rhs.Coefficient, SortedOperations.Merge(lhs.Variables, rhs.Variables.Select(var => ~var), MergeVariable, _baseComparer).Where(var => var.Exponent != 0).ToArray());
        }

        public static Expression operator +(Term lhs, Variable rhs)
        {
            return lhs + (Expression)rhs;
        }

        public static Expression operator +(Variable lhs, Term rhs)
        {
            return (Expression)lhs + rhs;
        }

        public static Expression operator -(Term lhs, Variable rhs)
        {
            return lhs - (Expression)rhs;
        }

        public static Expression operator -(Variable lhs, Term rhs)
        {
            return (Expression)lhs - rhs;
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

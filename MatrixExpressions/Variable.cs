using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    public struct Variable : IComparable<Variable>
    {
        public int ID { get; }
        public int Exponent { get; }

        public Variable(int id)
        {
            ID = id;
            Exponent = 1;
        }

        public Variable(string name, IVariableStore store)
        {
            ID = store.GetOrCreateID(name);
            Exponent = 1;
        }
        
        public Variable(int id, int exponent)
        {
            ID = id;
            Exponent = exponent;
        }

        public Variable(string name, IVariableStore store, int exponent)
        {
            ID = store.GetOrCreateID(name);
            Exponent = exponent;
        }

        public int CompareTo(Variable other)
        {
            int comp = ID.CompareTo(other.ID);
            if (comp != 0)
                return comp;
            return Exponent.CompareTo(other.Exponent);
        }

        private string ToString(string varName)
        {
            if (string.IsNullOrWhiteSpace(varName))
                varName = "?";
            if (Exponent == 1)
                return varName;
            return varName + "^" + Exponent;
        }

        public string ToString(IVariableStore store)
        {
            return ToString(store.GetName(ID));
        }

        public override string ToString()
        {
            return ToString("{" + ID + "}");
        }

        public static Term operator *(Variable lhs, Variable rhs)
        {
            return (Term)lhs * rhs;
        }

        public static Term operator /(Variable lhs, Variable rhs)
        {
            return (Term)lhs / rhs;
        }

        public static Expression operator +(Variable lhs, Variable rhs)
        {
            return (Expression)lhs + rhs;
        }

        public static Expression operator -(Variable lhs, Variable rhs)
        {
            return (Expression)lhs - rhs;
        }

        public static Variable operator ^(Variable variable, int exponent)
        {
            return new Variable(variable.ID, variable.Exponent * exponent);
        }

        public static Variable operator ~(Variable variable)
        {
            return new Variable(variable.ID, -variable.Exponent);
        }

        public Variable Pow(int exponent)
        {
            return this ^ exponent;
        }

        public class BaseComparer : Comparer<Variable>
        {
            public override int Compare(Variable x, Variable y)
            {
                return x.ID.CompareTo(y.ID);
            }
        }

        // Allows variable sorting order to be stable (preserved) under multiplication
        public class NullableComparer : Comparer<Variable?>
        {
            public override int Compare(Variable? x, Variable? y)
            {
                if (x == null && y == null) return 0;
                Variable lhs = x ?? new Variable(y.Value.ID, 0);
                Variable rhs = y ?? new Variable(x.Value.ID, 0);
                return lhs.CompareTo(rhs);
            }
        }
    }
}

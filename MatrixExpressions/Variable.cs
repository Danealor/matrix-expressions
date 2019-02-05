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
            return ID.CompareTo(other.ID);
        }

        public static Variable operator ~(Variable variable)
        {
            return new Variable(variable.ID, -variable.Exponent);
        }
    }
}

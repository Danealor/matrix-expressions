using System;
using System.Collections.Generic;
using System.Text;

namespace MatrixExpressions
{
    public interface IVariableStore
    {
        int GetID(string name);
        int GetOrCreateID(string name);
    }
}

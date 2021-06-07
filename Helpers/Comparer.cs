using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RstateAPI.Entities;

namespace RstateAPI.Helpers
{
    public class Comparer: IEqualityComparer<SaveModel>
    {  
        public bool Equals(SaveModel x, SaveModel y)
        {
            return x.uniqueID != y.uniqueID;
        }

        

        public int GetHashCode([DisallowNull] SaveModel obj)
        {
            throw new System.NotImplementedException();
        }
    }  
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata4
{
    public interface IDictionaryReader
    {
        IEnumerable<string> GetDictionary();
    }
}

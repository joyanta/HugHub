using System.Collections.Generic;
using System.Dynamic;
namespace ConsoleApp1
{
    public static class Lib
    {
        // https://stackoverflow.com/questions/2998954/test-if-a-property-is-available-on-a-dynamic-variable/5768449
        public static bool HasProperty(ExpandoObject expandoObj, string name)
        {
            return ((IDictionary<string, object>)expandoObj).ContainsKey(name);
        }

    }
}

using System;
using System.Collections.Generic;

namespace Common.Tools
{
    public class DictTool
    {
        public static T2 GetValue<T1, T2>(Dictionary<T1, T2> dict, T1 key)
        {
            T2 value;
            bool isSuccess = dict.TryGetValue(key, out value);

            return isSuccess ? value : default(T2);
        }
    }
}

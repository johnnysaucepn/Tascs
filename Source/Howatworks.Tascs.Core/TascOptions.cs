using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Howatworks.Tascs.Core
{
    public class TascOptions<T> : Dictionary<T, string>
    {
        public new string this[T key]
        {
            get
            {
                if (ContainsKey(key)) return base[key];
                var fieldInfo = typeof (T).GetField(key.ToString());
                var attrs =
                    (DefaultValueAttribute[]) (fieldInfo.GetCustomAttributes(typeof (DefaultValueAttribute), false));
                return attrs.Length < 1 ? null : attrs[0].Value.ToString();
            }

            set { base[key] = value; }
        }

        public static TascOptions<T> Merge(params TascOptions<T>[] optionsList)
        {
            var mergedOptions = new TascOptions<T>();

            foreach (var option in optionsList.SelectMany(partOptions => partOptions))
            {
                mergedOptions[option.Key] = option.Value;
            }
            return mergedOptions;

        }
    }
}
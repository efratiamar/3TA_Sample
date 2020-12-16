using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public static class Tools
    {
        public static void Clone<T, S>(this S from, T to)
        {
            foreach (PropertyInfo prop in from.GetType().GetProperties())
            {
                if (to.GetType().GetProperty(prop.Name) == null)
                    continue;
                var value = prop.GetValue(from, null);
                if (value is IEnumerable)
                {
                    //foreach (var item in (IEnumerable)value)
                    //    str += item.ToStringProperty("   ");
                }
                else if (value is ValueType)
                    prop.SetValue(to, value);
                else
                {
                    var refer = Activator.CreateInstance(value.GetType());
                    value.Clone(refer);
                    prop.SetValue(to, refer);
                }
            }
        }
    }
}

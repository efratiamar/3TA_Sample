using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public static class Tools
    {
        public static string ToStringProperty<T>(this T t, string suffix = "")
        {
            string str = "";
            foreach (PropertyInfo prop in t.GetType().GetProperties())
                if (prop.PropertyType is IEnumerable)
                {
                    foreach (var item in (IEnumerable)prop.GetValue(t, null))
                        str += item.ToStringProperty("   ");
                }
                else
                    str += "\n" + suffix + prop.Name + ": " + prop.GetValue(t, null);
            return str;
        }
    }
}

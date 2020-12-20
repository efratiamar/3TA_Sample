using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public static class DeepCopyUtilities
    {
        // It is not flat object - need recursion
        public static void DeepCopyTo<S, T>(this S from, T to)
        {
            var fromType = from.GetType();
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = fromType.GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
                else
                {
                    if (value == null)
                        continue;
                    var target = propTo.GetValue(to, null);
                    //if (target == null)
                    //    target = Activator.CreateInstance(propTo.PropertyType);

                    // If the property is a collection...
                    if (value is IEnumerable)
                    {
                        Type itemType = propTo.PropertyType.GetGenericArguments()[0];
                        propTo.PropertyType.GetMethod("Clear").Invoke(target, null);
                        foreach (var item in (value as IEnumerable))
                        {
                            var targetItem = Activator.CreateInstance(itemType);
                            item.DeepCopyTo(targetItem);
                            propTo.PropertyType.GetMethod("Add").Invoke(target, new object[] { targetItem });
                        }
                    }
                    else
                        value.DeepCopyTo(target);
                }
            }
        }
    }
}

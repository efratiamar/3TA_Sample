using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Tools
    {
        public static void Clone<T, S>(this S from, T to)
        {
            foreach (PropertyInfo propTo in to.GetType().GetProperties())
            {
                PropertyInfo propFrom = from.GetType().GetProperty(propTo.Name);
                if (propFrom == null)
                    continue;
                var value = propFrom.GetValue(from, null);
                if (value is ValueType || value is string)
                    propTo.SetValue(to, value);
            }
        }
        public static object CloneNew<S>(this S from, Type type)
        {
            object to = Activator.CreateInstance(type);
            from.Clone(to);
            return to;
        }
        public static BO.StudentCourse CloneToStudentCourse(this DO.Course course, DO.StudentInCourse sic)
        {
            BO.StudentCourse result = (BO.StudentCourse)course.CloneNew(typeof(BO.StudentCourse));
            // propertys' names changed? copy them here...
            result.Grade = sic.Grade;
            return result;
        }
    }
}

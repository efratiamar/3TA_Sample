using System;
using System.Reflection;
//using DO;

namespace DL
{
    static class Cloning
    {
        //first way - No Bonus
        //do it for each DO entity
        internal static DO.Person Clone(this DO.Person original)
        {
            DO.Person target = new DO.Person();
            target.ID = original.ID;
            target.Name = target.Name;
            //...
            return target;
        }

        //second way - With Bonus
        //cretae empty interface named IClonable in the DLAPI project
        //namespace DO
        //{
        //        public interface IClonable { }
        //}
        //add this method here in DLObject project
        //internal static IClonable Clone(this IClonable original)
        //{
        //    IClonable target = (IClonable)Activator.CreateInstance(original.GetType());
        //    //...
        //    return target;
        //}

        //third way - With Bonus
        internal static T Clone<T>(this T original)
        {
            T target = (T)Activator.CreateInstance(original.GetType());
            //...
            //foreach (PropertyInfo item in original.GetType().GetProperties())
            //    target.GetType().GetProperty(item.Name).SetValue(????, item.GetValue(original, null));

            return target;
        }


    }
}

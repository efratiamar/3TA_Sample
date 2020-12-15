using System;
using System.Reflection;

namespace DLAPI
{
    /// <summary>
    /// Static Factory class for creating DL tier implementation object according to
    /// configuration in file config.xml
    /// </summary>
    public static class DLFactory
    {
        /// <summary>
        /// The function creates DL tier implementation object according to DL type
        /// as appears in "dl" element in the configuration file config.xml.<br/>
        /// The configuration file also includes element "dl-packages" with list
        /// of available packages (.dll files) per DL type.<br/>
        /// Each DL package must use either "DL" namespace  or namespace per config
        /// attribute for the DL type and it must include internal access
        /// singleton class with the same name as package's name.<br/>
        /// The singleton class must include public static property called "Instance"
        /// which must contain the single instance of the class.
        /// </summary>
        /// <returns>DL tier implementation object</returns>
        public static IDL GetDL()
        {
            // get DL implementation name from config.xml according to <data> element
            string dlType = DLConfig.DLName;
            // bring package name (dll file name) for the DL name (above) from the list of packages in config.xml
            DLConfig.Package dlPackage = DLConfig.DLPackages[dlType];
            // if package name is not found in the list - there is a problem in config.xml
            if (dlPackage == null)
                throw new DLConfigException($"Wrong DL type: {dlType}");

            try // Load into CLR the DL implementation assembly according to dll file name (taken above)
            {
                Assembly.Load(dlPackage.Class);
            }
            catch (Exception ex)
            {
                throw new DLConfigException($"Failed loading {dlPackage.Class}.dll", ex);
            }

            // Get concrete DL implementation's class metadata object
            // 1st element in the list inside the string is full class name: namespace="DL", class name = package name
            //    the last requirement (class name = package name) is not mandatory in general - but this is the way it
            //    is configured per the implementation here, otherwise we'd need to add class name in addition to package
            //    name in the config.xml file - which is clearly a good option.
            //    NB: the class may not be public - it will still be found... Our approach that the implemntation class
            //        should hold "internal" access permission (which is actually the default access permission)
            // 2nd element is the package name = assembly name (as above)
            Type type = Type.GetType($"{dlPackage.NameSpace}.{dlPackage.Class}, {dlPackage.Class}");
            // If the type is not found - the implementation is not correct - it looks like the class name is wrong...
            if (type == null)
                throw new DLConfigException($"Class name is not the same as Assembly Name: {dlPackage.Class}");

            // *** Get concrete DL implementation's Instance
            // Get property info for public static property named "Instance" (in the DL implementation class- taken above)
            // If the property is not found or it's not public or not static then it is not properly implemented
            // as a Singleton...
            // Get the value of the property Instance (get function is automatically called by the system)
            // Since the property is static - the object parameter is irrelevant for the GetValue() function and we can use null
            try
            {
                IDL dl = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static).GetValue(null) as IDL;
                // If the instance property is not initialized (i.e. it does not hold a real instance reference)...
                if (dl == null)
                    throw new DLConfigException($"Class {dlPackage.Class} instance is not initialized");
                // now it looks like we have appropriate DL implementation instance :-)
                return dl;
            }
            catch (NullReferenceException ex)
            {
                throw new DLConfigException($"Class {dlPackage.Class} is not a singleton", ex);
            }

        }
    }
}

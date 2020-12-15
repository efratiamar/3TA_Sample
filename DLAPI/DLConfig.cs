using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DLAPI
{
    /// <summary>
    /// Class for processing config.xml file and getting from there
    /// information which is relevant for initialization of DalApi
    /// </summary>
    static class DLConfig
    {
        public class Package
        {
            public string Name;
            public string NameSpace;
            public string Class;
        }
        internal static string DLName;
        internal static Dictionary<string, Package> DLPackages;

        /// <summary>
        /// Static constructor extracts Dal packages list and Dal type from
        /// Dal configuration file config.xml
        /// </summary>
        static DLConfig()
        {
            XElement dalConfig = XElement.Load(@"config.xml");
            DLName = dalConfig.Element("DL").Value;
            DLPackages = (from pkg in dalConfig.Element("DL-packages").Elements()
                           let temp = pkg.Attribute("namespace").Value
                           let ns = temp == null ? "DL" : temp
                           select new Package() { Name = $"{pkg.Name}", Class = pkg.Value, NameSpace = ns })
                           .ToDictionary(p => "" + p.Name, p => p);
        }
    }

    /// <summary>
    /// Represents errors during DalApi initialization
    /// </summary>
    [Serializable]
    public class DLConfigException : Exception
    {
        public DLConfigException(string message) : base(message) { }
        public DLConfigException(string message, Exception inner) : base(message, inner) { }
    }
}

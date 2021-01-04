using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    [Serializable]
    public class BadPersonIdException : Exception
    {
        public int ID;
        public BadPersonIdException(int id) : base() => ID = id;
        public BadPersonIdException(int id, string message) :
            base(message) => ID = id;
        public BadPersonIdException(int id, string message, Exception innerException) : 
            base(message, innerException) => ID = id;
      
        public override string ToString() => base.ToString() + $", bad person id: {ID}";
    }

    public class BadPersonIdCourseIDException : Exception
    {
        public int personID;
        public int courseID;
        public BadPersonIdCourseIDException(int perID, int crsID) : base() {personID = perID; courseID = crsID;}  
        public BadPersonIdCourseIDException(int perID, int crsID, string message) :
            base(message) { personID = perID; courseID = crsID; }
        public BadPersonIdCourseIDException(int perID, int crsID, string message, Exception innerException) :
            base(message, innerException) { personID = perID; courseID = crsID; }

        public override string ToString() => base.ToString() + $", bad person id: {personID} and course id: {courseID}";
    }

    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath;}

        public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
    }

}

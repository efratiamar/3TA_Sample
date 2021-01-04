﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using DO;

namespace BO
{
    [Serializable]
    public class BadStudentIdException : Exception
    {
        public int ID;
        public BadStudentIdException(string message, Exception innerException) : 
            base(message, innerException) => ID = ((DO.BadPersonIdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad student id: {ID}";
    }

    [Serializable]
    public class BadLecturerIdException : Exception
    {
        public int ID;
        public BadLecturerIdException(string message, Exception innerException) : 
            base(message, innerException) => ID = ((DO.BadPersonIdException)innerException).ID;
        public override string ToString() => base.ToString() + $", bad student id: {ID}";
    }

    [Serializable]
    public class BadStudentIdCourseIDException : Exception
    {
        public int personID;
        public int courseID;
        public BadStudentIdCourseIDException(string message, Exception innerException) :
            base(message, innerException)
        {
            personID = ((DO.BadPersonIdCourseIDException)innerException).personID;
            courseID = ((DO.BadPersonIdCourseIDException)innerException).courseID;
        }
        public override string ToString() => base.ToString() + $", bad student id: {personID} and course ID: {courseID}";
    }

}

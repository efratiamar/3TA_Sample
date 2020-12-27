using System;
using System.Collections.Generic;

//using DO;

namespace DLAPI
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDL
    {
        #region Person
        IEnumerable<DO.Person> GetAllPersons();
        IEnumerable<DO.Person> GetAllPersonsBy(Predicate<DO.Person> predicate);
        DO.Person GetPerson(int id);
        void AddPerson(DO.Person person);
        void UpdatePerson(DO.Person person);
        void UpdatePerson(int id, Action<DO.Person> update); //method that knows to updt specific fields in Person
        void DeletePerson(int id);
        #endregion

        #region Student
        DO.Student GetStudent(int id);
        IEnumerable<DO.Student> GetAllStudents();
        IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate);
        void AddStudent(DO.Student student);
        void UpdateStudent(DO.Student student);
        void UpdateStudent(int id, Action<DO.Student> update); //method that knows to updt specific fields in Student
        void DeleteStudent(int id); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region StudentInCourse
        IEnumerable<DO.StudentInCourse> GetStudentsInCourseList(Predicate<DO.StudentInCourse> predicate);        
        void AddStudentInCourse(int perID, int courseID, float grade=0);
        void UpdateStudentGradeInCourse(int perID, int courseID, float grade);
        void DeleteStudentInCourse(int perID, int courseID);
        void DeleteStudentFromAllCourses(int perID);

        #endregion

        #region Course
        DO.Course GetCourse(int id);
        IEnumerable<DO.Course> GetAllCourses();

        #endregion

        #region Lecturer
        IEnumerable<DO.LecturerInCourse> GetLecturersInCourseList(Predicate<DO.LecturerInCourse> predicate);
        
        #endregion

    }
}

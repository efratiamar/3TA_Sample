using System;
using System.Collections.Generic;

using DO;

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
        IEnumerable<Person> GetAllPersons();
        IEnumerable<Person> GetAllPersonsBy(Predicate<Person> predicate);
        Person GetPerson(int id);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void UpdatePerson(int id, Action<Person> update); //method that knows to updt specific fields in Person
        void DeletePerson(int id);
        #endregion

        #region Student
        Student GetStudent(int id);
        IEnumerable<object> GetStudentIDs(Func<int, string, object> generate);
        IEnumerable<object> GetStudentIDs(Func<int, object> generate);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void UpdateStudent(int id, Action<Student> update); //method that knows to updt specific fields in Student
        void DeleteStudent(int id); // removes only Student, does not remove the appropriate Person...
        #endregion

        #region StudentInCourse
        IEnumerable<StudentInCourse> GetStudentInCourseList(Predicate<StudentInCourse> predicate);
        #endregion

        #region Course
        Course GetCourse(int id);
        #endregion
    }
}

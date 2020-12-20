using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DLAPI;
//using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal

    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD
        #region Person
        public DO.Person GetPerson(int id)
        {
            DO.Person per = DataSource.ListPersons.Find(p => p.ID == id);

            if (per != null)
                return per.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        public IEnumerable<DO.Person> GetAllPersons()
        {
            return from person in DataSource.ListPersons
                   select person.Clone();
        }
        public IEnumerable<DO.Person> GetAllPersonsBy(Predicate<DO.Person> predicate)
        {
            throw new NotImplementedException();
        }
        public void AddPerson(DO.Person person)
        {
            if (DataSource.ListPersons.FirstOrDefault(p => p.ID == person.ID) != null)
                throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");
            DataSource.ListPersons.Add(person.Clone());
        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(DO.Person p)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(int id, Action<DO.Person> update)
        {
            throw new NotImplementedException();
        }
        #endregion Person

        #region Student
        public DO.Student GetStudent(int id)
        {
            DO.Student stu = DataSource.ListStudents.Find(p => p.ID == id);
            try { Thread.Sleep(2000); } catch (ThreadInterruptedException ex) { }
            if (stu != null)
                return stu.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        }
        public void AddStudent(DO.Student student)
        {
            if (DataSource.ListStudents.FirstOrDefault(s => s.ID == student.ID) != null)
                throw new DO.BadPersonIdException(student.ID, "Duplicate student ID");
            if (DataSource.ListPersons.FirstOrDefault(p => p.ID == student.ID) == null)
                throw new DO.BadPersonIdException(student.ID, "Missing person ID");
            DataSource.ListStudents.Add(student.Clone());
        }
        public IEnumerable<object> GetStudentIDs(Func<int, string, object> generate)
        {
            return from student in DataSource.ListStudents
                   select generate(student.ID, GetPerson(student.ID).Name);
        }

        public IEnumerable<object> GetStudentIDs(Func<int, object> generate)
        {
            return from student in DataSource.ListStudents
                   select generate(student.ID);
        }
        public void UpdateStudent(DO.Student student)
        {
            throw new NotImplementedException();
        }

        public void UpdateStudent(int id, Action<DO.Student> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int id)
        {
            throw new NotImplementedException();
        }
        #endregion Student

        #region StudentInCourse
        public IEnumerable<DO.StudentInCourse> GetStudentInCourseList(Predicate<DO.StudentInCourse> predicate)
        {
            //option A - not good!!! 
            //produces final list instead of deferred query and does not allow proper cloning:
            // return DataSource.listStudInCourses.FindAll(predicate);

            // option B - ok!!
            //Returns deferred query + clone:
            //return DataSource.listStudInCourses.Where(sic => predicate(sic)).Select(sic => sic.Clone());

            // option c - ok!!
            //Returns deferred query + clone:
            return from sic in DataSource.ListStudInCourses
                   where predicate(sic)
                   select sic.Clone();
        }
        #endregion StudentInCourse

        #region Course
        public DO.Course GetCourse(int id)
        {
            return DataSource.ListCourses.Find(c => c.ID == id).Clone();
        }
        #endregion Course
    }
}
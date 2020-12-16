using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DLAPI;
using DO;
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
        public Person GetPerson(int id)
        {
            Person per = DataSource.listPersons.Find(p => p.ID == id); 

            if (per != null)
                return per.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");

            //if not exist,throw exception ...
        }
        public IEnumerable<Person> GetAllPersons()
        {
            return from person in DataSource.listPersons
                   select person.Clone();
        }
        public IEnumerable<Person> GetAllPersonsBy(Predicate<Person> predicate)
        {
            throw new NotImplementedException();
        }
        public void AddPerson(Person p)
        {
            DataSource.listPersons.Add(p);
            //if add failed throw suitible Exception ...
        }

        public void DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(Person p)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(int id, Action<Person> update)
        {
            throw new NotImplementedException();
        }



        public Student GetStudent(int id)
        {
            Student stu = DataSource.listStudents.Find(p => p.ID == id); 

            if (stu != null)
                return stu.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");

            //if not exist,throw exception ...
        }

        public IEnumerable<StudentInCourse> GetStudentInCourseList(Predicate<StudentInCourse> predicate)
        {
            // produces final list instead of deferred query and does not allow proper cloning:
            // return DataSource.listStudInCourses.FindAll(predicate);
            // Returns deferred query:
            //return DataSource.listStudInCourses.Where(sic => predicate(sic)).Select(sic => sic.Clone());
            // or:
            return from sic in DataSource.listStudInCourses
                   where predicate(sic)
                   select sic.Clone();
        }

        public Course GetCourse(int id)
        {
            return DataSource.listCourses.Find(c => c.ID == id).Clone();
        }
    }
}

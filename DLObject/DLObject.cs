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
            Person per = DataSource.ListPersons.Find(p => p.ID == id); 

            if (per != null)
                return per.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }
        public IEnumerable<Person> GetAllPersons()
        {
            return from person in DataSource.ListPersons
                   select person.Clone();
        }
        public IEnumerable<Person> GetAllPersonsBy(Predicate<Person> predicate)
        {
            throw new NotImplementedException();
        }
        public void AddPerson(Person p)
        {
            DataSource.ListPersons.Add(p);
            //test id is unique, otherwise throw suitible Exception ...
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
            Student stu = DataSource.ListStudents.Find(p => p.ID == id); 

            if (stu != null)
                return stu.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        }
        public IEnumerable<object> GetStudentIDs(Func<int, string, object> generate)
        {
            return from student in DataSource.ListStudents
                   select generate(student.ID, GetPerson(student.ID).Name);
        }

        public IEnumerable<StudentInCourse> GetStudentInCourseList(Predicate<StudentInCourse> predicate)
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

        public Course GetCourse(int id)
        {
            return DataSource.ListCourses.Find(c => c.ID == id).Clone();
        }

    }
}

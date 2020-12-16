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
    //will be sealed class ???
    //internal !!!
    class DLObject : IDL
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
            Person per = DataSource.listPersons.Find(p => p.ID == id);  //??? already cloned ???

            if (per != null)
                return per.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");

            //if not exist,throw exception ...
        }
        public IEnumerable<Person> GetAllPersons()
        {
            return DataSource.listPersons;
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
            Student stu = DataSource.listStudents.Find(p => p.ID == id);  //??? already cloned ???

            if (stu != null)
                return stu.Clone();
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");

            //if not exist,throw exception ...
        }

        public IEnumerable<StudentInCourse> GetStudentInCourseList(Predicate<StudentInCourse> predicate)
        {
            return DataSource.listStudInCourses.FindAll(predicate);  //??? already cloned ???
        }

        public Course GetCourse(int id)
        {
            throw new NotImplementedException();
        }
    }
}

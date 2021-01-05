using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DLAPI;
using DO;
//using DO;

namespace DL
{
    sealed class DLXML : IDL    //internal
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

        string personsPath = @"PersonsXml.xml"; //XElement
        
        string studentsPath = @"StudentsXml.xml"; //XMLSerializer
        string coursesPath = @"CoursesXml.xml"; //XMLSerializer
        string lecturersPath = @"LecturersXml.xml"; //XMLSerializer
        string lectInCoursesPath = @"LecturerInCourseXml.xml"; //XMLSerializer
        string studInCoursesPath = @"StudentInCoureseXml.xml"; //XMLSerializer


        #endregion

        #region Person
        public DO.Person GetPerson(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

            Person p =  (from per in personsRootElem.Elements()
                            where int.Parse(per.Element("ID").Value) == id
                            select new Person()
                            {
                                ID = Int32.Parse(per.Element("ID").Value),
                                Name = per.Element("Name").Value,
                                Street = per.Element("Street").Value,
                                HouseNumber = Int32.Parse(per.Element("HouseNumber").Value),
                                City = per.Element("City").Value,
                                BirthDate = DateTime.Parse(per.Element("BirthDate").Value),
                                PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), per.Element("PersonalStatus").Value)
                            }
                        ).FirstOrDefault();

            if (p == null)
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
    
            return p;
        }
        public IEnumerable<DO.Person> GetAllPersons()
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

            return (from p in personsRootElem.Elements()
                    select new Person()
                    {
                        ID = Int32.Parse(p.Element("ID").Value),
                        Name = p.Element("Name").Value,
                        Street = p.Element("Street").Value,
                        HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
                        City = p.Element("City").Value,
                        BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
                        PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value)
                    }
                   );
        }
        public IEnumerable<DO.Person> GetAllPersonsBy(Predicate<DO.Person> predicate)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

            return from p in personsRootElem.Elements()
                   let p1 = new Person()
                   {
                       ID = Int32.Parse(p.Element("ID").Value),
                       Name = p.Element("Name").Value,
                       Street = p.Element("Street").Value,
                       HouseNumber = Int32.Parse(p.Element("HouseNumber").Value),
                       City = p.Element("City").Value,
                       BirthDate = DateTime.Parse(p.Element("BirthDate").Value),
                       PersonalStatus = (PersonalStatus)Enum.Parse(typeof(PersonalStatus), p.Element("PersonalStatus").Value)
                   }
                   where predicate(p1)
                   select p1;
        }
        public void AddPerson(DO.Person person)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

            XElement per1 = (from p in personsRootElem.Elements()
                             where int.Parse(p.Element("ID").Value) == person.ID
                             select p).FirstOrDefault();

            if (per1 != null)
                throw new DO.BadPersonIdException(person.ID, "Duplicate person ID");

            XElement personElem = new XElement("Person",
                                   new XElement("ID", person.ID),
                                   new XElement("Name", person.Name),
                                   new XElement("Street", person.Street),
                                   new XElement("HouseNumber", person.HouseNumber.ToString()),
                                   new XElement("City", person.City),
                                   new XElement("BirthDate", person.BirthDate),
                                   new XElement("PersonalStatus", person.PersonalStatus.ToString()));

            personsRootElem.Add(personElem);
            
            XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        }

        public void DeletePerson(int id)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);

            XElement per = (from p in personsRootElem.Elements()
                                    where int.Parse(p.Element("ID").Value) == id
                                    select p).FirstOrDefault();
            
            if (per != null)
            {
                per.Remove();
                XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad person id: {id}");
        }

        public void UpdatePerson(DO.Person person)
        {
            XElement personsRootElem = XMLTools.LoadListFromXMLElement(personsPath);
            
            XElement per = (from p in personsRootElem.Elements()
                                    where int.Parse(p.Element("ID").Value) == person.ID
                                    select p).FirstOrDefault();

            if (per != null)
            {
                per.Element("ID").Value = person.ID.ToString();
                per.Element("Name").Value = person.Name;
                per.Element("Street").Value = person.Street;
                per.Element("HouseNumber").Value = person.HouseNumber.ToString();
                per.Element("City").Value = person.City;
                per.Element("BirthDate").Value = person.BirthDate.ToString();
                per.Element("PersonalStatus").Value = person.PersonalStatus.ToString();

                XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
            }
            else
                throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");
        }

        public void UpdatePerson(int id, Action<DO.Person> update)
        {
            throw new NotImplementedException();
        }

        #endregion Person

        #region Student
        public DO.Student GetStudent(int id)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            DO.Student stu = ListStudents.Find(p => p.ID == id);
            if (stu != null)
                return stu; //no need to Clone()
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");
        }
        public void AddStudent(DO.Student student)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            if (ListStudents.FirstOrDefault(s => s.ID == student.ID) != null)
                throw new DO.BadPersonIdException(student.ID, "Duplicate student ID");
                        
            if (GetPerson(student.ID) == null)
                throw new DO.BadPersonIdException(student.ID, "Missing person ID");
            
            ListStudents.Add(student); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);

        }
        public IEnumerable<DO.Student> GetAllStudents()
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            return from student in ListStudents
                   select student; //no need to Clone()
        }
        public IEnumerable<object> GetStudentFields(Func<int, string, object> generate)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            return from student in ListStudents
                   select generate(student.ID, GetPerson(student.ID).Name);
        }

        public IEnumerable<object> GetStudentListWithSelectedFields(Func<DO.Student, object> generate)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            return from student in ListStudents
                   select generate(student);
        }
        public void UpdateStudent(DO.Student student)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            DO.Student stu = ListStudents.Find(p => p.ID == student.ID);
            if (stu != null)
            {
                ListStudents.Remove(stu);
                ListStudents.Add(student); //no nee to Clone()
            }
            else
                throw new DO.BadPersonIdException(student.ID, $"bad student id: {student.ID}");

            XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);            
        }

        public void UpdateStudent(int id, Action<DO.Student> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(int id)
        {
            List<Student> ListStudents = XMLTools.LoadListFromXMLSerializer<Student>(studentsPath);

            DO.Student stu = ListStudents.Find(p => p.ID == id);

            if (stu != null)
            {
                ListStudents.Remove(stu);
            }
            else
                throw new DO.BadPersonIdException(id, $"bad student id: {id}");

            XMLTools.SaveListToXMLSerializer(ListStudents, studentsPath);
        }
        #endregion Student

        #region StudentInCourse
        public IEnumerable<DO.StudentInCourse> GetStudentsInCourseList(Predicate<DO.StudentInCourse> predicate)
        {
            List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

            return from sic in ListStudInCourses
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        {
            List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

            if (ListStudInCourses.FirstOrDefault(cis => (cis.PersonId == perID && cis.CourseId == courseID)) != null)
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is already registered to course ID");
            
            DO.StudentInCourse sic = new DO.StudentInCourse() { PersonId = perID, CourseId = courseID, Grade = grade };
            
            ListStudInCourses.Add(sic);

            XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);
        }

        public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        {
            List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

            DO.StudentInCourse sic = ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

            if (sic != null)
            {
                sic.Grade = grade;
            }
            else
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");

            XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);
        }

        public void DeleteStudentInCourse(int perID, int courseID)
        {
            List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);

            DO.StudentInCourse sic = ListStudInCourses.Find(cis => (cis.PersonId == perID && cis.CourseId == courseID));

            if (sic != null)
            {
                ListStudInCourses.Remove(sic);
            }
            else
                throw new DO.BadPersonIdCourseIDException(perID, courseID, "person ID is NOT registered to course ID");

            XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);

        }
        public void DeleteStudentFromAllCourses(int perID)
        {
            List<StudentInCourse> ListStudInCourses = XMLTools.LoadListFromXMLSerializer<StudentInCourse>(studInCoursesPath);
            
            ListStudInCourses.RemoveAll(p => p.PersonId == perID);

            XMLTools.SaveListToXMLSerializer(ListStudInCourses, studInCoursesPath);

        }

        #endregion StudentInCourse

        #region Course
        public DO.Course GetCourse(int id)
        {            
            List<Course> ListCourses = XMLTools.LoadListFromXMLSerializer<Course>(coursesPath);

            return ListCourses.Find(c => c.ID == id); //no need to Clone()

            //if not exist throw exception etc.
        }

        public IEnumerable<DO.Course> GetAllCourses()
        {
            List<Course> ListCourses = XMLTools.LoadListFromXMLSerializer<Course>(coursesPath);

            return from course in ListCourses
                   select course; //no need to Clone()
        }

        #endregion Course

        #region Lecturer
        public IEnumerable<DO.LecturerInCourse> GetLecturersInCourseList(Predicate<DO.LecturerInCourse> predicate)
        {
            List<LecturerInCourse> ListLectInCourses = XMLTools.LoadListFromXMLSerializer<LecturerInCourse>(lectInCoursesPath);

            return from sic in ListLectInCourses
                   where predicate(sic)
                   select sic; //no need to Clone()
        }
        #endregion


    }
}
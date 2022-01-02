using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;
//using BO;
//using BO;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl =   DLFactory.GetDL();

        #region Student
        BO.Student studentDoBoAdapter(DO.Student studentDO)
        {
            BO.Student studentBO = new BO.Student();
            DO.Person personDO;
            int id = studentDO.ID;
            try
            {
                personDO = dl.GetPerson(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }
            personDO.CopyPropertiesTo(studentBO);
            //studentBO.ID = personDO.ID;
            //studentBO.BirthDate = personDO.BirthDate;
            //studentBO.City = personDO.City;
            //studentBO.Name = personDO.Name;
            //studentBO.HouseNumber = personDO.HouseNumber;
            //studentBO.Street = personDO.Street;
            //studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            studentDO.CopyPropertiesTo(studentBO);
            //studentBO.StartYear = studentDO.StartYear;
            //studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
            //studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

            studentBO.ListOfCourses = from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
                                      let course = dl.GetCourse(sic.CourseId)
                                      select course.CopyToStudentCourse(sic);
            //new BO.StudentCourse()
            //{
            //    ID = course.ID,
            //    Number = course.Number,
            //    Name = course.Name,
            //    Year = course.Year,
            //    Semester = (BO.Semester)(int)course.Semester,
            //    Grade = sic.Grade
            //};

            return studentBO;
        }

        public BO.Student GetStudent(int id)
        {
            DO.Student studentDO;
            try
            {
                studentDO = dl.GetStudent(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
            }
            return studentDoBoAdapter(studentDO);
        }

        public IEnumerable<BO.Student> GetAllStudents()
        {
            //return from item in dl.GetStudentListWithSelectedFields( (stud) => { return GetStudent(stud.ID); } )
            //       let student = item as BO.Student
            //       orderby student.ID
            //       select student;
            return from studentDO in dl.GetAllStudents()
                   orderby studentDO.ID 
                   select studentDoBoAdapter(studentDO);

        }
        public IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.ListedPerson> GetStudentIDNameList()
        {
            return from item in dl.GetStudentListWithSelectedFields((studentDO) =>
                    {
                        try { Thread.Sleep(1500); } catch (ThreadInterruptedException e) { }
                        return new BO.ListedPerson() { ID = studentDO.ID, Name = dl.GetPerson(studentDO.ID).Name };
                    })
                   let studentBO = item as BO.ListedPerson
                   //orderby student.ID
                   select studentBO;
        }

        public void UpdateStudentPersonalDetails(BO.Student student)
        {
            //Update DO.Person            
            DO.Person personDO = new DO.Person();            
            student.CopyPropertiesTo(personDO);
            try
            {
                dl.UpdatePerson(personDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }

            //Update DO.Student            
            DO.Student studentDO = new DO.Student();
            student.CopyPropertiesTo(studentDO);
            try
            {
                dl.UpdateStudent(studentDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }

        }

        public void DeleteStudent(int id)
        {
            try
            {
                dl.DeletePerson(id);
                dl.DeleteStudent(id);
                dl.DeleteStudentFromAllCourses(id);                
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }         
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Person id does not exist or he is not a student", ex);
            }
        }

        public void AddStudent(BO.Student student)
        {
            //Add new student  (DO.Person + DO.Student)
            //with no courses (courses will be added one by one through AddStudentInCourse()
            
            //Add DO.Person            
            DO.Person personDO = new DO.Person();
            student.CopyPropertiesTo(personDO);
            try
            {
                dl.AddPerson(personDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }

            //Add DO.Student            
            DO.Student studentDO = new DO.Student();
            student.CopyPropertiesTo(studentDO);
            try
            {
                dl.AddStudent(studentDO);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }
        }

        #endregion

        #region StudentIn Course
        public void AddStudentInCourse(int perID, int courseID, float grade = 0)
        {
            try
            {
                dl.AddStudentInCourse(perID, courseID, grade);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }
        }

        public void UpdateStudentGradeInCourse(int perID, int courseID, float grade)
        {
            try
            {
                dl.UpdateStudentGradeInCourse(perID, courseID, grade);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }
        }

        public void DeleteStudentInCourse(int perID, int courseID)
        {
            try
            {
                dl.DeleteStudentInCourse(perID, courseID);
            }
            catch (DO.BadPersonIdCourseIDException ex)
            {
                throw new BO.BadStudentIdCourseIDException("Student ID and Course ID is Not exist", ex);
            }
        }
        #endregion

        #region Course

        BO.Course courseDoBoAdapter(DO.Course courseDO)
        {
            BO.Course courseBO = new BO.Course();            
            int id = courseDO.ID;
            courseDO.CopyPropertiesTo(courseBO);

            courseBO.Lecturers = from lic in dl.GetLecturersInCourseList(lic => lic.CourseId == id)
                                 let course = dl.GetCourse(lic.CourseId)
                                 select (BO.CourseLecturer) course.CopyPropertiesToNew(typeof(BO.CourseLecturer));
            return courseBO;
        }
        public IEnumerable<BO.Course> GetAllCourses()
        {
            return from crsDO in dl.GetAllCourses()
                   select courseDoBoAdapter(crsDO);
        }

        public IEnumerable<BO.StudentCourse> GetAllCoursesPerStudent(int id)
        {
            return from sic in dl.GetStudentsInCourseList(sic => sic.PersonId == id)
                                      let course = dl.GetCourse(sic.CourseId)
                                      select course.CopyToStudentCourse(sic);
        }

        #endregion


    }
}

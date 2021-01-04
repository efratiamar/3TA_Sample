using System;
using System.Collections.Generic;
using System.Linq;
using DLAPI;
using BLAPI;
using System.Threading;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();

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

            studentBO.ListOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == id)
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
    }
}

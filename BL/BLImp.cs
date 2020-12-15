using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using BO;

namespace BL
{
    class BLImp : IBL //internal
    {
        IDL dl = DLFactory.GetDL();



        public BO.Student GetStudent(int id)
        {
            BO.Student studentBO = new BO.Student();

            DO.Person personDO;
            try
            {
                personDO = dl.GetPerson(id);
            }
            catch (DO.BadPersonIdException ex)
            {
                throw new BO.BadStudentIdException("Student ID is illegal", ex);
            }
            studentBO.BirthDate = personDO.BirthDate;
            studentBO.City = personDO.City;
            studentBO.Name = personDO.Name;
            studentBO.HouseNumber = personDO.HouseNumber;
            studentBO.Street = personDO.Street;
            studentBO.PersonalStatus = (BO.PersonalStatus)(int)personDO.PersonalStatus;

            DO.Student studentDO = dl.GetStudent(id);
            studentBO.StartYear = studentDO.StartYear;
            studentBO.Status = (BO.StudentStatus)(int)studentDO.Status;
            studentBO.Graduation = (BO.StudentGraduate)(int)studentDO.Graduation;

            studentBO.listOfCourses = from sic in dl.GetStudentInCourseList(sic => sic.PersonId == id)
                                    let course = dl.GetCourse(sic.CourseId)
                                    select new BO.StudentCourse()
                                    {
                                        CourseId = course.ID,
                                        CourseNumber = course.Number,
                                        CourseName = course.Name,
                                        Year =  course.Year,
                                        Semester = (BO.Semester)(int)course.Semester,
                                        Grade = sic.Grade
                                    };
            return studentBO;
        }


        public IEnumerable<Student> GetAllStudents()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Student> GetStudentsBy(Predicate<Student> predicate)
        {
            throw new NotImplementedException();
        }
    }
}

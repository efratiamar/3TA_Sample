using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using BO;


namespace BLAPI
{
    public interface IBL
    {
        //Add Person to Course
        //get all courses for student
        //etc...
        BO.Student GetStudent(int id);
        IEnumerable<BO.Student> GetAllStudents();
        IEnumerable<BO.ListedPerson> GetStudentIDNameList();

        IEnumerable<BO.Student> GetStudentsBy(Predicate<BO.Student> predicate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DO;
namespace DS
{
    public static class DataSource
    {
        public static List<Person> listPersons;
        public static List<Course> listCourses;
        public static List<Student> listStudents;
        public static List<Lecturer> listLecturers;
        public static List<LecturerInCourse> listLectInCourses;
        public static List<StudentInCourse> listStudInCourses;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            listPersons = new List<Person>
            {
                new Person
                {
                    Name = "David",
                    ID = 36,
                    Street = "Harekefet",
                    HouseNumber = 44,
                    City = "Tel-Aviv",
                    PersonalStatus = PersonalStatus.MARRIED,
                    BirthDate = DateTime.Parse("24.03.85")
                },

                new Person
                {
                    Name = "Yossi",
                    ID = 23,
                    Street = "Moshe Dayan",
                    HouseNumber = 145,
                    City = "Jerusalem",
                    PersonalStatus = PersonalStatus.SINGLE,
                    BirthDate = DateTime.Parse("13.10.95")
                },

                new Person
                {
                    Name = "Roni",
                    ID = 15,
                    Street = " Dayan",
                    HouseNumber = 33,
                    City = "Eilat",
                    PersonalStatus = PersonalStatus.MARRIED,
                    BirthDate = DateTime.Parse("13.10.95")
                }
            };


            listStudents = new List<Student>
            {
                new Student
                {
                    ID = 36,
                    StartYear = 2018,
                    Status = StudentStatus.ACTIVE,
                    Graduation = StudentGraduate.BSC
                },
                new Student
                {
                    ID = 23,
                    StartYear = 2017,
                    Status = StudentStatus.FINISHED,
                    Graduation = StudentGraduate.PHD
                }
            };

            listCourses = new List<Course>
            {
                new Course
                {
                    ID = 1,
                    Number = 15333,
                    Name = "Mini",
                    LectureHours = 3,
                    PracticeHours = 1,
                    CreditPoint = 3,
                    Year = 2010,
                    Semester = Semester.A
                }
            };

            listStudInCourses = new List<StudentInCourse>
            {
                new StudentInCourse
                {
                    CourseId = 1,
                    Grade = 100,
                    PersonId = 36
                }
            };


        }
    }
}


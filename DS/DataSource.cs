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
        public static List<Person> ListPersons;
        public static List<Course> ListCourses;
        public static List<Student> ListStudents;
        public static List<Lecturer> ListLecturers;
        public static List<LecturerInCourse> ListLectInCourses;
        public static List<StudentInCourse> ListStudInCourses;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            ListPersons = new List<Person>
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


            ListStudents = new List<Student>
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

            ListCourses = new List<Course>
            {
                new Course
                {
                    ID = 1,
                    Number = 153007,
                    Name = "MiniProject with Windows Systems",
                    LectureHours = 3,
                    PracticeHours = 1,
                    CreditPoint = 3,
                    Year = 2010,
                    Semester = Semester.A
                }
            };

            ListStudInCourses = new List<StudentInCourse>
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


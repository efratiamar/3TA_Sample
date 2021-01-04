using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using DO;

namespace DS
{
    public static class DataSource
    {
        public static List<Person> Persons;
        public static List<Course> Courses;
        public static List<Student> Students;
        public static List<Lecturer> Lecturers;
        public static List<LecturerInCourse> ListLectInCourses;
        public static List<StudentInCourse> ListStudInCourses;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            Persons = new List<Person>
            {
                new Person
                {
                    Name = "David",
                    ID = 36,
                    Street = "Harekefet",
                    HouseNumber = 44,
                    City = "Tel-Aviv",
                    PersonalStatus = PersonalStatus.MARRIED,
                    BirthDate =  DateTime.ParseExact("24.03.85", "dd.MM.yy", null)
                },

                new Person
                {
                    Name = "Yossi",
                    ID = 23,
                    Street = "Moshe Dayan",
                    HouseNumber = 145,
                    City = "Jerusalem",
                    PersonalStatus = PersonalStatus.SINGLE,
                    BirthDate =  DateTime.ParseExact("27.12.95", "dd.MM.yy", null)
                },

                new Person
                {
                    Name = "Roni",
                    ID = 15,
                    Street = "Dayan",
                    HouseNumber = 33,
                    City = "Petach Tikva",
                    PersonalStatus = PersonalStatus.MARRIED,
                    BirthDate =  DateTime.ParseExact("14.11.97", "dd.MM.yy", null)
                },

                new Person
                {
                    Name = "Shira",
                    ID = 3,
                    Street = "Moshe",
                    HouseNumber = 33,
                    City = "Eilat",
                    PersonalStatus = PersonalStatus.SINGLE,
                    BirthDate =  DateTime.ParseExact("24.08.99", "dd.MM.yy", null)
                },

                new Person
                {
                    Name = "Gila",
                    ID = 67,
                    Street = "Marom",
                    HouseNumber = 56,
                    City = "Givataiim",
                    PersonalStatus = PersonalStatus.MARRIED,
                    BirthDate =  DateTime.ParseExact("23.12.77", "dd.MM.yy", null)
                }


            };


            Students = new List<Student>
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
                },
                new Student
                {
                    ID = 15,
                    StartYear = 2013,
                    Status = StudentStatus.FINISHED,
                    Graduation = StudentGraduate.BA
                }

            };

            Courses = new List<Course>
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
                },

                new Course
                {
                    ID = 2,
                    Number = 15005,
                    Name = "Intro to CS",
                    LectureHours = 3,
                    PracticeHours = 2,
                    CreditPoint = 4,
                    Year = 2011,
                    Semester = Semester.B
                },

                new Course
                {
                    ID = 3,
                    Number = 15004,
                    Name = "Data Structure 1",
                    LectureHours = 3,
                    PracticeHours = 1,
                    CreditPoint = 4,
                    Year = 2014,
                    Semester = Semester.A
                },

                new Course
                {
                    ID = 4,
                    Number = 15006,
                    Name = "Data Structure 2",
                    LectureHours = 3,
                    PracticeHours = 1,
                    CreditPoint = 4,
                    Year = 2014,
                    Semester = Semester.B
                }


            };

            ListStudInCourses = new List<StudentInCourse>
            {
                new StudentInCourse
                {
                    CourseId = 1,
                    Grade = 100,
                    PersonId = 36
                },
                new StudentInCourse
                {
                    CourseId = 2,
                    Grade = 100,
                    PersonId = 36
                },
                new StudentInCourse
                {
                    CourseId = 3,
                    Grade = 100,
                    PersonId = 23
                },
                new StudentInCourse
                {
                    CourseId = 3,
                    Grade = 100,
                    PersonId = 15
                }
            };

            ListLecturers = new List<Lecturer>
            {
                new Lecturer
                {
                    ID = 3,
                    Status = LecturerStatus.STUFF,
                    Position = LecturerPosition.PROFESSOR,
                    SeniorStuff  = true,
                    JuniorStuff = false,
                    AdjunctStuff = false
                },


                new Lecturer
                {
                    ID = 67,
                    Status = LecturerStatus.SABBATICAL,
                    Position = LecturerPosition.SENIOR_LECTURER,
                    SeniorStuff  = false,
                    JuniorStuff = true,
                    AdjunctStuff = false
                }
            };

            ListLectInCourses = new List<LecturerInCourse>
            {
                new LecturerInCourse
                {
                    CourseId = 1,
                    PersonId = 3,
                    Status = CourseLectureStatus.LECTURER,
                    GroupsAmount = 2
                },
                new LecturerInCourse
                {
                    CourseId = 3,
                    PersonId = 67,
                    Status = CourseLectureStatus.PRACTITIONER,
                    GroupsAmount = 1
                },
                new LecturerInCourse
                {
                    CourseId = 3,
                    PersonId = 67,
                    Status = CourseLectureStatus.LECTURER,
                    GroupsAmount =3
                }
            };

        }
    }
}


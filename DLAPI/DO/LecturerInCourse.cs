using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LecturerInCourse
    {
        public int PersonId { get; set; }
        public int CourseId { get; set; }
        public CourseLectureStatus Status { get; set; }
        public int GroupsAmount { get; set; }
    }
}

namespace BO
{
    /// <summary>
    ///  This BO class is based on DO.Course + DO.LecturerInCourse
    ///  It is to be used inside Course entity only - therefore it does not contain any Course data
    ///  Contains only course related lecturer data to be presented in the lst of lecturers of a course
    /// </summary>
    public class CourseLecturer
    {
        public int ID { get; set; } // person ID
        public string Name { get; set; }
        public bool IsPractitioner { get; set; }
        public bool IsLecturer { get; set; }
        public int GroupsAmount { get; set; }
    }
}
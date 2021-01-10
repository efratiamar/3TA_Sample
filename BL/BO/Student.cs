using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Student : Person
    {
        [Range(1800, 2021,
               ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int StartYear { get; set; }
        public StudentStatus Status { get; set; }
        public StudentGraduate Graduation { get; set; }
        public IEnumerable<StudentCourse> ListOfCourses {get;set;}
        public override string ToString() => this.ToStringProperty();
        //[StringLength(6)] 
        //[Required()]
    }
}

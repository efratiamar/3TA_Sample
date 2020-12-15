using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Lecturer
    {
        public int ID { get; set; } // person ID
        public LecturerStatus Status { get; set; }
        public LecturerPosition Position { get; set; }
        public bool SeniorStuff { get; set; }
        public bool JuniorStuff { get; set; }
        public bool AdjunctStuff { get; set; }
    }
}

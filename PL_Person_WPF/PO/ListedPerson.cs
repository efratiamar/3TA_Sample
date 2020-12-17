using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    public class ListedPerson
    {
        BO.ListedPerson person;
        public BO.ListedPerson Person { set { person = value; Show = string.Format("{0,-9} {1}", person.ID, person.Name); } get => person; }
        public string Show { get; private set; }
    }
}

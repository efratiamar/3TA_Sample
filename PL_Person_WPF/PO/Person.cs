using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    abstract class Person : DependencyObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BO.PersonalStatus PersonalStatus { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string City { get; set; }
        public DateTime BirthDate { get; set; }
    }
}

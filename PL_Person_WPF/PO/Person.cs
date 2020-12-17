using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public abstract class Person : DependencyObject
    {
        static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(int), typeof(Person));
        static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(Person));
        static readonly DependencyProperty PersonalStatusProperty = DependencyProperty.Register("PersonalStatus", typeof(BO.PersonalStatus), typeof(Person));
        static readonly DependencyProperty StreetProperty = DependencyProperty.Register("Street", typeof(string), typeof(Person));
        static readonly DependencyProperty HouseNumberProperty = DependencyProperty.Register("HouseNumber", typeof(int), typeof(Person));
        static readonly DependencyProperty CityProperty = DependencyProperty.Register("City", typeof(string), typeof(Person));
        static readonly DependencyProperty BirthDateProperty = DependencyProperty.Register("BirthDate", typeof(DateTime), typeof(Person));
        public int ID { get => (int)GetValue(IDProperty); set => SetValue(IDProperty, value); }
        public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
        public BO.PersonalStatus PersonalStatus { get => (BO.PersonalStatus)GetValue(PersonalStatusProperty); set => SetValue(PersonalStatusProperty, value); }
        public string Street { get => (string)GetValue(StreetProperty); set => SetValue(StreetProperty, value); }
        public int HouseNumber { get => (int)GetValue(HouseNumberProperty); set => SetValue(HouseNumberProperty, value); }
        public string City { get => (string)GetValue(CityProperty); set => SetValue(CityProperty, value); }
        public DateTime BirthDate { get => (DateTime)GetValue(BirthDateProperty); set => SetValue(BirthDateProperty, value); }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;
using System.Xml.Linq;

namespace OOP_Lab4_Masiuk.Classes
{
    internal class Faculty
    {
        public Faculty(string title, string department, string branch)
        {
            Title = title;
            Department = department;
            Branch = branch;
        }

        public string Title { get; set; }
        public string Department { get; set; }
        public string Branch { get; set; }
    }
}

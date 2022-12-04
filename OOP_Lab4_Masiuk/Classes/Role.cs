using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4_Masiuk.Classes
{
    internal class Role
    {
        public Role(string title, string startDate, string endDate)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}

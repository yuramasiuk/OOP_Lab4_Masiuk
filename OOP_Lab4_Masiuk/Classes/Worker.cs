using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Data;

namespace OOP_Lab4_Masiuk.Classes
{
    internal class Worker
    {
        //[JsonIgnore]
        //[JsonPropertyName("Name")]

        public Worker(string pIP, Faculty faculty, string cathedra, string laboratory, Role role)
        {
            PIP = pIP;
            Faculty = faculty;
            Cathedra = cathedra;
            Laboratory = laboratory;
            Role = role;
        }

        public Worker(List<string> workerData)
        {
            PIP = workerData[0];
            Faculty = new Faculty(workerData[1], workerData[2], workerData[3]);
            Cathedra = workerData[4];
            Laboratory = workerData[5];
            Role = new Role(workerData[6], workerData[7], workerData[8]);
        }

        public Worker(DataGridViewRow row)
        {
            PIP = row.Cells[0].Value + "";

            Faculty = new Faculty(
                row.Cells[1].Value + "", 
                row.Cells[2].Value + "", 
                row.Cells[3].Value + ""
                );

            Cathedra = row.Cells[4].Value + "";
            Laboratory = row.Cells[5].Value + "";

            Role = new Role(
                row.Cells[6].Value + "",
                row.Cells[7].Value + "",
                row.Cells[8].Value + ""
                );
        }

        public Worker()
        {
            PIP = "";
            Faculty = new Faculty("", "", "");
            Cathedra = "";
            Laboratory = "";
            Role = new Role("", "", "");
        }

        public string PIP { get; set; }
        public Faculty Faculty{ get; set; }
        public string Cathedra { get; set; }
        public string Laboratory { get; set; }
        public Role Role { get; set; }
        public string[] GetData()
        {
            string[] row = { 
                PIP, 
                Faculty.Title, 
                Faculty.Department, 
                Faculty.Branch,
                Cathedra,
                Laboratory,
                Role.Title, 
                Role.StartDate, 
                Role.EndDate
            };

            return row;
        }
    }
}

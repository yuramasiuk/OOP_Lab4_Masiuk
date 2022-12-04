using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOP_Lab4_Masiuk.Classes
{
    internal class Helper
    {
        public static string Serialize(Worker worker)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonWorker = JsonSerializer.Serialize(worker, options);
            return jsonWorker;
        }
        public static async Task<List<Worker>?> Deserialize(string path)
        {
            using FileStream fs = new FileStream(path, FileMode.Open);
            var workers = await JsonSerializer.DeserializeAsync<List<Worker>>(fs);
            return workers;
        }
        public static void SetupDataGridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.ColumnCount = 9;
            dataGridView.Columns[0].Name = "PIP";
            dataGridView.Columns[1].Name = "Faculty title";
            dataGridView.Columns[2].Name = "Faculty department";
            dataGridView.Columns[3].Name = "Faculty branch";
            dataGridView.Columns[4].Name = "Cathedra";
            dataGridView.Columns[5].Name = "Laboratory";
            dataGridView.Columns[6].Name = "Role title";
            dataGridView.Columns[7].Name = "Role start date";
            dataGridView.Columns[8].Name = "Role end date";
        }
    }
}

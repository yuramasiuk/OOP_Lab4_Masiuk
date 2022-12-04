using System.Text.Json;
using System.Text;
using System.IO;
using System.Diagnostics.Metrics;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using OOP_Lab4_Masiuk.Classes;

namespace OOP_Lab4_Masiuk
{
    public partial class MainForm : Form
    {
        const string DESERIALIZE_PATH = @".\data.json";
        const string SERIALIZE_PATH = @".\result.json";
        const string EXPORT_PATH = @".\output.txt";
        const string IMPORT_PATH = @".\input.txt";

        public List <List<string>> tableData = new List<List<string>>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Helper.SetupDataGridView(mainDataGridView);
        }

        public void AddRow(string[] row)
        {
            mainDataGridView.Rows.Add(row);
            tableData.Add(row.ToList());
        }

        private async void toolStripButton1_Click(object sender, EventArgs e) // Serialize
        {
            List<string> jsonWorkers = new List<string>();
            foreach (DataGridViewRow row in mainDataGridView.Rows)
            {
                bool rowIsEmpty = true;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        rowIsEmpty = false;
                        break;
                    }
                }

                if(!rowIsEmpty)
                {
                    Worker worker = new Worker(row);

                    string jsonWorker = Helper.Serialize(worker);
                    jsonWorkers.Add(jsonWorker);
                }
            }

            await File.WriteAllLinesAsync(SERIALIZE_PATH, jsonWorkers);
        }
        private async void toolStripButton2_Click(object sender, EventArgs e) // Deserialize
        {
            var workers = await Helper.Deserialize(DESERIALIZE_PATH);
            if(workers != null)
            {
                foreach (var worker in workers)
                {
                    mainDataGridView.Rows.Add(worker.GetData());
                    tableData.Add(worker.GetData().ToList());
                }
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e) // Add one with InputForm
        {
            InputForm inputForm = new InputForm(this);
            inputForm.ShowDialog();
        }

        private void toolStripButton5_Click(object sender, EventArgs e) // Import
        {
            try
            {
                foreach (string line in File.ReadLines(IMPORT_PATH))
                {
                    string[] workerData = line.Split(" ");
                    if(workerData.Length != 9)
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    tableData.Add(workerData.ToList());
                    Worker worker = new Worker(workerData.ToList());
                    mainDataGridView.Rows.Add(worker.GetData());

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Text file must contain all data in format of 9 strings in each row", "Input error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }

        }

        private async void toolStripButton6_Click(object sender, EventArgs e) // Export
        {
            List<string> strings = new List<string>();
            foreach (DataGridViewRow row in mainDataGridView.Rows)
            {
                bool rowIsEmpty = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value == null)
                    {
                        rowIsEmpty = true;
                        break;
                    }
                }
                if (!rowIsEmpty)
                {
                    var workerData =
                        row.Cells[0].Value + " " +
                        row.Cells[1].Value + " " +
                        row.Cells[2].Value + " " +
                        row.Cells[3].Value + " " +
                        row.Cells[4].Value + " " +
                        row.Cells[5].Value + " " +
                        row.Cells[6].Value + " " +
                        row.Cells[7].Value + " " +
                        row.Cells[8].Value + " ";
                    strings.Add(workerData);
                }
            }
            await File.WriteAllLinesAsync(EXPORT_PATH, strings);
        }

        private void cancelSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mainDataGridView.Rows.Clear();
            foreach (List<string> workerData in tableData)
            {
                Worker worker = new Worker(workerData.ToList());
                mainDataGridView.Rows.Add(worker.GetData());
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e) // Search by PIP
        {
            if (string.IsNullOrWhiteSpace(toolStripTextBox1.Text))
            {
                MessageBox.Show("Input PIP search criteria");
            }
            else
            {
                string pipCriteria = toolStripTextBox1.Text;
                var sortedByPIP = from workerData in tableData
                                  where workerData[0] == pipCriteria
                                  select workerData;
                mainDataGridView.Rows.Clear();
                foreach (List<string> workerData in sortedByPIP)
                {
                    Worker worker = new Worker(workerData.ToList());
                    mainDataGridView.Rows.Add(worker.GetData());
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e) // Search by faculty title
        {
            if (string.IsNullOrWhiteSpace(toolStripTextBox2.Text))
            {
                MessageBox.Show("Input faculty title search criteria");
            }
            else
            {
                string facultyTitleCriteria = toolStripTextBox2.Text;
                var sortedByFacultyTitle = from workerData in tableData
                                  where workerData[1] == facultyTitleCriteria
                                  select workerData;
                mainDataGridView.Rows.Clear();
                foreach (List<string> workerData in sortedByFacultyTitle)
                {
                    Worker worker = new Worker(workerData.ToList());
                    mainDataGridView.Rows.Add(worker.GetData());
                }
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e) // Search by cathedra
        {
            try
            {
                if (string.IsNullOrWhiteSpace(toolStripTextBox3.Text))
                {
                    MessageBox.Show("Input cathedra search criteria", "Input error");
                }
                else
                {
                    string cathedraCriteria = toolStripTextBox3.Text;
                    var sortedByCathedra = from workerData in tableData
                                           where workerData[4] == cathedraCriteria
                                           select workerData;
                    mainDataGridView.Rows.Clear();
                    foreach (List<string> workerData in sortedByCathedra)
                    {
                        Worker worker = new Worker(workerData.ToList());
                        mainDataGridView.Rows.Add(worker.GetData());
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e) // Delete by index
        {
            try
            {
                if (!string.IsNullOrEmpty(toolStripTextBox4.Text))
                {
                    if (int.TryParse(toolStripTextBox4.Text, out int index))
                    {
                        mainDataGridView.Rows.RemoveAt(index - 1);
                        tableData.RemoveAt(index - 1);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Number of the row must be in range from 1 to last row number", "Input error");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }

        private void mainDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
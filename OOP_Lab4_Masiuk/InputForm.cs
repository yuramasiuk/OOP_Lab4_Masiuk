using OOP_Lab4_Masiuk.Classes;
using System.Windows.Forms;

namespace OOP_Lab4_Masiuk
{
    public partial class InputForm : Form
    {
        public InputForm(MainForm parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        MainForm parentForm;

        private void button1_Click(object sender, System.EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    throw new Exception("All fields must be filled");
                }

                Worker worker = new Worker(
                    textBox1.Text,
                    new Faculty(textBox2.Text, textBox3.Text, textBox4.Text),
                    textBox5.Text,
                    textBox6.Text,
                    new Role(textBox7.Text, textBox8.Text, textBox9.Text)
                );

                parentForm.AddRow(worker.GetData());
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Input error");
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}

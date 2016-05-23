using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
        SqlConnection connection;
        private void Select(SqlConnection connection, string s, string s1)
        {
            SqlDataAdapter da = new SqlDataAdapter(s, connection);

            SqlCommandBuilder cb = new SqlCommandBuilder(da);

            DataSet ds = new DataSet();

            da.Fill(ds, s1);
            dataGridView1.DataSource = ds.Tables[0];
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            connectionStringBuilder.DataSource = @".\SQLEXPRESS";       
            connectionStringBuilder.InitialCatalog = "SEAPORT";          
            connectionStringBuilder.UserID = userNameTextBox.Text;      
            connectionStringBuilder.Password = passwordTextBox.Text;

            connection = new SqlConnection(connectionStringBuilder.ConnectionString);

            try
            {
                    connection.Open();
                    MessageBox.Show("Подключение выполено");
                    tabControl1.SelectedIndex = 1;
                    Select(connection, "SELECT * FROM Ship", "Ship");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                finally
                {
                    //connection.Close();
                }
                
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Судна": Select(connection, "SELECT * FROM Ship", "Ship"); break;
                case "Рейсы": Select(connection, "SELECT * FROM Voyage", "Ship"); break;
                case "Накладные": Select(connection, "SELECT * FROM Invoice", "Ship"); break;
                case "Контейнеры": Select(connection, "SELECT * FROM Container", "Ship"); break;
                case "Накладные-Контейнеры": Select(connection, "SELECT * FROM InvoiceContainer", "Ship"); break;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string temp="";
            if (radioButton1.Checked) temp = "Ship";
            if (radioButton2.Checked) temp = "Voyage";
            if (radioButton3.Checked) temp = "Invoice";
            if (radioButton4.Checked) temp = "Container";

            string s = "INSERT INTO " + temp + " VALUES(" + AddTextBox.Text + ")";
            SqlCommand insertCommand = connection.CreateCommand(); 
            insertCommand.CommandText = s;

            int rowAffected = insertCommand.ExecuteNonQuery();
            MessageBox.Show("Строка добавлена");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (connection != null)
           // {
                connection.Close();
                MessageBox.Show("Подключение закрыто");
           // }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string s = "";
            if (radioButton1.Checked)
                s = "DELETE Ship WHERE sname = " + DeleteTextBox.Text;

            if (radioButton2.Checked)
                s = "DELETE Voyage WHERE vnumber = " + DeleteTextBox.Text;

            if (radioButton3.Checked)
                s = "DELETE Invoice WHERE invnumber = " + DeleteTextBox.Text;

            if (radioButton4.Checked)     
                s = "DELETE Container WHERE cnumber = " + DeleteTextBox.Text;
            

            SqlCommand insertCommand = connection.CreateCommand();
            insertCommand.CommandText = s;

            int rowAffected = insertCommand.ExecuteNonQuery();
            MessageBox.Show("Строка удалена");
        }

        private void CountButton_Click(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Судна":
                    {
                        SqlCommand countCommand = connection.CreateCommand();
                        countCommand.CommandText = "SELECT COUNT(*) FROM Ship";
                        MessageBox.Show("Количество: "+ Convert.ToString(countCommand.ExecuteScalar()));
                        break;
                    }
                case "Рейсы":
                    {
                        SqlCommand countCommand = connection.CreateCommand();
                        countCommand.CommandText = "SELECT COUNT(*) FROM Voyage";
                        MessageBox.Show("Количество: " + Convert.ToString(countCommand.ExecuteScalar()));
                        break;
                    }
                case "Накладные":
                    {
                        SqlCommand countCommand = connection.CreateCommand();
                        countCommand.CommandText = "SELECT COUNT(*) FROM Invoice";
                        MessageBox.Show("Количество: " + Convert.ToString(countCommand.ExecuteScalar()));
                        break;
                    }
                case "Контейнеры":
                    {
                        SqlCommand countCommand = connection.CreateCommand();
                        countCommand.CommandText = "SELECT COUNT(*) FROM Container";
                        MessageBox.Show("Количество: " + Convert.ToString(countCommand.ExecuteScalar()));
                        break;
                    }
                case "Накладные-Контейнеры":
                    {
                        SqlCommand countCommand = connection.CreateCommand();
                        countCommand.CommandText = "SELECT COUNT(*) FROM InvoiceContainer";
                        MessageBox.Show("Количество: " + Convert.ToString(countCommand.ExecuteScalar()));
                        break;
                    }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVIDDashboard
{
    public partial class DisplayInformation : Form
    {
        public string gender = string.Empty;
        public string imgLoc = string.Empty;
        public DisplayInformation()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton1.Location.Y);
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Hide();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton2.Location.Y);
            TravelInfo travelInfo = new TravelInfo();
            travelInfo.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton3.Location.Y);
            InsertCustomer insertCustomer = new InsertCustomer();
            insertCustomer.Show();
            
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton4.Location.Y);
            InsertEmployee insertEmployee = new InsertEmployee();
            insertEmployee.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton6.Location.Y);
            DisplayEmployer displayEmployer= new DisplayEmployer();
            displayEmployer.Show();
        }

        private void ClearData()
        {
            nameMetroBox.Text = string.Empty;
            ageMetroTextbox4.Text = string.Empty;
            addressMetroTextbox2.Text = string.Empty;
            barangayMetroTextbox3.Text = string.Empty;
            regionMetroTextbox5.Text = string.Empty;
            enteringMetroTextbox7.Text = string.Empty;
            departingMetroTextbox8.Text = string.Empty;
            companyTextBox.Text = string.Empty;
            pictureBox11.Image = null;
            maleCheckBox1.Checked = false;
            femaleCheckBox2.Checked = false;
            othersCheckBox3.Checked = false;
            
        }

        private void DisplayData()
        {
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            var select = "SELECT * FROM inserClient";
            var c = new SqlConnection(connectionString); // Your Connection String here
            var dataAdapter = new SqlDataAdapter(select, c);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gunaDataGridView1.ReadOnly = true;
            gunaDataGridView1.DataSource = ds.Tables[0];
            c.Close();
        }
        
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
                MemoryStream memory = new MemoryStream();
                pictureBox11.Image.Save(memory, pictureBox1.Image.RawFormat);
                byte[] pic = memory.ToArray();
                string connectionDate = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security=True;";
                SqlConnection sqlConnection = new SqlConnection(connectionDate);
                sqlConnection.Open();
                string queryUpdate = "UPDATE inserCLIENT SET Name= @Name,Address= @Address,Age= @Age,Entering= @Entering,Departing= @Departing,Gender= @Gender,personImage= @personImage,departure= @departure,Region= @Region,Barangay= @Barangay,NoOfCompany= @NoOfCompany WHERE Number= @Number;";
                SqlCommand command = new SqlCommand(queryUpdate, sqlConnection);
                command.Parameters.AddWithValue("@Name", nameMetroBox.Text);
                command.Parameters.AddWithValue("@Address", addressMetroTextbox2.Text);
                command.Parameters.AddWithValue("@Age", ageMetroTextbox4.Text);
                command.Parameters.AddWithValue("@Entering", enteringMetroTextbox7.Text);
                command.Parameters.AddWithValue("@Departing", departingMetroTextbox8.Text);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@Region", regionMetroTextbox5.Text);
                command.Parameters.AddWithValue("@Barangay", barangayMetroTextbox3.Text);
                command.Parameters.AddWithValue("@NoOfCompany", companyTextBox.Text);
                command.Parameters.AddWithValue("@Number", searchTextBox.text);
                command.Parameters.AddWithValue("@departure", totalPopulation.Text);
                command.Parameters.AddWithValue("@personImage", pic);
                command.ExecuteNonQuery();
                MessageBox.Show("Values have been updated.", "Patient updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sqlConnection.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            var select = "SELECT * FROM inserClient";
            var c = new SqlConnection(connectionString); // Your Connection String here
            var dataAdapter = new SqlDataAdapter(select, c);
            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gunaDataGridView1.ReadOnly = true;
            gunaDataGridView1.DataSource = ds.Tables[0];
            c.Close();
            int count = gunaDataGridView1.Rows.Count;
            activePop.Text = count.ToString();
            int activePercentage = (100 * int.Parse(activePop.Text)) / int.Parse(totalPopulation.Text);
            bunifuCircleProgressbar2.Value = activePercentage;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
                string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                 string queryDelete = "DELETE FROM inserCLIENT WHERE Number= @Number";
                SqlCommand sqlDeleteCommand = new SqlCommand(queryDelete, sqlConnection);
                sqlDeleteCommand.Parameters.AddWithValue("@Number", searchTextBox.text);
                sqlDeleteCommand.ExecuteNonQuery();
                MessageBox.Show("Database updated.", "Patient deleted.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sqlConnection.Close();
                ClearData();
           
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nameMetroBox.Text = this.gunaDataGridView1.CurrentRow.Cells[0].Value.ToString();
            addressMetroTextbox2.Text = this.gunaDataGridView1.CurrentRow.Cells[1].Value.ToString();
            ageMetroTextbox4.Text = this.gunaDataGridView1.CurrentRow.Cells[2].Value.ToString();
            enteringMetroTextbox7.Text = this.gunaDataGridView1.CurrentRow.Cells[3].Value.ToString();
            departingMetroTextbox8.Text = this.gunaDataGridView1.CurrentRow.Cells[4].Value.ToString();
            regionMetroTextbox5.Text = this.gunaDataGridView1.CurrentRow.Cells[8].Value.ToString();
            barangayMetroTextbox3.Text = this.gunaDataGridView1.CurrentRow.Cells[9].Value.ToString();
            companyTextBox.Text = this.gunaDataGridView1.CurrentRow.Cells[10].Value.ToString();
           

            if(this.gunaDataGridView1.CurrentRow.Cells[5].Value.ToString()=="Male")
            {
                maleCheckBox1.Checked = true;
            }
            else if(this.gunaDataGridView1.CurrentRow.Cells[5].Value.ToString()=="Female")
            {
                femaleCheckBox2.Checked = true;
            }
            else
            {
                othersCheckBox3.Checked = true;
            }


            byte[] bytes = (byte[])gunaDataGridView1.CurrentRow.Cells[6].Value;
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            pictureBox11.Image = img;

        }

        private void enteringMetroTextbox7_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void maleCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (maleCheckBox1.Checked == true)
            {
                gender = maleCheckBox1.Text;
                femaleCheckBox2.Checked = false;
                othersCheckBox3.Checked = false;
                femaleCheckBox2.Enabled = false;
                othersCheckBox3.Enabled = false;
            }
            else if (maleCheckBox1.Checked != true)
            {
                femaleCheckBox2.Enabled = true;
                othersCheckBox3.Enabled = true;
            }
        }

        private void femaleCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (femaleCheckBox2.Checked == true)
            {
                gender = femaleCheckBox2.Text;
                maleCheckBox1.Checked = false;
                othersCheckBox3.Checked = false;
                maleCheckBox1.Enabled = false;
                othersCheckBox3.Enabled = false;
            }
            else if (femaleCheckBox2.Checked != true)
            {
                maleCheckBox1.Enabled = true;
                othersCheckBox3.Enabled = true;
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Choose an image.";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                imgLoc = opf.FileName.ToString();
                pictureBox11.ImageLocation = imgLoc;
            }
        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }

        private void DisplayInformation_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            todayLabel.Text = dateTime.ToString();
        }
    }
}


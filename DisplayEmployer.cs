using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace COVIDDashboard
{
    public partial class DisplayEmployer : Form
    {
        public string imgLoc= string.Empty;
        public string gender = string.Empty;
        public DisplayEmployer()
        {
            InitializeComponent();
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton3.Location.Y);
            this.Hide();
            InsertCustomer insertCustomer = new InsertCustomer();
            insertCustomer.Show();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton2.Location.Y);
            TravelInfo travelInfo = new TravelInfo();
            travelInfo.Show();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            panel2.Location = new Point(0, bunifuImageButton1.Location.Y);
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton4.Location.Y);
            InsertEmployee insertEmployee = new InsertEmployee();
            insertEmployee.Show();
            this.Hide();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton5.Location.Y);
            DisplayInformation displayInformation = new DisplayInformation();
            displayInformation.Show();
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuMetroTextbox2_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void DisplayEmployer_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            todayLabel.Text = dateTime.ToString();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string queryDelete = "DELETE FROM insertMedicalStaff WHERE Number= @Number";
            SqlCommand sqlDeleteCommand = new SqlCommand(queryDelete, sqlConnection);
            sqlDeleteCommand.Parameters.AddWithValue("@Number", searchTextBox.text);
            sqlDeleteCommand.ExecuteNonQuery();
            MessageBox.Show("Database updated.", "Employee deleted.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sqlConnection.Close();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            var select = "SELECT * FROM insertMedicalStaff";
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            var c = new SqlConnection(connectionString); // Your Connection String here
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gunaDataGridView1.ReadOnly = true;
            gunaDataGridView1.DataSource = ds.Tables[0];

            int count = gunaDataGridView1.Rows.Count;
            activeStaff.Text = count.ToString();
            int activePercentage = (100 * int.Parse(activeStaff.Text)) / int.Parse(totalStaff.Text);
            bunifuCircleProgressbar2.Value = activePercentage;

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Choose an image.";
            if (opf.ShowDialog() == DialogResult.OK)
            {
                imgLoc = opf.FileName.ToString();
                pictureBox2.ImageLocation = imgLoc;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            MemoryStream memory = new MemoryStream();
            pictureBox2.Image.Save(memory, pictureBox2.Image.RawFormat);
            byte[] pic = memory.ToArray();
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            string queryUpdate = "UPDATE insertMedicalStaff SET Name=@Name, Address=@Address, Email=@Email, PhoneNumber=@PhoneNumber, Hospital= @Hospital, NoOfPeople=@NoOfPeople, Speciality=@Speciality, EducationQualification=@EducationQualification, imageLoc= @imageLoc, Gender=@Gender WHERE Number=@Number;";
            SqlCommand sqlCommands2 = new SqlCommand(queryUpdate, sqlConnection);
            sqlCommands2.Parameters.AddWithValue("@Name", nameMetroTextbox1.Text);
            sqlCommands2.Parameters.AddWithValue("@Address", addressMetroTextbox2.Text);
            sqlCommands2.Parameters.AddWithValue("@Email", occupationMetroTextbox3.Text);
            sqlCommands2.Parameters.AddWithValue("@PhoneNumber", int.Parse(ageMetroTextbox4.Text));
            sqlCommands2.Parameters.AddWithValue("@Hospital", clinicLocationMetroTextbox5.Text);
            sqlCommands2.Parameters.AddWithValue("@NoOfPeople", int.Parse(numberOfPeopleMetroTextbox6.Text));
            sqlCommands2.Parameters.AddWithValue("@Speciality", enteringMetroTextbox7.Text);
            sqlCommands2.Parameters.AddWithValue("@EducationQualification", departingMetroTextbox8.Text);
            sqlCommands2.Parameters.AddWithValue("@Gender", gender);
            sqlCommands2.Parameters.AddWithValue("@imageLoc", pic);
            sqlCommands2.Parameters.AddWithValue("@Number", searchTextBox.text);
              MessageBox.Show("Values have been updated.", "Patient updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sqlCommands2.ExecuteNonQuery();
            sqlConnection.Close();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            


        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            nameMetroTextbox1.Text = this.gunaDataGridView1.CurrentRow.Cells[0].Value.ToString();
            addressMetroTextbox2.Text = this.gunaDataGridView1.CurrentRow.Cells[1].Value.ToString();
            occupationMetroTextbox3.Text = this.gunaDataGridView1.CurrentRow.Cells[2].Value.ToString();
            ageMetroTextbox4.Text = this.gunaDataGridView1.CurrentRow.Cells[3].Value.ToString();
            clinicLocationMetroTextbox5.Text = this.gunaDataGridView1.CurrentRow.Cells[4].Value.ToString();
            numberOfPeopleMetroTextbox6.Text = this.gunaDataGridView1.CurrentRow.Cells[5].Value.ToString();
            enteringMetroTextbox7.Text = this.gunaDataGridView1.CurrentRow.Cells[6].Value.ToString();
            departingMetroTextbox8.Text = this.gunaDataGridView1.CurrentRow.Cells[7].Value.ToString();

            byte[] bytes = (byte[])gunaDataGridView1.CurrentRow.Cells[8].Value;
            MemoryStream ms = new MemoryStream(bytes);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            pictureBox2.Image = img;

            if(this.gunaDataGridView1.CurrentRow.Cells[9].Value.ToString()=="Male")
            {
                maleCheckBox1.Checked = true;
            }
            else if(this.gunaDataGridView1.CurrentRow.Cells[9].Value.ToString()=="Female")
            {
                femaleCheckBox2.Checked = true;
            }
            else
            {
                othersCheckBox3.Checked = true;
            }
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

        private void othersCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (othersCheckBox3.Checked == true)
            {
                gender = othersCheckBox3.Text;
                maleCheckBox1.Checked = false;
                femaleCheckBox2.Checked = false;
                maleCheckBox1.Enabled = false;
                femaleCheckBox2.Enabled = false;
            }
            else if (othersCheckBox3.Checked != true)
            {
                maleCheckBox1.Enabled = true;
                femaleCheckBox2.Enabled = true;
            }
        }
    }
}

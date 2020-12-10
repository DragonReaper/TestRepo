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
    public partial class InsertEmployee : Form
    {
        public string imgLoc=string.Empty;
        public string gender = string.Empty;
        public static int x = 0;
        public InsertEmployee()
        {
            InitializeComponent();
        }

        private void InsertEmployee_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            todayLabel.Text = dateTime.ToString();
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            panel2.Location = new Point(0, bunifuImageButton1.Location.Y);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            TravelInfo travelInfo = new TravelInfo();
            travelInfo.Show();
            panel2.Location = new Point(0, bunifuImageButton2.Location.Y);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton3.Location.Y);
            this.Hide();
            InsertCustomer insert = new InsertCustomer();
            insert.Show();
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton5.Location.Y);
            this.Hide();
            DisplayInformation displayInformation = new DisplayInformation();
            displayInformation.Show();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton6.Location.Y);
            this.Hide();
            DisplayEmployer displayEmployer = new DisplayEmployer();
            displayEmployer.Show();
        }

        private void bunifuMetroTextbox5_OnValueChanged(object sender, EventArgs e)
        {

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
            string name = nameMetroTextbox1.Text;
            string address = addressMetroTextbox2.Text;
            string occupation = occupationMetroTextbox3.Text;
            string age = ageMetroTextbox4.Text;
            string clinicLocation = clinicLocationMetroTextbox5.Text;
            string numberOfAccompanyingPeople = numberOfPeopleMetroTextbox6.Text;
            string entring = enteringMetroTextbox7.Text;
            string departing = departingMetroTextbox8.Text;
            bool isIllFilled = nameMetroTextbox1.Text != string.Empty && addressMetroTextbox2.Text != string.Empty && occupationMetroTextbox3.Text!=string.Empty && ageMetroTextbox4.Text!=string.Empty && clinicLocationMetroTextbox5.Text!=string.Empty && numberOfPeopleMetroTextbox6.Text!= string.Empty && enteringMetroTextbox7.Text!= string.Empty && departingMetroTextbox8.Text!= string.Empty && gender!=string.Empty && imgLoc!=string.Empty;
            x++;

            if (isIllFilled)
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                x++;
                string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string numberOfStaffQuery = "INSERT INTO insertMedicalStaff VALUES(@Name, @Address, @Email, @PhoneNumber,@Hospital, @NoOfPeople, @Speciality, @EducationQualification, @imageLoc, @Gender, @Number);";

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@Name";
                parameter1.Value = name;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@Address";
                parameter2.Value = address;

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@Email";
                parameter3.Value = occupation;

                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@PhoneNumber";
                parameter4.Value = int.Parse(age);

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@Hospital";
                parameter5.Value = clinicLocation;

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@NoOfPeople";
                parameter6.Value = int.Parse(numberOfAccompanyingPeople);

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@Speciality";
                parameter7.Value = entring;

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@EducationQualification";
                parameter8.Value = departing;

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@imageLoc";
                parameter9.Value = img;

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@Gender";
                parameter10.Value = gender;

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@Number";
                parameter11.Value = x;

                SqlCommand sqlCommand2 = new SqlCommand(numberOfStaffQuery, sqlConnection);
                sqlCommand2.Parameters.Add(parameter1);
                sqlCommand2.Parameters.Add(parameter2);
                sqlCommand2.Parameters.Add(parameter3);
                sqlCommand2.Parameters.Add(parameter4);
                sqlCommand2.Parameters.Add(parameter5);
                sqlCommand2.Parameters.Add(parameter6);
                sqlCommand2.Parameters.Add(parameter7);
                sqlCommand2.Parameters.Add(parameter8);
                sqlCommand2.Parameters.Add(parameter9);
                sqlCommand2.Parameters.Add(parameter10);
                sqlCommand2.Parameters.Add(parameter11);
           

                sqlCommand2.ExecuteNonQuery();

                MessageBox.Show("Values have already been inserted into the table", "Medical staff registered", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            if(!isIllFilled)
            {
                MessageBox.Show("Fill all information.");
            }
        



        }

        private void nameMetroTextbox1_OnValueChanged(object sender, EventArgs e)
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

        private void numberOfPeopleMetroTextbox6_OnValueChanged(object sender, EventArgs e)
        {

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

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            nameMetroTextbox1.Text = string.Empty;
            addressMetroTextbox2.Text = string.Empty;
            occupationMetroTextbox3.Text = string.Empty;
            ageMetroTextbox4.Text = string.Empty;
            clinicLocationMetroTextbox5.Text = string.Empty;
            numberOfPeopleMetroTextbox6.Text = string.Empty;
            enteringMetroTextbox7.Text = string.Empty;
            departingMetroTextbox8.Text = string.Empty;
            pictureBox2.Image = null;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}

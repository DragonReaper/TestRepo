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
    public partial class InsertCustomer : Form
    {
        public static int x = 0;
        public string imgLoc=string.Empty;
        public string gender = string.Empty;
        public InsertCustomer()
        {
            InitializeComponent();
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

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            InsertEmployee insertCustomer = new InsertEmployee();
            insertCustomer.Show();
            panel2.Location = new Point(0, bunifuImageButton4.Location.Y);
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            DisplayInformation displayInformation = new DisplayInformation();
            displayInformation.Show();
            panel2.Location = new Point(0, bunifuImageButton5.Location.Y);
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton6.Location.Y);
            this.Hide();
            DisplayEmployer displayEmployer = new DisplayEmployer();
            displayEmployer.Show();
        }

        private void InsertCustomer_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            todayLabel.Text = dateTime.ToString();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string name = nameMetroBox.Text;
            string address = addressMetroTextbox2.Text;
            string region = regionMetroTextbox5.Text;
            string age = ageMetroTextbox4.Text;
            string barangay = barangayMetroTextbox3.Text;
            string noofCompany = companyTextBox.Text;
            string departing = departingMetroTextbox8.Text;
            string entering = enteringMetroTextbox7.Text;
            string departureText = todayLabel.Text;
          
            x++;
          
            bool checkIfall= nameMetroBox.Text!=string.Empty && addressMetroTextbox2.Text!=string.Empty && barangayMetroTextbox3.Text!=string.Empty && ageMetroTextbox4.Text!= string.Empty && regionMetroTextbox5.Text!= string.Empty && companyTextBox.Text!=string.Empty && departingMetroTextbox8.Text!=string.Empty && enteringMetroTextbox7.Text!=string.Empty && imgLoc!=string.Empty && gender!=string.Empty;

            if (checkIfall)
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);

                string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                string patientQuery = "INSERT INTO inserClient VALUES(@Name, @Address, @Age, @Entering, @Departing, @Gender, @personImage, @departure, @Region, @Barangay, @NoOfCompany, @Number);";

                SqlParameter parameter1 = new SqlParameter();
                parameter1.ParameterName = "@Name";
                parameter1.Value = name;

                SqlParameter parameter2 = new SqlParameter();
                parameter2.ParameterName = "@Address";
                parameter2.Value = address;

                SqlParameter parameter3 = new SqlParameter();
                parameter3.ParameterName = "@Age";
                parameter3.Value = int.Parse(age);


                SqlParameter parameter4 = new SqlParameter();
                parameter4.ParameterName = "@Region";
                parameter4.Value = region;

                SqlParameter parameter5 = new SqlParameter();
                parameter5.ParameterName = "@Barangay";
                parameter5.Value = barangay;

                SqlParameter parameter6 = new SqlParameter();
                parameter6.ParameterName = "@NoOfCompany";
                parameter6.Value = noofCompany;

                SqlParameter parameter7 = new SqlParameter();
                parameter7.ParameterName = "@Departing";
                parameter7.Value = departing;

                SqlParameter parameter8 = new SqlParameter();
                parameter8.ParameterName = "@Entering";
                parameter8.Value = entering;

                SqlParameter parameter9 = new SqlParameter();
                parameter9.ParameterName = "@personImage";
                parameter9.Value = img;

                SqlParameter parameter10 = new SqlParameter();
                parameter10.ParameterName = "@departure";
                parameter10.Value = departureText;

                SqlParameter parameter11 = new SqlParameter();
                parameter11.ParameterName = "@Gender";
                parameter11.Value = gender;

                SqlParameter parameter12 = new SqlParameter();
                parameter12.ParameterName = "@Number";
                parameter12.Value = x;


                SqlCommand sqlCommand2 = new SqlCommand(patientQuery, sqlConnection);
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
                sqlCommand2.Parameters.Add(parameter12);

                sqlCommand2.ExecuteNonQuery();
                

                MessageBox.Show("Values have already been inserted into the table", "Client registered", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            if (!checkIfall)
            {
                MessageBox.Show("Fill all information.");
            }

        }

        private void nameMetroBox_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Choose an image.";
            if(opf.ShowDialog()== DialogResult.OK)
            {
                imgLoc = opf.FileName.ToString();
                pictureBox2.ImageLocation = imgLoc;
            }

        }

        private void nationalityMetroTextbox5_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void maleCheckbox1_OnChange(object sender, EventArgs e)
        {

        }

        private void otherLabel_Click(object sender, EventArgs e)
        {

        }

        private void femaleCheckbox2_OnChange(object sender, EventArgs e)
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
            else if(maleCheckBox1.Checked!=true)
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
           else if(femaleCheckBox2.Checked!=true)
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
           else if(othersCheckBox3.Checked!=true)
           {
                maleCheckBox1.Enabled = true;
                femaleCheckBox2.Enabled = true;
           }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barangayMetroTextbox3_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            nameMetroBox.Text = string.Empty;
            addressMetroTextbox2.Text = string.Empty;
            barangayMetroTextbox3.Text = string.Empty;
            ageMetroTextbox4.Text = string.Empty;
            companyTextBox.Text = string.Empty;
            departingMetroTextbox8.Text = string.Empty;
            enteringMetroTextbox7.Text = string.Empty;
            regionMetroTextbox5.Text = string.Empty;
            maleCheckBox1.Checked = false;
            femaleCheckBox2.Checked = false;
            othersCheckBox3.Checked = false;
            pictureBox2.Image = null;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            var select = "SELECT * FROM inserClient";
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            var c = new SqlConnection(connectionString); // Your Connection String here
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gunaDataGridView1.ReadOnly = true;
            gunaDataGridView1.DataSource = ds.Tables[0];
        }

        private void bunifuThinButton22_Click_1(object sender, EventArgs e)
        {
            var select = "SELECT * FROM inserClient";
            string connectionString = "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog= FINALS; Integrated Security= True;";
            var c = new SqlConnection(connectionString); // Your Connection String here
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            gunaDataGridView1.ReadOnly = true;
            gunaDataGridView1.DataSource = ds.Tables[0];

            int count = gunaDataGridView1.Rows.Count;
            activePop.Text = count.ToString();
            int activePercentage = (100 * int.Parse(activePop.Text)) / int.Parse(totalPopulation.Text);
            bunifuCircleProgressbar2.Value = activePercentage;
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void deathCasesLabel_Click(object sender, EventArgs e)
        {

        }
    }
}

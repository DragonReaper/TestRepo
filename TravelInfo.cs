using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVIDDashboard
{
    public partial class TravelInfo : Form
    {
        public TravelInfo()
        {
            InitializeComponent();
        }

        private void TravelInfo_Load(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton1.Location.Y);
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton3.Location.Y);
            this.Hide();
            InsertCustomer insertEmployee = new InsertCustomer();
            insertEmployee.Show();
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
            panel2.Location = new Point(0, bunifuImageButton5.Location.Y);
            DisplayInformation displayInformation = new DisplayInformation();
            displayInformation.Show();
            this.Hide();
        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton6.Location.Y);
            DisplayEmployer displayEmployer = new DisplayEmployer();
            displayEmployer.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

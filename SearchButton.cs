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
    public partial class SearchButton : Form
    {
        public DataTable countryData { get; set; }
        public DialogResult result { get; set; }
        public string selectedCountry { get; set; }
        public SearchButton()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            searchTextBox.Text = string.Empty;
            this.Close();
        }

        private void SearchButton_Load(object sender, EventArgs e)
        {
            searchTextBox.Focus();
            searchTextBox.Text = string.Empty;
            gunaDataGridView1.DataSource = countryData;
            gunaVScrollBar1.Maximum = countryData.Rows.Count;
        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SearchButton_Deactivate(object sender, EventArgs e)
        {
            searchTextBox.Text = string.Empty;
            gunaDataGridView1.DataSource = countryData;
            this.Close();
        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaDataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            if(searchTextBox.Text==string.Empty)
            {
                gunaDataGridView1.DataSource = countryData;
            }

            try
            {
                countryData.DefaultView.RowFilter = string.Format("name LIKE '{0}*'", searchTextBox.text);
                if (countryData.DefaultView.Count == 0)
                {
                    gunaVScrollBar1.Enabled = false;
                }
                else
                {
                    gunaVScrollBar1.Enabled = true;
                    gunaVScrollBar1.Maximum = countryData.DefaultView.Count;
                }

            }
            catch(Exception)
            {

            }
         
        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
           
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedCountry = gunaDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            result = DialogResult.OK;
            this.Close();

        }

        private void bunifuCustomTextbox1_TextChanged_1(object sender, EventArgs e)
        {
          
           
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                gunaDataGridView1.FirstDisplayedScrollingRowIndex = e.NewValue;
                gunaDataGridView1.FirstDisplayedScrollingRowIndex = e.OldValue;
                gunaDataGridView1.DataSource = countryData;
            }
            catch(Exception)
            {

            }
        }

        private void vScrollBar1_Scroll_1(object sender, ScrollEventArgs e)
        {
            
        }
    }
}


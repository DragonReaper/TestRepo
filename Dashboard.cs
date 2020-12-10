using Bunifu.DataViz;
using Newtonsoft.Json;
using RestSharp;
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
    public partial class Dashboard : Form
    {
        DataTable countriesDataTable = new DataTable();
        DataTable historyDataTable = new DataTable();
        string HttpFeedBack, countryName;
        WorldStat worldStat;

        //DataVizualizationOfData
        DataPoint newCasesDataPoint = new DataPoint(BunifuDataViz._type.Bunifu_column);
        DataPoint deathCasesDataPoint = new DataPoint(BunifuDataViz._type.Bunifu_column);
        DataPoint totalCasesDataPoint = new DataPoint(BunifuDataViz._type.Bunifu_column);
        DataPoint criticalCasesDataPoint = new DataPoint(BunifuDataViz._type.Bunifu_column);
        public Dashboard()
        {
            InitializeComponent();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            panel2.Location = new Point(0, bunifuImageButton1.Location.Y);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            TravelInfo travelInfo = new TravelInfo();
            travelInfo.Show();
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton2.Location.Y);
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            InsertCustomer insertCustomer = new InsertCustomer();
            insertCustomer.Show();
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton3.Location.Y);
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            InsertEmployee insertEmployee = new InsertEmployee();
            insertEmployee.Show();
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton4.Location.Y);
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            DisplayInformation displayInformation = new DisplayInformation();
            displayInformation.Show();
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton5.Location.Y);
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            DisplayEmployer displayEmployer = new DisplayEmployer();
            displayEmployer.Show();
            this.Hide();
            panel2.Location = new Point(0, bunifuImageButton6.Location.Y);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            loadingScreen1.Visible = true;
            backgroundWorker1.RunWorkerAsync();
            DateTime dateTime = DateTime.Now;
            todayLabel.Text = dateTime.ToString();

        }

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuImageButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuCustomDataGrid1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

            SearchButton searchButton = new SearchButton();
            searchButton.countryData = countriesDataTable;
            searchButton.StartPosition = FormStartPosition.Manual;
            searchButton.Location = new Point(countryBunifuButton.Location.X + 235, countryBunifuButton.Location.Y + 215);
            searchButton.ShowDialog();
            if (searchButton.result == DialogResult.OK)
            {
                countryLabel.Text = searchButton.selectedCountry;
                countryLabel2.Text = searchButton.selectedCountry;
                countryName = searchButton.selectedCountry;
                loadingScreen1.Visible = true;
           

                if(historyDataTable.Columns.Count>0)
                {
                    historyDataTable.Columns.Clear();
                }
                if (historyDataTable.Rows.Count > 0)
                {
                    historyDataTable.Rows.Clear();
                }
                gunaDataGridView1.DataSource = null;
                newCasesDataPoint.clear();
                deathCasesDataPoint.clear();
                totalCasesDataPoint.clear();
                criticalCasesDataPoint.clear();
                
                backgroundWorker1.RunWorkerAsync();
               
            }
        }

        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void countryBunifuButton_DoubleClick(object sender, EventArgs e)
        {
            SearchButton searchButton = new SearchButton();
            searchButton.Close();
        }

        private void loadingScreen1_Load(object sender, EventArgs e)
        {

        }

    
        private void getAllCountries()
        {
            var client = new RestClient("https://restcountries-v1.p.rapidapi.com/all");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "1988653984mshf4117a59927ac63p1444d8jsn9b0bca7ad496");
            request.AddHeader("x-rapidapi-host", "restcountries-v1.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var countries = JsonConvert.DeserializeObject<List<Country>>(content);
                countriesDataTable.Columns.Add("name");
                foreach (var country in countries)
                {
                    countriesDataTable.Rows.Add(country.name);
                }
            }
            else
            {
                HttpFeedBack = response.ErrorMessage;
                backgroundWorker1.CancelAsync();
            }


        }

   
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {

        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if(!string.IsNullOrEmpty(countryName))
            {
                GetCOVID19HistoryData(countryName);
            }
            else
            {
                getAllCountries();
                worldStatistics();
            }


            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) {
                timer1.Start();
                MessageBox.Show(HttpFeedBack);

                #region default_values
                activeCasesLabel.Text = "0";
                totalCasesLabel.Text = "0";
                criticalCasesLabel.Text = "0";
                deathTotalCasesLabel.Text = "0";
                criticalTotalCasesLabel.Text = "0";
                deathTotalCasesLabel.Text = "0";

                bunifuCircleProgressbar1.Value = 0;
                bunifuCircleProgressbar2.Value = 0;
                bunifuCircleProgressbar3.Value = 0;
                #endregion

                loadingScreen1.Visible = false;
            }
            else
            {
                Console.WriteLine(historyDataTable.Rows.Count);  
                if (historyDataTable.Rows.Count>0)
                {
                    gunaDataGridView1.DataSource = historyDataTable;
                    gunaDataGridView1.Columns[0].Width = 110;

                    activeCasesLabel.Text = historyDataTable.Rows[0]["active cases"].ToString();
                    totalCasesLabel.Text = historyDataTable.Rows[0]["total cases"].ToString();
                    criticalCasesLabel.Text = historyDataTable.Rows[0]["total critical"].ToString();
                    deathCasesLabel.Text = historyDataTable.Rows[0]["total deaths"].ToString();
                    criticalTotalCasesLabel.Text = historyDataTable.Rows[0]["total cases"].ToString();
                    deathTotalCasesLabel.Text = historyDataTable.Rows[0]["total cases"].ToString();

                    int activeCaseNumbers = int.Parse(activeCasesLabel.Text, System.Globalization.NumberStyles.AllowThousands);
                    int deathCaseNumbers = int.Parse(deathCasesLabel.Text, System.Globalization.NumberStyles.AllowThousands);
                    int criticalCaseNumbers = int.Parse(criticalCasesLabel.Text, System.Globalization.NumberStyles.AllowThousands);
                    int totalCasesNumbers = int.Parse(totalCasesLabel.Text, System.Globalization.NumberStyles.AllowThousands);

                    int activePercentage = (100 * activeCaseNumbers) / totalCasesNumbers;
                    int criticalPercentage = (100 * criticalCaseNumbers) / totalCasesNumbers;
                    int deathPercentage = (100 * deathCaseNumbers) / totalCasesNumbers;

                    bunifuCircleProgressbar1.Value = activePercentage;
                    bunifuCircleProgressbar2.Value = deathPercentage;
                    bunifuCircleProgressbar3.Value = criticalCaseNumbers;

                    Canvas statistics = new Canvas();
                    statistics.addData(newCasesDataPoint);
                    statistics.addData(totalCasesDataPoint);
                    statistics.addData(deathCasesDataPoint);
                    statistics.addData(criticalCasesDataPoint);

                    bunifuDataViz1.colorSet.Add(Color.FromArgb(159, 134, 255));
                    bunifuDataViz1.colorSet.Add(Color.FromArgb(0, 122, 255));
                    bunifuDataViz1.colorSet.Add(Color.FromArgb(243, 249, 210));
                    bunifuDataViz1.colorSet.Add(Color.FromArgb(44,43,60));

                    bunifuDataViz1.Render(statistics);
                }
                timer1.Start();
                MessageBox.Show("Data loaded.");
                totalCases.Text = worldStat.total_cases;
                    totalInfected.Text = worldStat.total_recovered;
                    totalDeaths.Text = worldStat.total_deaths;
                     loadingScreen1.Visible = false;
                
              
               
                
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            loadingScreen1.Visible = false;
            timer1.Stop();
          
        }

        private void bunifuDataViz1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void activeCasesLabel_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomDataGrid1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void GetCOVID19HistoryData(string country)
        {
            var client = new RestClient($"https://coronavirus-monitor.p.rapidapi.com/coronavirus/cases_by_particular_country.php?country={countryName}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "1988653984mshf4117a59927ac63p1444d8jsn9b0bca7ad496");
            request.AddHeader("x-rapidapi-host", "coronavirus-monitor.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            if(response.StatusCode== System.Net.HttpStatusCode.OK)
            {
                var content = response.Content;
                var historyData = JsonConvert.DeserializeObject<History_stats>(content);

                if(historyData!= null)
                {
                    DataColumn[] dataColumn = new DataColumn[]
                    {
                      new DataColumn("record date"),
                      new DataColumn("total cases"),
                      new DataColumn("new cases"),
                      new DataColumn("active cases"),
                      new DataColumn("total deaths"),
                      new DataColumn("total recovered"),
                      new DataColumn("total critical")
                     };

                    DateTime[] last_seven_days = Enumerable.Range(0, 7).Select(i => DateTime.Now.Date.AddDays(-i)).ToArray();
                    //historyData.stat_by_country.Reverse();
                    historyDataTable.Columns.AddRange(dataColumn);

                    Dictionary<string, int> casesDictionary = new Dictionary<string, int>();
                    Dictionary<string, int> totalDictionary = new Dictionary<string, int>();
                    Dictionary<string, int> deathDictionary = new Dictionary<string, int>();
                    Dictionary<string, int> criticalDictionary = new Dictionary<string, int>();


                    foreach(var lastDay in last_seven_days)
                    {
                        foreach (var x in historyData.stat_by_country)
                        {
                            #region convert_data
                            if (x.new_cases == "")
                            {
                                x.new_cases = "0";
                            }
                            if (x.total_cases == "")
                            {
                                x.total_cases = "0";
                            }
                            if (x.active_cases == "")
                            {
                                x.active_cases = "0";
                            }
                            if (x.total_recovered == "")
                            {
                                x.total_recovered = "0";
                            }
                            if (x.serious_critical == "")
                            {
                                x.serious_critical = "0";
                            }
                            if (x.total_deaths == "")
                            {
                                x.total_deaths = "0";
                            }
#endregion

                            if(x.record_date.Contains($"{lastDay:yyyy-MM-dd}"))
                            {
                                DateTime dateTime = new DateTime(lastDay.Date.Year, lastDay.Date.Month, lastDay.Date.Day) ;
                                historyDataTable.Rows.Add($"{lastDay:dd-MM-yyyy}"+""+dateTime.ToString("ddd"), x.total_cases, x.new_cases, x.active_cases, x.total_deaths, x.total_recovered, x.serious_critical);
                                casesDictionary.Add(dateTime.ToString("ddd"), int.Parse(x.total_cases, System.Globalization.NumberStyles.AllowThousands));
                                totalDictionary.Add(dateTime.ToString("ddd"), int.Parse(x.total_recovered, System.Globalization.NumberStyles.AllowThousands));
                                deathDictionary.Add(dateTime.ToString("ddd"), int.Parse(x.total_deaths, System.Globalization.NumberStyles.AllowThousands));
                                criticalDictionary.Add(dateTime.ToString("ddd"), int.Parse(x.active_cases, System.Globalization.NumberStyles.AllowThousands));

                                break;
                            }
                            

                            
                        }
                    }

                    var newCasesDictonaryReversal = casesDictionary.Reverse();
                    var totalCasesDictionaryReversal = totalDictionary.Reverse();
                    var deathDictionaryReversal = deathDictionary.Reverse();
                    var criticalDictionaryReversal = criticalDictionary.Reverse();

                    foreach(var dataNewCases in newCasesDictonaryReversal)
                    {
                        newCasesDataPoint.addLabely(dataNewCases.Key,dataNewCases.Value.ToString());
                    }

                    foreach(var datatotalCases in totalCasesDictionaryReversal)
                    {
                        totalCasesDataPoint.addLabely(datatotalCases.Key, datatotalCases.Value.ToString());
                    }

                    foreach(var dataDeathCases in deathDictionaryReversal)
                    {
                        deathCasesDataPoint.addLabely(dataDeathCases.Key, dataDeathCases.Value.ToString());
                    }

                    foreach(var dataCriticalCases in criticalDictionaryReversal)
                    {
                        criticalCasesDataPoint.addLabely(dataCriticalCases.Key, dataCriticalCases.Value.ToString());
                    }
                    
                 
                }
                else
                {
                    HttpFeedBack = "No data";
                    backgroundWorker1.CancelAsync();
                }
            }
            else
            {
                HttpFeedBack = response.StatusDescription;
                backgroundWorker1.CancelAsync();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
           
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void worldStatistics()
        {
            var client = new RestClient("https://coronavirus-monitor.p.rapidapi.com/coronavirus/worldstat.php");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "1988653984mshf4117a59927ac63p1444d8jsn9b0bca7ad496");
            request.AddHeader("x-rapidapi-host", "coronavirus-monitor.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            if(response.StatusCode==System.Net.HttpStatusCode.OK)
            {
                var stringResponse = response.Content;
                worldStat = JsonConvert.DeserializeObject<WorldStat>(stringResponse);
            }
            else
            {
                HttpFeedBack = response.ErrorMessage;
                backgroundWorker1.CancelAsync();
            }
        }
    }
}

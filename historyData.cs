using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVIDDashboard
{
    class historyData
    {
        public string country{get;set;}
        public string cases { get; set; }
        public string deaths { get; set; }

        public string active { get; set; }
        
    }

    class StatsByCountry2
    {
        public string cases { get; set; }
        public string todayCases { get; set; }
        public string deaths { get; set; }
        public string todayDeaths { get; set; }
        public string recovered { get; set; }
        public string todayRecovered { get; set; }
        public string active { get; set; }
        public string critical { get; set; }
        public string casesPerOneMillion { get; set; }
        public string deathsPerOneMillion { get; set; }
        public string tests { get; set; }
        public string testsPerOneMillion { get; set; }
        public string population { get; set; }
        public string continent { get; set; }
        public int oneCasePerPeople { get; set; }
        public int oneDeathPerPeople { get; set; }
        public int oneTestPerPeople { get; set; }
        public int activePerOneMillion { get; set; }
        public double recoveredPerOneMillion { get; set; }
        public double criticalPerOneMillion { get; set; }
    }

    class historyDataOfCovid 
    {
        public string country { get; set; }
    }

    class timeline
    {
        public string cases { get; set; }
    }


    public class RootSlang
    {

    }

    public class countryStats
    {

    }
}

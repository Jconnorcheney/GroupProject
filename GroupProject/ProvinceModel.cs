using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProject
{

    public class ProvinceModel
    {


        public string province { get; set; }
        public long activeCases { get; set; }
        public long cumulativeCases { get; set; }
        public long cumulativeDeaths { get; set; }
        public long cumulativeVaccine { get; set; }
        public long cumulativeRecovered { get; set; }
        public long cumulativeTesting { get; set; }
        public string provinceFlag { get; set; }



        public ProvinceModel(string province, long activeCases, long cumulativeCases, long cumulativeDeaths, long cumulativeVaccine, long cumulativeRecovered, long cumulativeTesting, string provFlag)
        {
            this.province = province;
            this.activeCases = activeCases;
            this.cumulativeCases = cumulativeCases;
            this.cumulativeDeaths = cumulativeDeaths;
            this.cumulativeVaccine = cumulativeVaccine;
            this.cumulativeRecovered = cumulativeRecovered;
            this.cumulativeTesting = cumulativeTesting;
            this.provinceFlag = provFlag;

        }



    }




}




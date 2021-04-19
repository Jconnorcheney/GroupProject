using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Search;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using System.IO;
using System.Threading;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProject
{
    public class ProvinceViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<ProvinceModel> Files { get; set; }
        public List<ProvinceModel> _allFiles = new List<ProvinceModel>();
        public event PropertyChangedEventHandler PropertyChanged;
        private ProvinceModel _selectedFile;
        private string _filter;



        public string province { get; set; }
        public long activeCases { get; set; }
        public long cumulativeCases { get; set; }
        public long cumulativeDeaths { get; set; }
        public long cumulativeVaccine { get; set; }
        public long cumulativeRecovered { get; set; }
        public long cumulativeTesting { get; set; }
        public BitmapImage provinceFlag { get; set; }



        //public string countryName { get; set; }
        //public string countryPop { get; set; }
        //public string countryCapitol { get; set; }

        //public string totalCases { get; set; }
        //public string totalRecovered { get; set; }
        //public string totalDeaths { get; set; }

        //public string totalVacines { get; set; }
        //public string totalPartialVacines { get; set; }
        //public string totalUnvaccinated { get; set; }


        public ProvinceModel SelectedFile
        {
            get { return _selectedFile; }
            set
            {
                _selectedFile = value;
                //If the file is empty
                if (value == null)
                { //Ouput that its empty

                }
                else //Set its text to the files text
                {
                    province = value.province;
                    activeCases = value.activeCases;
                    cumulativeCases = value.cumulativeCases;
                    cumulativeDeaths = value.cumulativeDeaths;
                    cumulativeVaccine = value.cumulativeVaccine;
                    cumulativeRecovered = value.cumulativeRecovered;
                    cumulativeTesting = value.cumulativeTesting;
                    provinceFlag = value.provinceFlag;

                }

                //TODO Property for starter pages variables
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("province"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("activeCases"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("cumulativeRecovered"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("provinceFlag"));

            }
        }

        public ProvinceViewModel()
        {

            //Create the colelction
            Files = new ObservableCollection<ProvinceModel>();

            //Create the collection
            CreateCollection();

            //Perform Filtering
            PerformFiltering();
        }


        public async void CreateCollection()
        {
            _allFiles.Clear();
            Files.Clear();

            FetchData tmpFetch = new FetchData();

            //tmpFetch.GetData();
            //ProvinceModel provinceDemo = new ProvinceModel("Ontario", 100, 0, 1000, 123, 123, 100, "./Assets/Flags/Flag_of_Ontario.png");
            //_allFiles.Add(provinceDemo);
            _allFiles = await tmpFetch.GetData();


            PerformFiltering();
        }


        public void PerformFiltering()
        {
            Files.Clear();

            //If filter is null set it to ""
            if (_filter == null)
            {
                _filter = "";
            }
            //If _filter has a value (ie. user entered something in Filter textbox)
            //Lower-case and trim string
            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            //Use LINQ query to get all personmodel names that match filter text, as a list
            var result =
                _allFiles.Where(d => d.province.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            //Get list of values in current filtered list that we want to remove
            //(ie. don't meet new filter criteria)
            var toRemove = Files.Except(result).ToList();

            //Loop to remove items that fail filter
            foreach (var x in toRemove)
            {
                Files.Remove(x);
            }

            var resultCount = result.Count;

            // Add back in correct order.
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Files.Count || !Files[i].Equals(resultItem))
                {
                    Files.Insert(i, resultItem);
                }
            }
        }


        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter) { return; }
                _filter = value;
                PerformFiltering();
                //Invovoked whenever the property is changed
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }





    }
}

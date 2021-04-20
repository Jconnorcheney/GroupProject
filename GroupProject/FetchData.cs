using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace GroupProject
{

    public class FetchData
    {
        public FetchData()
        {

        }


        public async Task<List<ProvinceModel>> GetData()
        {
            List<ProvinceModel> provinces = new List<ProvinceModel>();

            string baseUrl = "https://api.opencovid.ca/summary";

            string province;
            long activeCases;
            long cumalativeCases;
            long cumalativeDeaths;
            long cumalativeVaccine;
            long cumalativeRecovered;
            long cumalativeTesting;
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {

                        using (HttpContent content = res.Content)
                        {
                            // get the content of the http response message and converted to a string
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {

                                var dataObj = JObject.Parse(data);
                                int length = ((JArray)dataObj["summary"]).Count;


                                for (int i = 0; i < length; i++)
                                {
                                    // fetch the json data and convert to appropriate data type
                                    if (!($"{dataObj["summary"][i]["province"]}").Equals("Repatriated"))
                                    {
                                        province = ($"{dataObj["summary"][i]["province"]}");
                                        activeCases = long.Parse(($"{dataObj["summary"][i]["active_cases"]}"));
                                        cumalativeCases = long.Parse(($"{dataObj["summary"][i]["cumulative_cases"]}"));
                                        cumalativeDeaths = long.Parse(($"{dataObj["summary"][i]["cumulative_deaths"]}"));
                                        cumalativeVaccine = long.Parse($"{dataObj["summary"][i]["cumulative_dvaccine"]}");
                                        cumalativeRecovered = long.Parse(($"{dataObj["summary"][i]["cumulative_recovered"]}"));
                                        cumalativeTesting = long.Parse(($"{dataObj["summary"][i]["cumulative_testing"]}"));

                                        provinces.Add(GetProvinceData(province, activeCases, cumalativeCases, cumalativeDeaths, cumalativeVaccine,
                                            cumalativeRecovered, cumalativeTesting, "No Image Supplied Give Error Pls"));
                                    }

                                }

                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine("Oh noes! Can not read data " + ex.Message);
            }

            return provinces;

        }

        // create and return ProvinceModel 
        public static ProvinceModel GetProvinceData(string province, long aCases, long cACases, long cDeath, long cVaccine, long cRecovered, long cTesting, string provImage)
        {
            return new ProvinceModel(province, aCases, cACases, cDeath, cVaccine, cRecovered, cTesting, provImage);
        }

    }
}
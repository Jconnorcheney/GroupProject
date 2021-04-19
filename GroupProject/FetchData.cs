using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject
{
    public class FetchData
    {
        public List<ProvinceModel> provinces;

        public async void GetData()
        {
            this.provinces = new List<ProvinceModel>();

            string baseUrl = "https://api.opencovid.ca/summary";

            string province;
            long activeCases;
            long cumalativeCases;
            long cumalativeDeaths;
            long cumalativeVaccine;
            long cumalativeRecovered;
            long cumalativeTesting;
            string provImage = "";

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {

                        using (HttpContent content = res.Content)
                        {
                            var data = await content.ReadAsStringAsync();

                            if (data != null)
                            {

                                var dataObj = JObject.Parse(data);
                                int length = ((JArray)dataObj["summary"]).Count;


                                for (int i = 0; i < length; i++)
                                {
                                    if (!($"{dataObj["summary"][i]["province"]}").Equals("Repatriated"))
                                    {
                                        province = ($"{dataObj["summary"][i]["province"]}");
                                        activeCases = long.Parse(($"{dataObj["summary"][i]["active_cases"]}"));
                                        cumalativeCases = long.Parse(($"{dataObj["summary"][i]["cumulative_cases"]}"));
                                        cumalativeDeaths = long.Parse(($"{dataObj["summary"][i]["cumulative_deaths"]}"));
                                        cumalativeVaccine = long.Parse($"{dataObj["summary"][i]["cumulative_dvaccine"]}");
                                        cumalativeRecovered = long.Parse(($"{dataObj["summary"][i]["cumulative_recovered"]}"));
                                        cumalativeTesting = long.Parse(($"{dataObj["summary"][i]["cumulative_testing"]}"));

                                        provImage = GetProvinceFlag(province);

                                        Debug.WriteLine(province + " " + activeCases + " " + cumalativeCases + " " + cumalativeDeaths
                                            + " " + cumalativeVaccine + " " + cumalativeRecovered + " " + cumalativeTesting);

                                        this.provinces.Add(GetProvinceData(province, activeCases, cumalativeCases, cumalativeDeaths, cumalativeVaccine,
                                            cumalativeRecovered, cumalativeTesting, provImage));
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

        }

        public static ProvinceModel GetProvinceData(string province, long aCases, long cACases, long cDeath, long cVaccine, long cRecovered, long cTesting, string provImage)
        {
            return new ProvinceModel(province, aCases, cACases, cDeath, cVaccine, cRecovered, cTesting, provImage);
        }

        public string GetProvinceFlag(string provinceName)
        {
            string pImage = "";

            switch (provinceName)
            {
                case "Alberta":
                    pImage = "./Assets/Flags/Flag_of_Alberta.png";
                    break;

                case "BC":
                    pImage = "./Assets/Flags/Flag_of_British_Columbia.png";
                    break;

                case "New Brunswick":
                    pImage = "./Assets/Flags/Flag_of_New_Brunswick.png";
                    break;

                case "NL":
                    pImage = "./Assets/Flags/Flag_of_Newfoundland_and_Labrador.png";
                    break;

                case "Nova Scotia":
                    pImage = "./Assets/Flags/Flag_of_Nova_Scotia.png";
                    break;

                case "Nunavut":
                    pImage = "./Assets/Flags/Flag_of_Nunavut.png";
                    break;

                case "NWT":
                    pImage = "./Assets/Flags/Flag_of_the_Northwest_Territories.png";
                    break;

                case "Ontario":
                    pImage = "./Assets/Flags/Flag_of_Ontario.png";
                    break;

                case "PEI":
                    pImage = "./Assets/Flags/Flag_of_Prince_Edward_Island.png";
                    break;

                case "Quebec":
                    pImage = "./Assets/Flags/Flag_of_Quebec.png";
                    break;

                case "Saskatwchean":
                    pImage = "./Assets/Flags/Flag_of_Saskatchewan.png";
                    break;

                case "Yukon":
                    pImage = "./Assets/Flags/Flag_of_Yukon.png";
                    break;
            }

            return pImage;


        }
    }

}

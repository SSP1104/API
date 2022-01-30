using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarApi
{
    class CarApi
    {
        public static async Task Main()
        {
            Api api = new Api();
            await api.ApiCars();
            String allCars = api.GetCars();
            allCars = allCars.Replace("},{", "|");
            allCars = allCars.Trim(new Char[] {'[',']','{','}'});
            string[] carData = allCars.Split("|");
            List<List<string>> CarList = new List<List<string>>();
            for (int i = 0; i < carData.Length; i++)
            {
                string[] param = carData[i].Split(",");
                CarList.Add(new List<string> {param[0],param[1],param[2],param[3],param[4]});
            }
            foreach (List<string> subList in CarList)
            {
                foreach (string item in subList)
                {
                    Console.WriteLine(item);
                }
            }


        }
    }
    class Api
        {
            String Cars;
            public async Task ApiCars() 
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://car-data.p.rapidapi.com/cars/?limit=50"),
                    Headers =
    {
        { "x-rapidapi-host", "car-data.p.rapidapi.com" },
        { "x-rapidapi-key", "b8973fd7f8msh6cca9288561ff9cp1119eajsn2c644287a966" },
    },
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    Cars = body;
                }
            }
        public string GetCars()
        {
            return Cars;
        }
        
        }
    
}

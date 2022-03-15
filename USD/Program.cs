using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
namespace USD
{
    internal class Program
    {


        public static float BubleSort(List<float> collection, List<DateTime> days, int n)
        {
            float temp = 0;
            DateTime day = new();
            for (int write = 0; write < n; write++)
            {
                for (int sort = 0; sort < n - 1; sort++)
                {
                    if (collection[sort] > collection[sort + 1])
                    {
                        temp = collection[sort + 1];
                        day = days[sort + 1];
                        days[sort + 1] = days[sort];
                        days[sort] = day;
                        collection[sort + 1] = collection[sort];
                        collection[sort] = temp;
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine(days[0]);
            return collection[0];
        }
        public class USD
        {
            public async Task<string> GetJsonData(string url)
            {
                var client = new HttpClient();
                var prodResp = await client.GetAsync(url);
                var prods = await prodResp.Content.ReadAsStringAsync();
                return prods;
            }

        }
        public static async Task Main()
        {
            DateTime date = new DateTime(2020, 1, 1);
            USD us = new();

            var days = new List<DateTime>();
            var UAHtoUSD = new List<float>();
            int days_n = 365;
            for (int i = 0; i < days_n; i++)
            {
                date = date.AddDays(1);
                var response = await us
               .GetJsonData($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=usd&date={date.ToString("yyyyMMdd")}&json");
                
                try {
                    var strdollar = $"    {response[43]}{response[44]},{response[46]}{response[47]}     ";
                    float usd = float.Parse(strdollar);
                    days.Add(date);
                    UAHtoUSD.Add(usd);
                    Console.WriteLine(date);
                    Console.WriteLine(usd);

                }
                catch (System.FormatException)
                {
                    var strdollar = $"    {response[43]}{response[44]},{response[46]}     ";
                    float usd = float.Parse(strdollar);
                    days.Add(date);
                    UAHtoUSD.Add(usd);
                    Console.WriteLine(date);
                    Console.WriteLine(usd);
                }
               



            }

            Console.WriteLine(BubleSort(UAHtoUSD, days, days_n));



        }













    }
}
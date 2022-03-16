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

        public static List<string> FromTwotoOne(List<float> first, List<DateTime> second , int n)
        {
            var BaseUsd = new List<string>();
            for (int i = 0; i < n; i++)
            {
                BaseUsd.Add(second[i].ToString());
                BaseUsd.Add(first[i].ToString());
            }
            return BaseUsd;
        }
        public static float SearchMin(List<float> collection, List<DateTime> days, int n)
        {
            int min = 0;
            for (int i = 0; i<n; i++)
            {
                if (collection[i] < collection[min]) { min = i; }
            }
            Console.WriteLine();
            Console.WriteLine(days[min]);
            return collection[min];
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
            public async Task<string> Savetofile(string UAH, int n)
            {
                string path = @"C:\Users\usd.txt";
                FileInfo file = new(path);               
                await File.AppendAllTextAsync(path, UAH);
                return "oo";
            }

        }
       public static async Task<string> Readtxt(string path)
        { 
            string line = await File.ReadAllTextAsync(path);
            return line;
       }
    
        public static async Task Main()
        {
            DateTime date = new DateTime(2021, 1, 1);
            USD us = new();

            var days = new List<DateTime>();
            var UAHtoUSD = new List<float>();
            int days_n = 1;
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
            USD file = new();
            var UAHtoUSD_ = new List<string>();
            string  v = "\n";
            
            await file.Savetofile(String.Join(v, FromTwotoOne(UAHtoUSD,days, days_n)), days_n);
            
            string text = await Readtxt(@"C:\Users\usd.txt");
            Console.WriteLine(text);



        }













    }
}
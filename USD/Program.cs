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
        public static int SearchMin(List<float> collection,  int n)
        {
            int min = 0;
            for (int i = 0; i<n; i++)
            {
                if (collection[i] < collection[min]) { min = i; }
            }
            
            return min;
        }
        public static float MidinMonth(List<float> usd, List<DateTime> dates)

        {
            float sum_january = 0;
            float sum_february = 0;
            float sum_march = 0;
            float sum_april = 0;
            float sum_may = 0;
            float sum_june = 0;
            float sum_jule = 0;
            float sum_august = 0;
            float sum_september = 0;
            float sum_october = 0;
            float sum_november = 0;
            float sum_december = 0;
            for (int i = 0; i<365; i++)
            {
               
                if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.01.yyyy"))
                { 
                   sum_january += usd[i];
                }
                else if(dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.02.yyyy"))
                {
                    sum_february += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.03.yyyy"))
                {
                    sum_march += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.04.yyyy"))
                {
                    sum_april += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.05.yyyy"))
                {
                    sum_may += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.06.yyyy"))
                {
                    sum_june += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.07.yyyy"))
                {
                    sum_jule += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.08.yyyy"))
                {
                    sum_august += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.09.yyyy"))
                {
                    sum_september += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.10.yyyy"))
                {
                    sum_october += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.11.yyyy"))
                {
                    sum_november += usd[i];
                }
                else if (dates[i].ToString("dd.MM.yyyy") == dates[i].ToString("dd.12.yyyy"))
                {
                    sum_december += usd[i];
                }
            }
            Console.WriteLine("January:");
            Console.WriteLine(sum_january/31);
            Console.WriteLine("February:");
            Console.WriteLine(sum_february/28);
            Console.WriteLine("March:");
            Console.WriteLine(sum_march/31);
            Console.WriteLine("April:");
            Console.WriteLine(sum_april/30);
            Console.WriteLine("May:");
            Console.WriteLine(sum_may/31);
            Console.WriteLine("June:");
            Console.WriteLine(sum_june/30);
            Console.WriteLine("Jule:");
            Console.WriteLine(sum_jule/31);
            Console.WriteLine("August:");
            Console.WriteLine(sum_august/31);
            Console.WriteLine("September:");
            Console.WriteLine(sum_september/30);
            Console.WriteLine("October:");
            Console.WriteLine(sum_october/31);
            Console.WriteLine("November:");
            Console.WriteLine(sum_november/31);
            Console.WriteLine("December:");
            Console.WriteLine(sum_december/31);

            return 1;
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
        public static int SearchMax(List<float> collection, int n)
        {
            int max = 0;
            for(int i = 0; i < n; i++) { if (collection[i]>collection[max]) { max = i; } }
            
            return max;
        }
        public static float SearchMedian(List<float> collection, int n)
        {
            collection.Sort();
            Console.WriteLine("median");
            return collection[n/2];
        }
        public static async Task<string> Savetofile( int days_n)
        {
            DateTime date = new DateTime(2021, 1, 1);
            USD us = new();

            var days = new List<DateTime>();
            var UAHtoUSD = new List<float>();
            for (int i = 0; i < days_n; i++)
            {
                date = date.AddDays(1);
                var response = await us
               .GetJsonData($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?valcode=usd&date={date.ToString("yyyyMMdd")}&json");

                try
                {
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
            string path = @"C:\Users\usd.txt";
            FileInfo file = new(path);
            string UAH = String.Join("\n", FromTwotoOne(UAHtoUSD, days, days_n));
            string path_onlyUS = @"C:\Users\usd_only.txt";
            await File.AppendAllTextAsync(path, UAH);
            string US = String.Join("\n", UAHtoUSD);
            await File.AppendAllTextAsync(path_onlyUS, US);
            string path_date = @"C:\Users\date_only.txt";
            string date_str = String.Join("\n", days );
            await File.AppendAllTextAsync(path_date, date_str);
            return "1";
        }
        public static async Task<string> Readtxt(string path)
        {
          
            string line = await File.ReadAllTextAsync(path);
            return line;
       }
    
        public static async Task Main()
        {
     
            string text = await Readtxt(@"C:\Users\usd_only.txt");
            string datetxt = await Readtxt(@"C:\Users\date_only.txt");
            var ListofUS = text.Split("\n");
            var Listofdates = datetxt.Split("\n");  
            var USD = new List<float>();
            var dates = new List<DateTime>();
            for (int i = 0; i < 365; i++) { USD.Add(float.Parse(ListofUS[i])); dates.Add(DateTime.Parse(Listofdates[i]));  }
            Console.WriteLine("Min:");
            Console.WriteLine(Listofdates[SearchMin(USD,365)]);
            Console.WriteLine(USD[SearchMin(USD,365)]);
            Console.WriteLine("Max:");
            Console.WriteLine(Listofdates[SearchMax(USD,365)]);
            Console.WriteLine(USD[SearchMax(USD,365)]);
            Console.WriteLine(MidinMonth(USD, dates));
            
        }

    }
}
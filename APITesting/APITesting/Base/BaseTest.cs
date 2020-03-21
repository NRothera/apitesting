using APITesting.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APITesting.Base
{
    [Binding]
    public class BaseTest
    {
        public static Uri BaseUrl { get; set; }
        public static IRestResponse<Posts> Response { get; set; }
        public static IRestRequest Request { get; set; }
        public static RestClient RestClient { get; set; } = new RestClient();


        [BeforeScenario]
        public void TestSetup()
        {
            BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"].ToString());
            RestClient.BaseUrl = BaseUrl;
        }

        public static Dictionary<string, string> ToDictionary(Table table)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var row in table.Rows)
            {
                dictionary.Add(row[0], row[1]);
            }
            return dictionary;
        }


    }
}

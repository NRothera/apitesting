using APITesting.Base;
using APITesting.Model;
using APITesting.Utilities;
using NUnit.Framework;
using System.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace APITesting.Steps
{
    [Binding]
    public class PostProfileSteps : BaseTest
    {
       

        [Given(@"I perform POST operation for ""(.*)"" with body")]
        public void GivenIPerformPOSTOperationForWithBody(string url, Table table)
        {
            Request = new RestRequest(url, Method.POST);

            Dictionary<string, string> data = ToDictionary(table);
            string profile = data["postId"];

            Request.AddUrlSegment("postId", profile);

            Request.RequestFormat = DataFormat.Json;
            Request.AddBody(new { name = data["name"] });


            Response = RestClient.ExecuteAsyncsRequest<Posts>(Request).GetAwaiter().GetResult();
                
        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.That(Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is incorrect");
        }

    }
}
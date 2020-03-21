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
using System.Threading;

namespace APITesting.Steps
{
    [Binding]
    public class GetPostsSteps : BaseTest
    {

        [Given(@"I perfom GET operation for ""(.*)""")]
        public void GivenIPerfomGETOperationFor(string url)
        {
            Request = new RestRequest(url, Method.GET);
        }

        [Given(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int postId)
        {
            Request.AddUrlSegment("postid", postId.ToString());
            Response = RestClient.ExecuteAsync<Posts>(Request).GetAwaiter().GetResult();
            
        }

    }
}

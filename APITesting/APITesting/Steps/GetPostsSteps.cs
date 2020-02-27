using APITesting.Base;
using APITesting.Model;
using APITesting.Utilities;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APITesting.Steps
{
    [Binding]
    public class GetPostsSteps
    {
        private Settings _settings;
        public GetPostsSteps(Settings settings) => _settings = settings;

        [Given(@"I perfom GET operation for ""(.*)""")]
        public void GivenIPerfomGETOperationFor(string url)
        {
            _settings.Request = new RestRequest(url, Method.GET);
        }

        [Given(@"I perform operation for post ""(.*)""")]
        public void GivenIPerformOperationForPost(int postId)
        {
            _settings.Request.AddUrlSegment("postid", postId.ToString());
            _settings.Response = _settings.RestClient.ExecuteAsyncRequest<Posts>(_settings.Request).GetAwaiter().GetResult();
            
        }

        [Then(@"I should see the ""(.*)"" name as ""(.*)""")]
        public void ThenIShouldSeeTheNameAs(string key, string value)
        {
            Assert.That(_settings.Response.GetResponseObject(key), Is.EqualTo(value), $"The {key} is incorrect");
        }

    }
}

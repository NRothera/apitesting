using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace APITesting
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("post/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var content = client.Execute(request).Content;


        }
    }
}

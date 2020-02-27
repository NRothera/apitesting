using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APITesting.Model;
using APITesting.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using Assert = NUnit.Framework.Assert;

namespace APITesting
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts/{postid}", Method.GET);

            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            //Dictionary based response

            //var deserialize = new JsonDeserializer();
            //var output =  deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["author"];

            //Json based response

            var author = response.GetResponseObject("author");
            Assert.That(author, Is.EqualTo("typicode"), "Author is not correct");
        }

        [Test]
        public void PostWithAnonymousBody()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts/{postid}/profile", Method.POST);

            request.AddJsonBody(new { name = "Nigel" });

            request.AddUrlSegment("postid", 1);

            var response = client.Execute(request);

            var result = response.DeserializeResponse()["name"];

            Assert.That(result, Is.EqualTo("Nigel"), "That's incorrect you fool");
        }

        [Test]
        public void PostWithTypeClassBody()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts", Method.POST);

            request.AddJsonBody(new Posts() { id = "15", author = "Execute Automation", title = "this is a title" });

            // putting posts in like that deserialises the response for us automatially, by going to the 
            // posts class that we made and using the model (id, author, title get set bits)
            var response = client.Execute<Posts>(request);

            //var deserialize = new JsonDeserializer();
            //var output = deserialize.Deserialize<Dictionary<string, string>>(response);
            //var result = output["author"];

            Assert.That(response.Data.author, Is.EqualTo("Execute Automation"), "That's incorrect you fool");
        }


        [Test]
        public void PostWithAsync()
        {
            var client = new RestClient("http://localhost:3000/");

            var request = new RestRequest("posts", Method.POST);

            // using async

            request.AddJsonBody(new Posts() { id = "18", author = "Execute Automation", title = "this is a title" });

            //execute post async is an inbuilt method, but it does the same thing as ExecuteAsyncRequest that we created in libraries.cs

            var response = client.ExecutePostAsync<Posts>(request).GetAwaiter().GetResult();

            Assert.That(response.Data.author, Is.EqualTo("Execute Automation"), "That's incorrect you fool");
        }

        //The t is just a placeholder for the type we are passing, which is this case is Posts

    }
}

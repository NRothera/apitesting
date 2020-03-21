using APITesting.Model;
using APITesting.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
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

            request.AddJsonBody(new Posts() { id = "16", author = "Execute Automation", title = "this is a title" });

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

        [Test]
        public void AuthenticationMechanism()
        {
            var client = new RestClient("http://localhost:3000");
            var request = new RestRequest("auth/login", Method.POST);

            request.AddJsonBody(new {  email = "bruno@email.com", password = "bruno"});

            var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            var access_token = response.DeserializeResponse()["access_token"];

            client.Authenticator = new JwtAuthenticator(access_token);

            var getRequest = new RestRequest("posts/{postid}", Method.GET);
            getRequest.AddUrlSegment("postid", 1);

            var result = client.ExecuteAsync<Posts>(getRequest).GetAwaiter().GetResult();
            Assert.That(result.Data.author, Is.EqualTo("George BB"), "You are wrong fool");


        }

        [Test]
        public void AuthenticationMechanismWithJSONFile()
        {
            var client = new RestClient("http://localhost:3000/");
            var request = new RestRequest("auth/login", Method.POST);
            var file = @"TestData\Data.json";
            request.RequestFormat = DataFormat.Json;
            var jsonData = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file)).ToString());
            request.AddJsonBody(jsonData);
            var response = client.ExecutePostTaskAsync(request).GetAwaiter().GetResult();
            var access_token = response.DeserializeResponse()["access_token"];
            var jwtAuth = new JwtAuthenticator(access_token);
            client.Authenticator = jwtAuth;
            var getRequest = new RestRequest("posts/{postid}", Method.GET);
            getRequest.AddUrlSegment("postid", 5);
            //Perform Get operation
            var result = client.ExecuteAsync<Posts>(getRequest).GetAwaiter().GetResult();
            Assert.That(result.Data.author, Is.EqualTo("Karthik KK"), "The author is not correct");
        }

        //[Test]
        //public void AuthenticationMechanismWithJsonFile()
        //{
        //    var client = new RestClient("http://localhost:3000");
        //    var request = new RestRequest("auth/login", Method.POST);

        //    var dataFile = @"TestData\Data.json";
        //    request.RequestFormat = DataFormat.Json;
        //    var jsonData = JsonConvert.DeserializeObject<User>(File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dataFile)).ToString());
        //    request.AddJsonBody(jsonData);
        //    var response = client.ExecuteAsync(request).GetAwaiter().GetResult();
        //    var access_token = response.DeserializeResponse()["access_token"];

        //    client.Authenticator = new JwtAuthenticator(access_token);

        //    var getRequest = new RestRequest("posts/{postid}", Method.GET);
        //    getRequest.AddUrlSegment("postid", 1);

        //    var result = client.ExecuteAsync<Posts>(getRequest).GetAwaiter().GetResult();
        //    Assert.That(result.Data.author, Is.EqualTo("George BB"), "You are wrong fool");


        //}

        private class User
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}

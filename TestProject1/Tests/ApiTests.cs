using FluentAssertions;
using RestSharp;
using System.Net;
using System.Text.Json;
using NUnit.Framework;

namespace TestProject1.Tests
{
    internal class ApiTests
    {
        private readonly string baseUrl = "https://qacandidatetest.ensek.io/ENSEK";
        private readonly RestClient client;

        public ApiTests()
        {
            client = new RestClient(baseUrl);
        }

        [Test]
        public void GetEnergy()
        {
            RestRequest restRequest = new RestRequest("energy", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.Should().NotBeNull();
            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [TestCase(1,1)]
        [TestCase(2,1)]
        [TestCase(3,1)]
        [TestCase(4,1)]
        public void BuyEnergy(int id, int quantity)
        {
            RestRequest restRequest = new RestRequest($"buy/{id}/{quantity}", Method.Get);
            RestResponse restResponse = client.Execute(restRequest);
            restResponse.Should().NotBeNull();
            restResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // All tests return 405 Method Not Allowed. No explanation.
        }

        [OneTimeTearDown]
        public void DisposeClient()
        {
            client.Dispose();
        }
    }
}

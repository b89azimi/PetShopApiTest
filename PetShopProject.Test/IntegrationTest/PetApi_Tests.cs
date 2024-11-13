using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using PetShopProject.Test.Helper;

namespace PetShopProject.Test.IntegrationTest
{
    public class PetApi_Tests
    {
        private RestClient _client;
        [SetUp]
        public void Setup()
        {
            _client = new RestClient(Urls.BaseUrl);
        }

        // 1. Pet API Tests
        [Test]
        public async Task GetPetById_ShouldReturnPetDetails()
        {
            var petId = 1;
            var request = new RestRequest($"/pet/{petId}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["id"].Value<int>(), Is.EqualTo(petId));
        }

        [Test]
        public async Task AddPet_ShouldReturnCreatedPet()
        {
            var request = new RestRequest("/pet", Method.Post);
            request.AddJsonBody(new { id = 123456, name = "TestPet", status = "available" });
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["id"].Value<int>(), Is.EqualTo(123456));
        }

        [Test]
        public async Task UpdatePet_ShouldModifyPetDetails()
        {
            var request = new RestRequest("/pet", Method.Put);
            request.AddJsonBody(new { id = 123456, name = "UpdatedPet", status = "sold" });
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["name"].Value<string>(), Is.EqualTo("UpdatedPet"));
        }

        [Test]
        public async Task DeletePet_ShouldReturnSuccess()
        {
            var petId = 123456;
            var request = new RestRequest($"/pet/{petId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
        }

    }
}

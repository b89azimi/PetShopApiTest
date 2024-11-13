using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using PetShopProject.Test.Helper;

namespace PetShopProject.Test.IntegrationTest
{
    public class StoreApi_Tests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(Urls.BaseUrl);
        }
        [Test]
        public async Task PlaceOrder_ShouldReturnOrderDetails()
        {
            var request = new RestRequest("/store/order", Method.Post);
            request.AddJsonBody(new { id = 78910, petId = 123456, quantity = 1, status = "placed" });
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["id"].Value<int>(), Is.EqualTo(78910));
        }

        [Test]
        public async Task GetOrderById_ShouldReturnOrderDetails()
        {
            var orderId = 78910;
            var request = new RestRequest($"/store/order/{orderId}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["id"].Value<int>(), Is.EqualTo(orderId));
        }

        [Test]
        public async Task DeleteOrder_ShouldReturnSuccess()
        {
            var orderId = 78910;
            var request = new RestRequest($"/store/order/{orderId}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
        }

    }
}

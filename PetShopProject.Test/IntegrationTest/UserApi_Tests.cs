using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using PetShopProject.Test.Helper;

namespace PetShopProject.Test.IntegrationTest
{
    public class UserApi_Tests
    {
        private RestClient _client;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(Urls.BaseUrl);
        }

        [Test]
        public async Task CreateUser_ShouldReturnCreatedUser()
        {
            var request = new RestRequest("/user", Method.Post);
            request.AddJsonBody(new { id = 111213, username = "testuser", firstName = "Test", lastName = "User", email = "test@example.com", password = "password123", phone = "1234567890" });
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["username"].Value<string>(), Is.EqualTo("testuser"));
        }

        [Test]
        public async Task GetUserByUsername_ShouldReturnUserDetails()
        {
            var username = "testuser";
            var request = new RestRequest($"/user/{username}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
            var responseData = JObject.Parse(response.Content);
            Assert.That(responseData["username"].Value<string>(), Is.EqualTo(username));
        }

        [Test]
        public async Task UpdateUser_ShouldModifyUserDetails()
        {
            var username = "testuser";
            var request = new RestRequest($"/user/{username}", Method.Put);
            request.AddJsonBody(new { id = 111213, username = "testuser", firstName = "Updated", lastName = "User", email = "updated@example.com", password = "newpassword123", phone = "0987654321" });
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
        }

        [Test]
        public async Task DeleteUser_ShouldReturnSuccess()
        {
            var username = "testuser";
            var request = new RestRequest($"/user/{username}", Method.Delete);
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);
        }
    }
}

using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using PetShopProject.Test.Helper;

namespace PetShopProject.Test.EndToEndTest
{
    public class PetStoreApiEndToEnd_Tests
    {
        private readonly string baseUrl = Urls.BaseUrl;
        private RestClient _client;
        private long _petId;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(baseUrl);
        }

        [Test]
        public async Task EndToEndTest_ShouldCreateUpdateRetrieveAndDeletePet()
        {
            // Step 1: Create a new pet
            var petData = new
            {
                id = 0,
                name = "Test Pet",
                category = new { id = 1, name = "Dog" },
                photoUrls = new[] { "http://example.com/photo.jpg" },
                tags = new[] { new { id = 1, name = "cute" } },
                status = "available"
            };

            var createRequest = new RestRequest("/pet", Method.Post);
            createRequest.AddJsonBody(petData);
            var createResponse = await _client.ExecuteAsync(createRequest);
            Assert.That((int)createResponse.StatusCode, Is.EqualTo(200));

            var createResponseData = JObject.Parse(createResponse.Content);
            _petId = createResponseData["id"].ToObject<long>(); // Store the pet ID for future use

            Assert.That(createResponseData["name"].ToString(), Is.EqualTo("Test Pet"));
            Assert.That(createResponseData["status"].ToString(), Is.EqualTo("available"));

            // Step 2: Retrieve pet details by ID
            var getRequest = new RestRequest($"/pet/{_petId}", Method.Get);
            var getResponse = await _client.ExecuteAsync(getRequest);
            Assert.That((int)getResponse.StatusCode, Is.EqualTo(200));

            var getResponseData = JObject.Parse(getResponse.Content);
            Assert.That(getResponseData["id"].ToString(), Is.EqualTo(_petId.ToString()));
            Assert.That(getResponseData["name"].ToString(), Is.EqualTo("Test Pet"));
            Assert.That(getResponseData["status"].ToString(), Is.EqualTo("available"));

            // Step 3: Update pet details
            var updatePetData = new
            {
                id = _petId,
                name = "Updated Test Pet",
                category = new { id = 1, name = "Dog" },
                photoUrls = new[] { "http://example.com/photo2.jpg" },
                tags = new[] { new { id = 1, name = "playful" } },
                status = "pending"
            };

            var updateRequest = new RestRequest("/pet", Method.Put);
            updateRequest.AddJsonBody(updatePetData);
            var updateResponse = await _client.ExecuteAsync(updateRequest);
            Assert.That((int)updateResponse.StatusCode, Is.EqualTo(200));

            var updateResponseData = JObject.Parse(updateResponse.Content);
            Assert.That(updateResponseData["name"].ToString(), Is.EqualTo("Updated Test Pet"));
            Assert.That(updateResponseData["status"].ToString(), Is.EqualTo("pending"));

            // Step 4: Delete the pet
            var deleteRequest = new RestRequest($"/pet/{_petId}", Method.Delete);
            var deleteResponse = await _client.ExecuteAsync(deleteRequest);
            Assert.That((int)deleteResponse.StatusCode, Is.EqualTo(200));

           

            // Verify that the pet is deleted
            var verifyGetRequest = new RestRequest($"/pet/{_petId}", Method.Get);
            var verifyGetResponse = await _client.ExecuteAsync(verifyGetRequest);
            Assert.That((int)verifyGetResponse.StatusCode, Is.EqualTo(404)); // Pet should not be found
        }
    }
}


using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PetShopProject.Test.Models;
using PetShopProject.Test.Helper;

namespace PetShopProject.Test.ORMTest
{
    public class PetApi_OrmTest
    {
        private RestClient _client;
        private PetContext _dbContext;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(Urls.BaseUrl);
            // Database settings in memory
            var options = new DbContextOptionsBuilder<PetContext>()
                .UseInMemoryDatabase(databaseName: "PetStoreTestDb")
                .Options;

            _dbContext = new PetContext(options);
        }
        [Test]
        public async Task AddPet_ShouldStoreAndRetrieveComplexPetFromDb()
        {
            // Arrange
            var petId = 1;
            var request = new RestRequest($"/pet/{petId}", Method.Get);

            // Send a GET request to fetch pet data from the API and verify success
            var response = await _client.ExecuteAsync(request);
            Assert.That(response.IsSuccessful, Is.True);

            // Parse the response content into a JSON object
            var responseData = JObject.Parse(response.Content);

            // Map API response data to the Category and Pet objects
            var category = new Category
            {
                Id = responseData["category"]["id"].Value<long>(),
                Name = responseData["category"]["name"].Value<string>()
            };

            var pet = new Pet
            {
                Id = responseData["id"].Value<long>(),
                Name = responseData["name"].Value<string>(),
                Status = responseData["status"].Value<string>(),
                Category = category,

                // Map photo URLs to a list of PhotoUrl objects
                PhotoUrls = responseData["photoUrls"].Select(url => new PhotoUrl { Url = url.Value<string>() }).ToList(),

                // Map tags to a list of PetTag objects, each containing a Tag
                PetTags = responseData["tags"].Select(tag => new PetTag
                {
                    Tag = new Tag
                    {
                        Id = tag["id"].Value<long>(),
                        Name = tag["name"].Value<string>()
                    }
                }).ToList()
            };

            // Act
            // Save the pet with related data to the in-memory database
            _dbContext.Pets.Add(pet);
            await _dbContext.SaveChangesAsync();

            // Retrieve the saved pet along with related data (Category, PhotoUrls, Tags) from the database
            var storedPet = _dbContext.Pets
                .Include(p => p.Category)
                .Include(p => p.PhotoUrls)
                .Include(p => p.PetTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefault(p => p.Id == petId);

            // Assert
            // Verify that the stored pet is not null and data matches the original pet details
            Assert.That(storedPet, Is.Not.Null);
            Assert.That(storedPet.Name, Is.EqualTo(pet.Name));
            Assert.That(storedPet.Status, Is.EqualTo(pet.Status));
            Assert.That(storedPet.Category.Name, Is.EqualTo(category.Name));

            // Ensure that the photo URLs and tags count matches what was saved
            Assert.That(storedPet.PhotoUrls.Count, Is.EqualTo(pet.PhotoUrls.Count));
            Assert.That(storedPet.PetTags.Count, Is.EqualTo(pet.PetTags.Count));
        }



        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}

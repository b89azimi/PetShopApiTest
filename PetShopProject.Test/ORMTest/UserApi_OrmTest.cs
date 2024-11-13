using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetShopProject.Test.Models;
 

namespace PetShopProject.Test.ORMTest
{
    public class UserApi_OrmTest
    {

        private UsersContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UsersContext>()
                .UseInMemoryDatabase(databaseName: "PetStoreTestDb")
                .Options;

            _dbContext = new UsersContext(options);
        }

        [Test]
        public async Task AddUser_ShouldStoreAndRetrieveUser()
        {
            // Arrange
            var user = new User
            {
                Username = "john_doe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Password = "Password123",
                Phone = "123-456-7890",
                UserStatus = 1 // Active user status
            };

            // Act
            // Save the user to the in-memory database
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            // Retrieve the user from the database
            var storedUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == "john_doe");

            // Assert
            // Verify that the user was stored correctly
            Assert.That(storedUser, Is.Not.Null);
            Assert.That(storedUser.Username, Is.EqualTo(user.Username));
            Assert.That(storedUser.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(storedUser.LastName, Is.EqualTo(user.LastName));
            Assert.That(storedUser.Email, Is.EqualTo(user.Email));
            Assert.That(storedUser.Phone, Is.EqualTo(user.Phone));
            Assert.That(storedUser.UserStatus, Is.EqualTo(user.UserStatus));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}

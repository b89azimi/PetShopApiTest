using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PetShopProject.Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopProject.Test.ORMTest
{
    public class OrderApi_OrmTest
    {
        private OrderContext _dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<OrderContext>()
                .UseInMemoryDatabase(databaseName: "PetStoreTestDb")
                .Options;

            _dbContext = new OrderContext(options);
        }

        [Test]
        public async Task AddOrder_ShouldStoreAndRetrieveOrder()
        {
            // Arrange
            var order = new Order
            {
                PetId = 1,
                Quantity = 2,
                ShipDate = "2024-11-15T10:00:00Z", // ISO format
                Status = "placed",
                Complete = false
            };

            // Act
            // Save the order to the in-memory database
            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            // Retrieve the order from the database
            var storedOrder = await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.PetId == 1 && o.Status == "placed");

            // Assert
            // Verify that the order was stored correctly
            Assert.That(storedOrder, Is.Not.Null);
            Assert.That(storedOrder.PetId, Is.EqualTo(order.PetId));
            Assert.That(storedOrder.Quantity, Is.EqualTo(order.Quantity));
            Assert.That(storedOrder.ShipDate, Is.EqualTo(order.ShipDate));
            Assert.That(storedOrder.Status, Is.EqualTo(order.Status));
            Assert.That(storedOrder.Complete, Is.EqualTo(order.Complete));
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}

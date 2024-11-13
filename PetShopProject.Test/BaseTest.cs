using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace PetShopProject.Test
{
    public class BaseTest
    {
        public static WebApplicationFactory<Program> _factory;
        private ServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // تنظیمات ویژه تست‌ها را در اینجا قرار دهید
                    // حذف سرویس‌هایی که نیاز ندارید
                });
            });
        }

        protected T GetService<T>() => _serviceProvider.GetRequiredService<T>();

        [OneTimeTearDown]
        public void Teardown() => _factory.Dispose();
    }

}

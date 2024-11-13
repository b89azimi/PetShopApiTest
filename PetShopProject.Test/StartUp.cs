using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PetShopProject.Test
{
    public class StartUp
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // پیکربندی سرویس‌ها مانند DbContext و غیره
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            // اضافه کردن سرویس‌ها
            services.AddSingleton(configuration); // مثال برای افزودن پیکربندی

            // سایر سرویس‌ها
        }
    }
}

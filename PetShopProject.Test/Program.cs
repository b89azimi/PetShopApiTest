using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PetShopProject.Test;

namespace PetShopProject
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<StartUp>(); // مطمئن شوید که از کلاس صحیح استفاده می‌کنید
                });
    }
}

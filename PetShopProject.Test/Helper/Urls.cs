using System;

namespace PetShopProject.Test.Helper
{
    internal static class Urls
    {
        public readonly static string BaseUrl = "https://petstore.swagger.io/v2";
        public readonly static string BaseUrlUI = "https://localhost:7218";

        // Pet API Endpoints
        public readonly static string GetPetById = "/pet/{petId}";
        public readonly static string FindPetsByStatus = "/pet/findByStatus";
        public readonly static string FindPetsByTags = "/pet/findByTags";
        public readonly static string AddPet = "/pet";
        public readonly static string UpdatePet = "/pet";
        public readonly static string DeletePet = "/pet/{petId}";

        // Store API Endpoints
        public readonly static string GetInventory = "/store/inventory";
        public readonly static string PlaceOrder = "/store/order";
        public readonly static string GetOrderById = "/store/order/{orderId}";
        public readonly static string DeleteOrder = "/store/order/{orderId}";

        // User API Endpoints
        public readonly static string CreateUser = "/user";
        public readonly static string CreateUsersWithArray = "/user/createWithArray";
        public readonly static string CreateUsersWithList = "/user/createWithList";
        public readonly static string GetUserByUsername = "/user/{username}";
        public readonly static string UpdateUser = "/user/{username}";
        public readonly static string DeleteUser = "/user/{username}";
        public readonly static string LoginUser = "/user/login";
        public readonly static string LogoutUser = "/user/logout";
    }
}

namespace GradTest.API.Routes;

public static class ApiEndpoints
{
        public static class Orders
        {
            public const string Create = "/orders";
            public const string GetById = "/orders/{id}";
            public const string GetAll = "/orders";
            public const string GetByUserId = "/orders";
        }

        public static class Products 
        {
            public const string Create = "/products";
            public const string Delete = "/products/{id}";
            public const string Update = "/products/{id}";
            public const string List = "/products";
            public const string GetById = "/products/{id}";
        }
}
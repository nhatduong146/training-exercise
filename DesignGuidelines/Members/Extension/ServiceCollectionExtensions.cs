using Microsoft.Extensions.DependencyInjection;

namespace DesignGuidelines.Members.Extension
{
    public class ProductService 
    {
        public void AddProduct()
        {
            // Add product logic
        }
    }

    public class UserService
    {
        public void AddUser()
        {
            // Add user logic
        }
    }

    public static class ServiceCollectionExtensions
    {
        // DO use extension when dealing with classes that aren’t your own, or a class from a 3rd party library. 
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddTransient<ProductService>();
            services.AddTransient<UserService>();

            return services;
        }
    }
}

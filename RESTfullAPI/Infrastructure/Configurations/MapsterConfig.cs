using Mapster;
using RESTfullAPI.Domain.Entities;
using RESTfullAPI.ViewModels.Product.Requests;

namespace RESTfullAPI.Infrastructure.Configurations;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<PatchProductRequest, Product>
            .NewConfig()
            .IgnoreNullValues(true);
    }
}

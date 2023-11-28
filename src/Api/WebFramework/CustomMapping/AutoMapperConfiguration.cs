using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Models.CustomMapping;
using System;
using System.Linq;
using System.Reflection;
using WebFramework.CustomMapping;


public static class AutoMapperConfiguration
{
    public static void InitializeAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
    {

        services.AddAutoMapper(config =>
        {
            config.AddCustomMappingProfile();
        }, assemblies);
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config)
    {
        var repositoriesAssembly = typeof(IHaveCustomMapping).Assembly;
        config.AddCustomMappingProfile(repositoriesAssembly);
    }

    public static void AddCustomMappingProfile(this IMapperConfigurationExpression config, params Assembly[] assemblies)
    {
        var allTypes = assemblies.SelectMany(a => a.ExportedTypes);

        var list = allTypes.Where(type => type.IsClass && !type.IsAbstract &&
            type.GetInterfaces().Contains(typeof(IHaveCustomMapping)))
            .Select(type => (IHaveCustomMapping)Activator.CreateInstance(type));

        var profile = new CustomMappingProfile(list);

        config.AddProfile(profile);
    }
}

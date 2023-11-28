using Common;
using Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WebFramework.Swagger;


public static class SwaggerConfigurationExtensions
{
    [Obsolete]
    public static void AddSwagger(this IServiceCollection services, SiteSettings siteSetting, string ProjectName)
    {
        Assert.NotNull(services, nameof(services));

        #region AddSwaggerExamples
        //We call this method for by reflection with the Startup type of entry assembly (CongestionTaxCalculator assembly)
        var mainAssembly = Assembly.GetEntryAssembly(); // => CongestionTaxCalculator project assembly
        var mainType = mainAssembly.GetExportedTypes()[0];

        const string methodName = nameof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions.AddSwaggerExamplesFromAssemblyOf);
        //MethodInfo method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetMethod(methodName);
        var method = typeof(Swashbuckle.AspNetCore.Filters.ServiceCollectionExtensions).GetRuntimeMethods().FirstOrDefault(x => x.Name == methodName && x.IsGenericMethod);
        var generic = method.MakeGenericMethod(mainType);
        generic.Invoke(null, new[] { services });
        #endregion

        //Add services and configuration to use swagger
        services.AddSwaggerGen(options =>
        {
            var xmlDocPath = Path.Combine(AppContext.BaseDirectory, ProjectName);// "CongestionTaxCalculator.xml");
                                                                                 //show controller XML comments like summary
            options.IncludeXmlComments(xmlDocPath, true);

            options.EnableAnnotations();



            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "CongestionTaxCalculator Api",
                Description = "CongestionTaxCalculator Api",
                TermsOfService = new Uri("http://test.com"),
                Contact = new OpenApiContact
                {
                    Name = "Hossein Shahbazi",
                    Email = "Hoseinshahbazi29@gmail.com",
                    Url = new Uri("http://test.com"),
                }
            });

            #region Filters
            options.ExampleFilters();

            options.OperationFilter<ApplySummariesOperationFilter>();

            #region Add UnAuthorized to Response
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
            #endregion

            #region Add Jwt Authentication

            //OAuth2Scheme
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("http://5.63.13.16/api/v1/users/Token")
                    }
                }
            });
            #endregion

            #region Versioning
            // Remove version parameter from all Operations
            options.OperationFilter<RemoveVersionParameters>();

            //set version "api/v{version}/[controller]" from current swagger doc version
            options.DocumentFilter<SetVersionInPaths>();

            //Separate and categorize end-points by doc version
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

                var versions = methodInfo.DeclaringType
                    .GetCustomAttributes<ApiVersionAttribute>(true)
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v.ToString()}" == docName);
            });
            #endregion

            #endregion
        });
    }

    public static void UseSwaggerAndUi(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        app.UseSwagger(options =>
        {
        });

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CongestionTaxCalculator V1 Docs");
            options.RoutePrefix = "swagger";

            #region Customizing
            //// Display
            //options.DefaultModelExpandDepth(2);
            //options.DefaultModelRendering(ModelRendering.Model);
            //options.DefaultModelsExpandDepth(-1);
            //options.DisplayOperationId();
            //options.DisplayRequestDuration();
            options.DocExpansion(DocExpansion.None);
            //options.EnableDeepLinking();
            //options.EnableFilter();
            //options.MaxDisplayedTags(5);
            //options.ShowExtensions();

            //// Network
            //options.EnableValidator();
            //options.SupportedSubmitMethods(SubmitMethod.Get);

            //// Other
            //options.DocumentTitle = "CustomUIConfig";
            options.InjectStylesheet("/style/custom-stylesheet.css");
            //options.InjectJavascript("/ext/custom-javascript.js");
            //options.RoutePrefix = "api-docs";
            #endregion
        });

    }
}

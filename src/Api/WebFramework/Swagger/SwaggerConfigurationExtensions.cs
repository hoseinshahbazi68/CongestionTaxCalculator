﻿using Common;
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
        //Add services to use Example Filters in swagger
        //If you want to use the Request and Response example filters (and have called options.ExampleFilters() above), then you MUST also call
        //This method to register all ExamplesProvider classes form the assembly
        //services.AddSwaggerExamplesFromAssemblyOf<PersonRequestExample>();

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

            #region DescribeAllEnumsAsStrings
            //This method was Deprecated. 
            ///hhhhhhhhhhhhhhhhhhhhhhhhh
            //  options.DescribeAllEnumsAsStrings();

            //You can specify an enum to convert to/from string, using :
            //[JsonConverter(typeof(StringEnumConverter))]
            //public virtual MyEnums MyEnum { get; set; }

            //Or can apply the StringEnumConverter to all enums globally, using :
            //SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            //OR
            //JsonConvert.DefaultSettings = () =>
            //{
            //    var settings = new JsonSerializerSettings();
            //    settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
            //    return settings;
            //};
            #endregion

            //options.DescribeAllParametersInCamelCase();
            //options.DescribeStringEnumsInCamelCase()
            //options.UseReferencedDefinitionsForEnums()
            //options.IgnoreObsoleteActions();
            //options.IgnoreObsoleteProperties();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "CongestionTaxCalculator Api",
                Description = "CongestionTaxCalculator Api",
                TermsOfService = new Uri("http://test.com"),
                Contact = new OpenApiContact
                {
                    Name = "Hossein SHahbazi",
                    Email = "Hoseinshahbazi29@gmail.com",
                    Url = new Uri("http://test.com"),
                }
            });

            #region Filters
            //Enable to use [SwaggerRequestExample] & [SwaggerResponseExample]
            options.ExampleFilters();

            //It doesn't work anymore in recent versions because of replacing Swashbuckle.AspNetCore.Examples with Swashbuckle.AspNetCore.Filters
            //Adds an Upload button to endpoints which have [AddSwaggerFileUploadButton]
            //options.OperationFilter<AddFileParamTypesOperationFilter>();

            //Set summary of action if not already set
            options.OperationFilter<ApplySummariesOperationFilter>();

            #region Add UnAuthorized to Response
            //Add 401 response and security requirements (Lock icon) to actions that need authorization
            options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "OAuth2");
            #endregion

            #region Add Jwt Authentication
            //Add Lockout icon on top of swagger ui page to authenticate
            #region Old way
            //options.AddSecurityDefinition("Bearer", new ApiKeyScheme
            //{
            //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //    Name = "Authorization",
            //    In = "header"
            //});
            //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
            //{
            //    {"Bearer", new string[] { }}
            //});
            #endregion

            //options.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //        new OpenApiSecurityScheme
            //        {
            //            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" }
            //        },
            //        Array.Empty<string>() //new[] { "readAccess", "writeAccess" }
            //    }
            //});

            //OAuth2Scheme
            options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                //Scheme = "Bearer",
                //In = ParameterLocation.Header,
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

            //If use FluentValidation then must be use this package to show validation in swagger (MicroElements.Swashbuckle.FluentValidation)
            //options.AddFluentValidationRules();
            #endregion
        });
    }

    public static void UseSwaggerAndUi(this IApplicationBuilder app)
    {
        Assert.NotNull(app, nameof(app));

        app.UseSwagger(options =>
        {
            //options.RouteTemplate = "api-docs/{documentName}/swagger.json";
        });

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
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

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            // options.SpecUrl("/swagger/v1/swagger.json");
            options.SpecUrl("/swagger/v2/swagger.json");

            #region Customizing
            //By default, the ReDoc UI will be exposed at "/api-docs"
            //options.RoutePrefix = "docs";
            //options.DocumentTitle = "My API Docs";

            options.EnableUntrustedSpec();
            options.ScrollYOffset(10);
            options.HideHostname();
            options.HideDownloadButton();
            options.ExpandResponses("200,201");
            options.RequiredPropsFirst();
            options.NoAutoAuth();
            options.PathInMiddlePanel();
            options.HideLoading();
            options.NativeScrollbars();
            options.DisableSearch();
            options.OnlyRequiredInSamples();
            options.SortPropsAlphabetically();
            #endregion
        });
    }
}

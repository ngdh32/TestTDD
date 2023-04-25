
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TestTddCore.Entities;
using TestTddCore.Interfaces;
using TestTddInfra.Entities;
using TestTddInfra.Entities.EF;

namespace TestTddInfra
{
    public static class InitInfra
    {
        public static void InitializeInfra(this IServiceCollection service, IConfiguration configuration)
        {
            var databaseType = Enum.Parse(typeof(DatabaseType),  configuration["DatabaseType"]);
            switch (databaseType)
            {
                case DatabaseType.EF:
                    InitEF(service, configuration);
                    break;
                case DatabaseType.DynamoDb:
                    InitDynamoDb(service, configuration);
                    break;
                default:
                    throw new Exception("Not valid database type");
            }
        }

        private static void InitEF(IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("mysql");
            Console.WriteLine(connectionString);
            service.AddDbContext<BookDbContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("TestTddApi"));
            });

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<EfBook, Book>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate))
                    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                    .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.Author.Id));
            });

            service.AddTransient<IBookRepository, EfBookRepository>();
            service.AddSingleton<IMapper>(new Mapper(mapperConfiguration));
        }

        private static void InitDynamoDb(IServiceCollection service, IConfiguration configuration)
        {
            var awsOptions = new AWSOptions()
            {
                Region = Amazon.RegionEndpoint.USEast1
            };
            var accessKey = Environment.GetEnvironmentVariable("DYNAMO_ACCESS_KEY_ID");
            var accessSecretKey = Environment.GetEnvironmentVariable("DYNAMO_SECRET_ACCESS_KEY");
            awsOptions.Credentials = new BasicAWSCredentials(accessKey, accessSecretKey);

            service.AddDefaultAWSOptions(awsOptions);
            service.AddAWSService<IAmazonDynamoDB>();
            service.AddTransient<IBookRepository, DynamoDbBookRepository>();

        }

    }
}

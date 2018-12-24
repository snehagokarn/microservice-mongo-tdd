using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Repository
{
    public enum DataProviders
    {
        MongoDb,
        ElasticSearch,
        FireBase,
        DynamoDb,
        SqlServer
    }
    public abstract class BaseRepository
    {
        private readonly IConfiguration configuration;

        public BaseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GetConnectionString(DataProviders providers = DataProviders.MongoDb)
        {
            return configuration["ConnectionString"];
        }
    }
}

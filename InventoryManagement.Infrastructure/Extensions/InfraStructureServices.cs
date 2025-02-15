﻿using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using InventoryManagement.Application.RedisSearch;
using InventoryManagement.Infrastructure.Messaging;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagment.DomainModels.Interfaces;
using InventoryManagment.DomainModels.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Redis.OM;
using Redis.OM.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Extensions
{
    public static class InfraStructureServices
    {
        public static void AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            var azureServiceBusConnection = configuration.GetConnectionString("AzureServiceBusConnection");
            var MongoConnectionString = configuration.GetConnectionString("MongoDbConnection");
            var RedisConnectionString = configuration.GetConnectionString("RedisConnection");
            services.AddSingleton<ServiceBusClient>(c =>
            {
                return new ServiceBusClient(azureServiceBusConnection);
            });
            services.AddSingleton<ServiceBusAdministrationClient>(c =>
            {
                return new ServiceBusAdministrationClient(azureServiceBusConnection);
            });
            services.AddSingleton<IProductRepository, MongoProductRepository>(c =>
            {
                var client = new MongoClient(MongoConnectionString);
                var database = client.GetDatabase("mydatabase");
                return new MongoProductRepository(database);
            });
            services.AddSingleton<IProductSearchRepository, ProductSearchRepository>(c =>
            {
                var provider = new RedisConnectionProvider(RedisConnectionString);
                provider.Connection.CreateIndex(typeof(ProductSearchModel));
                return new ProductSearchRepository(provider);
            });
            services.AddSingleton<IMessageBus, MessageBus>();
        }
    }
}

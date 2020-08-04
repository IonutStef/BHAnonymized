using Anonymized.Assessment.Api.Infrastructure;
using Anonymized.Assessment.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Anonymized.Assessment.Api.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IApplicationBuilder"/>.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Initialize the database with default data.
        /// </summary>
        /// <param name="app">the <see cref="IApplicationBuilder"/>.</param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/> to get the required services.</param>
        /// <returns></returns>
        public static IApplicationBuilder InitializeTestData(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var customerService = serviceProvider.GetRequiredService<ICustomerService>();
            var dataSeed = serviceProvider.GetRequiredService<ICustomersDataSeed>();

            if(dataSeed.Data != null && dataSeed.Data.Count > 0)
            {
                customerService.InsertCustomers(dataSeed.Data);
            }

            return app;
        }
    }
}
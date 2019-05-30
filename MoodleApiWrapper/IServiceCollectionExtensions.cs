using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Web;

namespace MoodleApiWrapper
{
    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddMoodleService(this IServiceCollection services, Func<Options.Moodle, Options.Moodle> optionsBuilder)
        {
            var options = new Options.Moodle();
            options = optionsBuilder.Invoke(options);

            services.AddMoodleService(options);

            return services;
        }

        public static IServiceCollection AddMoodleService(this IServiceCollection services, Options.Moodle options)
        {
            if (string.IsNullOrEmpty(options.Host))
                throw new ArgumentNullException(nameof(options.Host));

            if (string.IsNullOrEmpty(options.ApiToken))
                throw new ArgumentNullException(nameof(options.ApiToken));

            services.AddHttpClient<IMoodleApiClient, MoodleApiClient>(client =>
            {
                client.BaseAddress = new Uri(options.Host);
            });
            return services;
        }

        
    }
}

using System;
using System.Net.Http;
using Xunit;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using MoodleApiWrapper.Exceptions;

namespace MoodleApiWrapper.Tests
{
    public class MoodleApiClientTest
    {
        

        [Fact]
        public void GetApiTokenTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);

                var target = new MoodleApiClient(client, null);
                var result = target.GetApiToken(Constants.USER, Constants.PASSWORD, Constants.SERVICE).Result;
                Assert.Equal(Constants.API_TOKEN, result.Data.token);
            }
        }

        [Fact]
        public void GetSiteInfoTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);
                var options = Microsoft.Extensions.Options.Options.Create(new MoodleApiWrapper.Options.Moodle
                {
                    ApiToken = Constants.API_TOKEN
                });
                var target = new MoodleApiClient(client, options);
                var result = target.GetSiteInfo().Result;
                var functions = result.Data.functions.Select(f => f.name).ToList();
                Assert.Equal("Administrador", result.Data.firstname);
            }
        }

        [Fact]
        public void ExecuteMethod_InvalidParameterException()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);
                var options = Microsoft.Extensions.Options.Options.Create(new MoodleApiWrapper.Options.Moodle
                {
                    ApiToken = Constants.API_TOKEN
                });
                var target = new MoodleApiClient(client, options);


                //var result = target.ExecuteMethod<Content>("core_course_get_contents", new { courseid = 2 }).Result;
                Assert.ThrowsAsync<InvalidParameterException>(async () => {
                    var result = await target.ExecuteMethod<Content>("core_course_get_categories", new { courseid = 2 });
                });
                
            }
        }

        [Fact]
        public void ExecuteMethod_core_course_get_contents()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);
                var options = Microsoft.Extensions.Options.Options.Create(new MoodleApiWrapper.Options.Moodle
                {
                    ApiToken = Constants.API_TOKEN
                });
                var target = new MoodleApiClient(client, options);

                
                    //var result = target.ExecuteMethod<Content>("core_course_get_contents", new { courseid = 2 }).Result;
                var result = target.ExecuteMethod<Content>("core_course_get_categories", new { courseid = 2 }).Result;
            }
        }
    }
}

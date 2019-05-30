using System;
using System.Net.Http;
using Xunit;
using System.Web;
using System.Collections.Generic;

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

                Assert.Equal("Administrador", result.Data.firstname);
            }
        }

        public class MyClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void GetCoursesTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);
                var options = Microsoft.Extensions.Options.Options.Create(new MoodleApiWrapper.Options.Moodle
                {
                    ApiToken = Constants.API_TOKEN
                });
                var target = new MoodleApiClient(client, options);

                var hola = new MyClass()
                {
                    Id = 1,
                    Name = "hola"
                };

                var list = new List<MyClass>();
                list.Add(hola);
                var hola2 = hola.GetQueryString();
                var hola3 = list.GetQueryString();

                var result = target.ExecuteMethod<Course>("core_group_get_course_groups");
                //Assert.NotNull(result.DataArray);
                
            }
        }

        [Fact]
        public void GetSignupSettingsTest()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.HOST);
                var options = Microsoft.Extensions.Options.Options.Create(new MoodleApiWrapper.Options.Moodle
                {
                    ApiToken = Constants.API_TOKEN
                });
                var target = new MoodleApiClient(client, options);
                var result = target.GetSignupSettings().Result;

                Assert.Equal("Administrador", result.Data.firstname);
            }
        }
    }
}

using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SocialNetworkApi.DTO.POST.Authentication;
using SocialNetworkApi.Model;


namespace SocialNetworkApi.Test
{
    public class IntegrationTest
    {
        [Fact]
        public async void Authenticate_Admin_Ok()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();

            var request = new AuthenticationRequest
            {
                Email = "admin@mail.com",
                Password = "admin"
            };

            var response = await client.PostAsJsonAsync("ApplicationUser/Authenticate", request);
            var body = response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            var topken = body.Result!.Token;
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async void Authenticate_Admin_NotOk()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();

            var request = new AuthenticationRequest
            {
                Email = "admin@mail.com",
                Password = "123456"
            };

            var response = await client.PostAsJsonAsync("ApplicationUser/Authenticate", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }

        [Fact]
        public async void Authenticate_Admin_GetAllOk()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();

            var request = new AuthenticationRequest
            {
                Email = "admin@mail.com",
                Password = "admin"
            };

            var response = await client.PostAsJsonAsync("ApplicationUser/Authenticate", request);
            
            var body = response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            var token = body.Result!.Token;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var users = await client.GetFromJsonAsync<List<DTO.GET.GetUsers.ApplicationUser>>("ApplicationUser/GetAll");

            Assert.NotEmpty(users!);

        }

        [Fact]
        public async void Authenticate_Admin_DeleteUserThatNotExist_NotOk()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();

            var request = new AuthenticationRequest
            {
                Email = "admin@mail.com",
                Password = "admin"
            };

            var response = await client.PostAsJsonAsync("ApplicationUser/Authenticate", request);

            var body = response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            var token = body.Result!.Token;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var users = await client.GetFromJsonAsync<List<DTO.GET.GetUsers.ApplicationUser>>("ApplicationUser/GetAll");

            Assert.NotEmpty(users!);

        }

        [Fact]
        public async void Register_DuplicatedUser_NotOk()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder => { });

            var client = application.CreateClient();

            var request = new DTO.POST.Registration.ApplicationUser
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "admin@mail.com",
                Password = "aPassword",
                Birthday = null,
                Gender = null
            };

            var response = await client.PostAsJsonAsync("ApplicationUser/Register", request);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        }
    }
}
using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DefaultAPI.UnitTesting
{
    public class UserServiceFake
    {
        [Fact]
        public async Task Test_GetAll()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/users/v1/GetAll");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetAllPaginate()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/users/v1/GetAllPaginate"
                        , new StringContent(
                        JsonConvert.SerializeObject(new UserFilter()
                        {
                            pageIndex = 1,
                            pageSize = 10
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetById()
        {
            const long id = 1;

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/users/v1/GetById/{id}");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetByLogin()
        {
            const string login = "administrator";

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/users/v1/GetByLogin/{login}");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_GetUsers()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync($"/api/users/v1/GetUsers");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Add()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/users/v1/Add"
                        , new StringContent(
                        JsonConvert.SerializeObject(new UserSendDto()
                        {
                            Login = "NovoLogin",
                            Password = "pass",
                            IsActive = true,
                            IsAuthenticated = false,
                            IdProfile = 1
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Update()
        {
            const long id = 1;

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PutAsync($"/api/users/v1/Update/{id}"
                        , new StringContent(
                        JsonConvert.SerializeObject(new UserSendDto()
                        {
                            Login = "NovoLogin",
                            Password = "pass",
                            IsActive = true,
                            IsAuthenticated = true,
                            IdProfile = 1
                        }),
                    Encoding.UTF8,
                    "application/json"));

                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact]
        public async Task Test_Delete()
        {
            const long id = 1;
            const bool isDeletePhysical = false;

            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync($"/api/users/v1/Delete/{id}/{isDeletePhysical}");
                response.EnsureSuccessStatusCode();
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}

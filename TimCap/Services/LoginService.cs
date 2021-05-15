using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TimCap.Model;
using System.Text.Json;

namespace TimCap.Services
{
    public class LoginService
    {
        public HttpClient Client { get; }

        public LoginService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://lucky2021.test.muxixyz.com/");
            client.DefaultRequestHeaders.Add("User-Agent", "WUT-TokenTeam");
            Client = client;
        }

        public async Task<ApiRes> LoginThrougthCcnu(User user)
        {
            
            var response = await Client.SendAsync(
                new HttpRequestMessage(
                    HttpMethod.Post, "api/signin"
                )
                {
                    Content = JsonContent.Create(user)
                });
            response.EnsureSuccessStatusCode();
            var IsSuccess = JsonDocument.Parse(response.Content.ReadAsStringAsync().Result)
                .RootElement.GetProperty("msg").GetString();
            if (IsSuccess != "登陆成功")
            {
                return new ApiRes(ApiCode.Error,"登录失败",null);
            }
            else
            {
                return new ApiRes(ApiCode.Success,"登录成功", JsonDocument.Parse(response.Content.ReadAsStringAsync().Result)
                    .RootElement.GetProperty("data").GetProperty("sno").GetString());
            }

        }
    }
}

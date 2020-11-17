using MedBook_RazorPages.External.Contracts;
using MedBook_RazorPages.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MedBook_RazorPages.Services;

namespace MedBook_RazorPages.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {

        private const string TokenVAlidationUrl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(FacebookAuthSettings facebookAuthSettings, IHttpClientFactory httpClinetFactory)
        {
            _facebookAuthSettings = facebookAuthSettings;
            _httpClientFactory = httpClinetFactory;
        }

        public async Task<FacebookUserInfoResult> GetUserInfoResult(string accessToken)
        {
            var formattedUrl = string.Format(UserInfoUrl, accessToken);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();
            var responsAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responsAsString);
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(TokenVAlidationUrl, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();
            var responsAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responsAsString);
        }
    }
}


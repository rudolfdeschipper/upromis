using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Services;
using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;

namespace IdentityServerAspNetIdentity.Models
{

    public class ClaimValues
    {
        public string Key;
        public string Value;
    }
    public class ContractClaimsLoader : IProfileService
    {

        private readonly UserManager<uPromis.Microservice.Identityserver.Models.ApplicationUser> _userManager;

        public ContractClaimsLoader(UserManager<uPromis.Microservice.Identityserver.Models.ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public virtual async Task GetProfileDataAsync(IdentityServer4.Models.ProfileDataRequestContext context)
        {

            context.IssuedClaims.AddRange(context.Subject.Claims);

            var client = new HttpClient();
            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            if (disco.IsError)
            {
                await Task.FromException(disco.Exception);
                Console.WriteLine(disco.Error);
                return;
            }

            // request token to access api1
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            });

            if (tokenResponse.IsError)
            {
                await Task.FromException(tokenResponse.Exception);
                Console.WriteLine(tokenResponse.ErrorDescription);
                return;
            }

            // make the api call
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // get user id - this is in the "sub" claim
            // avoids doing a round trip to the database
            var userID = context.Subject.Claims.FirstOrDefault(c => c.Type == "sub").Value;
            var response = await apiClient.GetAsync("http://localhost:5001/api/contract/getclaims/" + userID, HttpCompletionOption.ResponseContentRead); ;
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                // read the results
                var content = await response.Content.ReadAsStringAsync();

                // convert (in this case we only expect one claim back
                var result = JsonConvert.DeserializeObject<ClaimValues>(content);

                // add it to the claims collection
                var c = new System.Security.Claims.Claim(result.Key, result.Value);
                context.IssuedClaims.Add(c);

                Console.WriteLine(content);
            }
            await Task.CompletedTask;
            return;
        }

        public virtual async Task IsActiveAsync(IdentityServer4.Models.IsActiveContext context)
        {
            // here you would check if the user is still active:
            var user = await _userManager.GetUserAsync(context.Subject);
            // if email not confirmed, the user is not active
            context.IsActive = user.EmailConfirmed;
            await Task.CompletedTask;
            return;
        }
    }
}

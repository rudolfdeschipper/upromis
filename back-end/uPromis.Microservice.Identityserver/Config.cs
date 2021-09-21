// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace uPromis.Microservice.Identityserver
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                   };

        public static IEnumerable<ApiResource> Apis => new List<ApiResource>
        {
            // local API
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "Contract API"), // {  UserClaims = { "lastproject"} },
                new ApiScope("api2", "Attachment API") {},
                new ApiScope("api3", "Notification API") {},
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName) {},
                new ApiScope("api4", "Identity API") {},
                new ApiScope("api5", "Project API") {},
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    // commandline client
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    UserSsoLifetime = null,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1", "api2" }
                },
                new Client
                {
                    // MVC client
                    ClientId = "uProMIS",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,

                    UserSsoLifetime = null,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    UserSsoLifetime = null,

                    RedirectUris = { "http://localhost:5003/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5003/index.html" },
                    AllowedCorsOrigins = { "http://localhost:5003" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1",
                        "api2"
                    }
                },
                // SPA client, the real one
                new Client
                {
                    ClientId = "uProMISspa",
                    ClientName = "uProMIS frontend",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RequireConsent = false,
                    UserSsoLifetime = null,
                    RedirectUris = { "http://localhost:3000/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:3000/home" },
                    AllowedCorsOrigins = { "http://localhost:3000" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api2",
                        "api1",
                        "api3",
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api4",
                        "api5",
                    }
                }

            };
    }
}
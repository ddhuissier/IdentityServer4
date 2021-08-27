using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name = "ddh.scope",
                    UserClaims =
                    {
                        "ddh.bedroom"
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis() =>
            new List<ApiResource> {
                new ApiResource("WebApi"),
                new ApiResource("OthersApi", new string[] { "ddh.api.bedroom" }),
            };

        public static IEnumerable<Client> GetClients() =>
            new List<Client> {
                new Client {
                    ClientId = "client_id",
                    ClientSecrets = { new Secret("client_secret".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    AllowedScopes = { "WebApi" }
                },
                new Client {
                    ClientId = "client_id_mvc",
                    ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    RedirectUris = { "https://localhost:44322/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44322/Home/Index" },

                    AllowedScopes = {
                        "WebApi",
                        "OthersApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        "ddh.scope",
                    },

                    // puts all the claims in the id token
                    //AlwaysIncludeUserClaimsInIdToken = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false,
                },
                new Client {
                    ClientId = "client_id_js",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:44345/home/signin" },
                    PostLogoutRedirectUris = { "https://localhost:44345/Home/Index" },
                    AllowedCorsOrigins = { "https://localhost:44345" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "WebApi",
                        "OthersApi",
                        "ddh.scope",
                    },

                    AccessTokenLifetime = 1,

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },

                new Client {
                    ClientId = "angular",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "WebApi",
                        "OthersApi",
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },

                new Client {
                    ClientId = "flutter",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4000/" },
                    AllowedCorsOrigins = { "http://localhost:4000" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "WebApi",
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },

            };
    }
}

using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace SGN.IDP.Configuration
{
    public static class Config
    {

        public static IEnumerable<TestUser> GetUsers()
        {

            return new[] {
                new TestUser(){
                    SubjectId = "1",
                    Username = "Frank",
                    Password = "password",
                    IsActive = true,
                    Claims = new List<Claim>{
                        new Claim("given_name", "Frank"),
                        new Claim("family_name", "Underwood"),
                        new Claim("address", "in the street of his dreams"),
                        new Claim("role", "FreeUser")
                    }
                },
                new TestUser(){
                    SubjectId = "2",
                    Username = "Claire",
                    Password = "password",
                    IsActive = true,
                    Claims = new List<Claim>{
                        new Claim("given_name", "Claire"),
                        new Claim("family_name", "Underwood"),
                        new Claim("address", "main street 1"),
                        new Claim("role", "PayingUser")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources(){

            return new List<IdentityResource> { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles","You're roles", new []{"role"})
            };
        }

        public static IEnumerable<Client> GetClients(){

            return new List<Client>(){
                new Client(){
                    ClientName = "Image Gallery",
                    ClientId = "imagegalleryclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new []{
                        "https://localhost:5002/signin-oidc"
                    },
                    PostLogoutRedirectUris = {
                        "https://localhost:5002/signout-callback-oidc"
                    },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles"
                    },
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    }
                }};
        }
    }
}
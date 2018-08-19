using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SGN.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            */

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                    .AddCookie("Cookies", options =>
                    {
                        options.AccessDeniedPath = "/Authorization/AccessDenied";
             })
                    .AddOpenIdConnect("oidc", options =>
                    {
                        options.SignInScheme = "Cookies";
                        options.Authority = "https://localhost:5001";
                        options.ClientId = "imagegalleryclient";
                        options.ClientSecret = "secret";
                        options.ResponseType = "code id_token";
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.Scope.Add("address");
                        options.Scope.Add("roles");
                        options.SaveTokens = true;
                        options.GetClaimsFromUserInfoEndpoint = true;
                        options.ClaimActions.Remove("amr");
                        options.ClaimActions.DeleteClaim("sid");
                        options.ClaimActions.DeleteClaim("idp");
                        options.ClaimActions.MapUniqueJsonKey("role", "role");

                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            NameClaimType = JwtClaimTypes.GivenName,
                            RoleClaimType = JwtClaimTypes.Role
                        };
                    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

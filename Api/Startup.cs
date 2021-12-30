using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.Enums;
using Domain.Auth;
using Domain.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Services;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = AuthOptions.SignIssuer,
                        ValidAudience = AuthOptions.SignAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AuthOptions.SignKey)),
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });
            
            var permissions = (RolePermissions[]) Enum.GetValues(typeof(RolePermissions));

            services.AddAuthorization(options =>
            {
                permissions.ToList().ForEach(permission =>
                {
                    options.AddPolicy(permission.GetPermissionName(),
                        policy => policy.RequireClaim(RolePermissionExtension.ClaimType,
                            permission.GetPermissionName()));
                });
            });
            
            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                })
                ;
            
            string version = GetMajorVersion();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{version}", new OpenApiInfo()
                {
                    Version = $"v{version}",
                    Title = "BiBi Game API",
                    Description = "API for BiBi Game"
                });
                
            });
            
            services.AddDomain(Configuration, true);
            
            // services.AddDataProtection()
            //     .PersistKeysToFileSystem(new System.IO.DirectoryInfo(@"./"))
            //     .ProtectKeysWithCertificate(GetCertificate());
            
        }
        
        // private X509Certificate2 GetCertificate()
        // {
        //     var assembly = typeof(Startup).GetTypeInfo().Assembly;
        //     using (var stream = assembly.GetManifestResourceStream(
        //         assembly.GetManifestResourceNames().First(r => r.EndsWith("api.pfx"))))
        //     {
        //         if (stream == null)
        //             throw new ArgumentNullException(nameof(stream));
        //
        //         var bytes = new byte[stream.Length];
        //         stream.Read(bytes, 0, bytes.Length);
        //         return new X509Certificate2(bytes);
        //     }
        // }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              //  app.UseHsts();
            }

            
          //  app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseMvc();

            app.UseSwagger(config => { config.RouteTemplate = "api/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                string version = GetMajorVersion();
                c.SwaggerEndpoint($"/api/swagger/v{version}/swagger.json", $"BiBi Game API v{version}");
                c.RoutePrefix = "api/swagger";
            });
        }
        
        private string GetMajorVersion()
        {
            var versionString = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3);
            return versionString;
        }
    }
}
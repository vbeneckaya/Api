using System;
using DAL;
using DAL.Services;
using Domain.Auth;
using Domain.Download;
using Domain.Error;
using Domain.Log;
using Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services
{
    public static class DomainInstaller
    {
        public static void AddDomain(this IServiceCollection services, IConfiguration configuration, bool migrateDb)
        {
            services.AddSingleton(configuration);

            services.AddScoped<AppDbContext, AppDbContext>();
            services.AddScoped<ICommonDataService, CommonDataService>();
            //  services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IErrorService, ErrorHandlerService>();
            services.AddScoped<IDownloadService, DownloadService>();

            InitDatabase(services, configuration, migrateDb);
        }
        
        

        private static void InitDatabase(IServiceCollection services, IConfiguration configuration, bool migrateDb)
        {
            var connectionString = configuration.GetConnectionString("DefaultDatabase");

            //var buildServiceProvider = services.AddEntityFrameworkNpgsql();

            try
            {
                var serviceProvider = services.AddDbContext<AppDbContext>((sp,options) =>
                    {
                        options.UseNpgsql(connectionString);
                    })
                    .BuildServiceProvider();
            
                var appDbContext = serviceProvider.GetService<AppDbContext>();

                Console.WriteLine(appDbContext?.Database.GetDbConnection().ConnectionString);

                if (migrateDb)
                {
                    //appDbContext.DropDb();
                    appDbContext?.Migrate(connectionString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        
    }
}

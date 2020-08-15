using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SmartHome.Services;
using Microsoft.EntityFrameworkCore;
using SmartHome.Contexts;

namespace SmartHome
{
    public class Startup {

        //private const string Connection = "Data Source=db-mssql;Initial Catalog=s18911;Integrated Security=True";
        private const string Connection = "Host=sh_postgres;Database=smarthomedb;Username=bursztyn;Password=openflow";
        public Startup(IConfiguration configuration){
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddDbContext<SmartHomeDbContext>(options =>            {
                options.UseNpgsql(Connection);
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "SmartHome",
                    ValidAudience = "Clients",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
                };
            });
            services.AddScoped<ISmartHomeDbService, EfSmartHomeDbService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env){
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else{
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            //app.UseMiddleware<AccountMiddleware>();

            app.UseEndpoints(endpoints =>{
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=SmartHome}/{action=Index}/{id?}");
            });
        }
    }
}

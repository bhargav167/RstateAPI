using System;
using System.IO;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RstateAPI.Data;
using RstateAPI.EmailServices;
using RstateAPI.Helpers;

namespace RstateAPI {
    public class Startup {
        private readonly IConfiguration _configuration;
        public Startup (IConfiguration configuration) {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<StoreContext> (s => s.UseMySql (_configuration.GetConnectionString ("DefaultConnection")));
            services.AddCors ();
            services.Configure<ApplicationSetting> (_configuration.GetSection ("AppSettings"));
            services.AddIdentityCore<ApplicationUser> ().AddRoles<IdentityRole> ().AddEntityFrameworkStores<StoreContext> ();
            services.Configure<CloudinarySettings> (_configuration.GetSection ("CloudinarySettings"));
            services.AddAutoMapper ();
            services.AddControllers ();
            services.AddScoped<DbContext, StoreContext> ();
            services.AddTransient<ISpacingRepo, SapcingRepo> ();
            services.AddSingleton<IEmailSender, EmailSender> ();
            services.Configure<EmailOptions> (_configuration);
            services.Configure<FormOptions> (o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            services.Configure<IdentityOptions> (opt => {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 4;
            });
            var key = Encoding.UTF8.GetBytes (_configuration["AppSettings:Token"].ToString ());

            services.AddAuthentication (x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer (c => {
                c.RequireHttpsMetadata = false;
                c.SaveToken = false;
                c.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey (key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddGoogle (googleOptions => {
                IConfigurationSection googleAuthNSection =
                    _configuration.GetSection ("Authentication:Google");
                googleOptions.ClientId = googleAuthNSection["ClientId"];
                googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
            });

            services.AddControllersWithViews ()
                .AddNewtonsoftJson (options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseCors (x => x.AllowAnyOrigin ().AllowAnyMethod()
                .AllowAnyHeader ()
            ); 
            app.UseRouting ();
            app.UseAuthentication ();
            app.UseAuthorization ();
            
            app.UseStaticFiles ();
            // app.UseStaticFiles (new StaticFileOptions () {
            //     FileProvider = new PhysicalFileProvider (Path.Combine (Directory.GetCurrentDirectory (), @"Resources")),
            //         RequestPath = new PathString ("/Resources")
            // });

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
                endpoints.MapFallbackToController ("Index", "Fallback");
            });

        }
    }
}
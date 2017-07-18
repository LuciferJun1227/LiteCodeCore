using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

using LuckyCode.Core.WebSocket;
using LuckyCode.Core.WebSocketChat;
using LuckyCode.Core.Data;
using LuckyCode.Core.Data.DapperExtensions;
using LuckyCode.Core.Filtes;
using LuckyCode.Core.Middleware;
using LuckyCode.Core.Redis;
using LuckyCode.Data;
using LuckyCode.Entity.IdentityEntity;
using LuckyCode.Service;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.WebFrameWork.MvcCaptcha;
using LuckyCode.WebFrameWork.TagHelper.MVCPager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.Webpack;
using NLog.Extensions.Logging;
using NLog.LayoutRenderers;
using NLog.Web;
using HttpContext = LuckyCode.Core.Utility.HttpContext;

namespace LuckyCode.WebSite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LiteCodeContext>(options =>
                   options.UseMySql(Configuration.GetConnectionString("mySqlConnection")));
            //services.AddSingleton(Configuration);
            services.AddOptions();
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            services.AddIdentity<SysUsers, SysRoles>()
                .AddEntityFrameworkStores<LiteCodeContext, string>()
                .AddDefaultTokenProviders();
            // Add framework services.
            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 1024 * 1024 * 100; //100M上传
            });
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
            });
            // services.AddScoped<ILiteCodeContext>(x => new LiteCodeContext(new DbContextOptionsBuilder<LiteCodeContext>().UseSqlServer(Configuration.GetConnectionString("mySqlConnection")).Options));
            services.AddScoped<ILiteCodeContext>(x => x.GetService<LiteCodeContext>());
            services.AddScoped<IMainContext>(x => x.GetService<ILiteCodeContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IDapperContext,DapperContext>();
            // services.AddScoped<IDatabase>(x => new Database(Configuration.GetConnectionString("mySqlConnection"), DatabaseType.MySQL, Pomelo.Data.MySql.MySqlClientFactory.Instance));
            services.Configure<RedisConnection>(Configuration.GetSection("RedisConfig:DefaultConnection"));

            services.AddSingleton<RedisClientManager>();
            services.AddSingleton<ICacheClient, RedisCacheClient>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.CanViewUsers,
                    policy => policy.Requirements.Add(new ResourceRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, ResourceHandler>();
            services.AddTransient<IPagerdMetaService, PagerdMetaService>();
            services.AddService();
            services.AddWebSocketManager();
            services.AddScoped<ICaptchaValidManager, CaptchaValidManager>();
            // services.AddScoped<IStartupFilter>(x=>new Hotlinking());
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });
           
            AutoMapperConfiguration.Init();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddNLog();
            app.UseSession();
            app.AddNLogWeb();
            //注册根目录地址
            LayoutRenderer.Register("basedir", (logEvent) => env.ContentRootPath);

            HttpContext.Configure(app.ApplicationServices.
                GetRequiredService<Microsoft.AspNetCore.Http.IHttpContextAccessor>()
            );
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "SysManager",
                LoginPath = "/SysManager/Account/Login",
                LogoutPath = "/SysManager/Account/LoginOut",
                AccessDeniedPath = "/SysManager/Account/Login",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                CookieHttpOnly = true,

                ExpireTimeSpan = new TimeSpan(0, 0, 30, 0),
                CookiePath = "/"
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //
            app.UseMiddleware<CaptchaMiddleware>();
            app.UseWebSockets();
            app.MapWebSocketManager("/LiveChat", serviceProvider.GetService<ChartHandler>());
            app.UseStaticFiles();
            //app.UseMiddleware<LoggerMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                  routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
    }
}

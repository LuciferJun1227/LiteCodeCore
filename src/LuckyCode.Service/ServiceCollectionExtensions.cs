using LuckyCode.Core.Service;
using LuckyCode.IService;
using LuckyCode.IService.News;
using LuckyCode.Service.News;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyCode.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services)
        {
            services.AddSingleton<ISignal, Signal>();
            services.AddScoped<ISysUserService, SysUserService>();
            services.AddScoped<ISysModulesService, SysModulesService>();
            services.AddScoped<ISysApplicationService, SysApplicationService>();
            services.AddScoped<ISysDepartmentService, SysDepartmentService>();
            services.AddScoped<ISysRolesService, SysRolesService>();
            services.AddScoped<ILinkService, LinkService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<INewsArticleService, NewsArticleService>();
            services.AddScoped<INewsBannerService, NewsBannerService>();
            return services;
        }
    }
}

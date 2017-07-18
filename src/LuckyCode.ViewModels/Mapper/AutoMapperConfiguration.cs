using AutoMapper;
using LuckyCode.Entity.IdentityEntity;
using LuckyCode.Entity.News;
using LuckyCode.Entity.OauthBase;
using LuckyCode.ViewModels.News;
using LuckyCode.ViewModels.SiteManager;

namespace LuckyCode.ViewModels.Mapper
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration _mapperConfiguration;
        private static IMapper _mapper;

        public static void Init()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                //用户
                cfg.CreateMap<SysUsers, SysUsersCreateViewModel>();
                cfg.CreateMap<SysUsersCreateViewModel, SysUsers>();
                cfg.CreateMap<SysDepartment, SysDepartmentViewModel>().ForMember(vm => vm.Id, en => en.MapFrom(a => a.DepartmentId));
                cfg.CreateMap<SysDepartmentViewModel, SysDepartment>();

                cfg.CreateMap<SysModules, SysModuleViewModel>().ForMember(vm => vm.ModuleType, en => en.MapFrom(a => (ModuleType)a.ModuleType));
                cfg.CreateMap<SysModuleViewModel, SysModules>();

               

                cfg.CreateMap<SysRoles, SysRoleViewModel>().ForMember(vm => vm.RoleType, en => en.MapFrom(a => (RoleType)a.RoleType));
                cfg.CreateMap<SysRoleViewModel, SysRoles>();

                cfg.CreateMap<SysApplication, SysApplicationViewModel>();
                cfg.CreateMap<SysApplicationViewModel, SysApplication>();

                cfg.CreateMap<NewsArticle, ArticleViewModel>();
                cfg.CreateMap<ArticleViewModel, NewsArticle>();

                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<CategoryViewModel, Category>();

                cfg.CreateMap<Link, LinkViewModel>();
                cfg.CreateMap<LinkViewModel, Link>();

                cfg.CreateMap<NewsBanner, NewsBannerViewModel>();
                cfg.CreateMap<NewsBannerViewModel, NewsBanner>();

            });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Mapper
        /// </summary>
        public static IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }
        /// <summary>
        /// Mapper configuration
        /// </summary>
        public static MapperConfiguration MapperConfiguration
        {
            get
            {
                return _mapperConfiguration;
            }
        }
    }
}

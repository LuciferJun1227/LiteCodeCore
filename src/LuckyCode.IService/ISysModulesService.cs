using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.Core.Utility;
using LuckyCode.ViewModels.SiteManager;

namespace LuckyCode.IService
{
    public interface ISysModulesService
    {
        Task<SysModuleViewModel> SaveSysModule(SysModuleViewModel model);
        Task<SysModuleViewModel> UpdateSysModule(SysModuleViewModel model);
        Task DeleteSysModule(string id);
        Task<PagedList<SysModuleViewModel>> GetPagedList(int pageIndex, int pageSize);
        Task<SysModuleViewModel> GetModuleViewModel(string id);
        Task<List<ListItemEntity>> ModuleItemEntities();
        Task<List<SysModuleBase>> GetModuleBases();
        Task<List<string>> GetControllerNameList();
        Task<List<SysModuleViewModel>> GetSysModuleViewModels(string roleId);
        Task UpdateModuleSort(SysModuleSortViewModel[] items);
        Task<List<SysModuleSortViewModel>> GetModuleSortViewModels(string id);
        Task<bool> ValidateActionName(string id, string controllerName, string actionName);
    }
}

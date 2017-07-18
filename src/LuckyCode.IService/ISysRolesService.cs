using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.Core.Utility;
using LuckyCode.ViewModels.SiteManager;

namespace LuckyCode.IService
{
    public interface ISysRolesService
    {
        Task<SysRoleViewModel> SaveSysRole(SysRoleViewModel model);
        Task<SysRoleViewModel> UpdateSysModule(SysRoleViewModel model);
        Task DeleteSysRole(string id);
        Task<PagedList<SysRoleViewModel>> GetPagedList(int pageIndex, int pageSize);
        Task<SysRoleViewModel> GetSysRoleViewModel(string id);
        Task SaveRoleModule(SysRoleModuleViewModel model);
        Task<List<SysRoleModulePurviewViewModel>> GetModulePurviewViewModel(string roleid);
        Task<List<ListItemEntity>> GetRoleItemEntities();
    }
}

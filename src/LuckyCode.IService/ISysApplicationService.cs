using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.Core.Utility;
using LuckyCode.ViewModels.SiteManager;

namespace LuckyCode.IService
{
    public interface ISysApplicationService
    {
        Task DeleteSysApplication(string id);
        Task<SysApplicationViewModel> SaveSysApplication(SysApplicationViewModel model);
        Task<SysApplicationViewModel> UpdateSysApplication(SysApplicationViewModel model);
        Task<PagedList<SysApplicationViewModel>> GetPagedList(int pageIndex, int pageSize);
        Task<SysApplicationViewModel> GetApplicationViewModel(string id);
        Task<List<ListItemEntity>> AppItemEntities();
    }
}

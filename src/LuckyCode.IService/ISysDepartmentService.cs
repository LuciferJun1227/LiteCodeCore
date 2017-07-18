using System.Collections.Generic;
using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.Core.Utility;
using LuckyCode.ViewModels.SiteManager;

namespace LuckyCode.IService
{
    public interface ISysDepartmentService
    {
        Task<SysDepartmentViewModel> SaveSysDepartment(SysDepartmentViewModel model);
        Task<SysDepartmentViewModel> UpdateSysDeparment(SysDepartmentViewModel model);
        Task DeleteDepartment(string id);
        Task<PagedList<SysDepartmentViewModel>> GetPagedList(int pageIndex, int pageSize);
        Task<SysDepartmentViewModel> GetSysDepartmentViewModel(string id);
        Task<List<ListItemEntity>> GetDepartmentTree();
    }
}

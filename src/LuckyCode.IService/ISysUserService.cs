using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.ViewModels;

namespace LuckyCode.IService
{
    public interface ISysUserService
    {
        Task<PagedList<SysUsersCreateViewModel>> GetPagedList(int pageIndex, int pageSize);
        Task<SysUsersCreateViewModel> GetSysUsersViewModel(string id);
        /// <summary>
        /// 判断用户名是否重复
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> Exits(string id, string userName);
    }
}

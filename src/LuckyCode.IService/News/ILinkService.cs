using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.ViewModels.News;

namespace LuckyCode.IService.News
{
    public interface ILinkService
    {
        void DeleteLink(string id);
        LinkViewModel SaveLink(LinkViewModel model);
        LinkViewModel UpdateLink(LinkViewModel model);
        PagedList<LinkViewModel> GetPagedList(int pageIndex, int pageSize);
        Task<LinkViewModel> GetLinkViewModel(string id);
       
    }
}

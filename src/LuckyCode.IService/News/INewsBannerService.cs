using System;
using System.Threading.Tasks;
using LuckyCode.Core;
using LuckyCode.ViewModels.News;

namespace LuckyCode.IService.News
{
   public interface INewsBannerService
    {
        void DeleteNewsBanner(Guid id);
        NewsBannerViewModel SaveNewsBanner(NewsBannerViewModel model);
        NewsBannerViewModel UpdateNewsBanner(NewsBannerViewModel model);
        PagedList<NewsBannerViewModel> GetPagedList(int pageIndex, int pageSize);
        Task<NewsBannerViewModel> GetNewsBannerViewModel(Guid id);
        
    }
}

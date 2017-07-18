using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LuckyCode.Core;
using LuckyCode.Core.Data;
using LuckyCode.Core.Utility.Sequence;
using LuckyCode.Data;
using LuckyCode.Entity.News;
using LuckyCode.IService.News;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.News;
using Microsoft.Extensions.Logging;

namespace LuckyCode.Service.News
{
    public class LinkService : ILinkService
    {
        private IRepository<Link> _repository;
        private ILogger _logger;
        private ILiteCodeContext _context;
        public LinkService(ILiteCodeContext context, IRepository<Link> repository,ILogger<LinkService> logger )
        {
            _repository = repository;
            _logger = logger;
            _context = context;
        }
       

        public void DeleteLink(string id)
        {
            _repository.Delete(a=>a.LinkID==new Guid(id));
            _context.SaveChanges();
        }

        public async Task<LinkViewModel> GetLinkViewModel(string id)
        {
            var entity =await _repository.SingleAsync(a => a.LinkID == new Guid(id));
            var model = entity.ToModel();
            
            return entity.ToModel();
        }

        public PagedList<LinkViewModel> GetPagedList(int pageIndex, int pageSize)
        {
            return _repository.Query().ProjectTo<LinkViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateDate).ToPagedList(pageIndex, pageSize);
        }

        public LinkViewModel SaveLink(LinkViewModel model)
        {
            var entity = model.ToEntity();
            entity.LinkID = SequenceQueue.NewIdGuid();
            entity.CreateDate=DateTime.Now;
            entity.IsImage = model.IsImage;
            _repository.AddAsync(entity);
            _context.SaveChanges();
            return model;
        }

        public LinkViewModel UpdateLink(LinkViewModel model)
        {
            var entity = model.ToEntity();
            _repository.Update(entity);
            _context.SaveChanges();
            return model;
        }
    }
}

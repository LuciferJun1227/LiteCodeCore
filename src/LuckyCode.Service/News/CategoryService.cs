using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LuckyCode.Core;
using LuckyCode.Core.Data;
using LuckyCode.Core.Utility;
using LuckyCode.Core.Utility.Sequence;
using LuckyCode.Data;
using LuckyCode.Entity.News;
using LuckyCode.IService.News;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.News;
using Microsoft.Extensions.Logging;

namespace LuckyCode.Service.News
{
    public class CategoryService : ICategoryService
    {
        private IRepository<Category> _repository;
        private ILiteCodeContext _context;
        private ILogger _logger;
        public CategoryService(ILiteCodeContext context, IRepository<Category> repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
            _context = context;
        }
        public List<ListItemEntity> AppItemEntities()
        {
            return
                _repository.Query()
                    .Select(a => new ListItemEntity() {ID = a.CategoryId, ParentID = a.ParentId, Title = a.Title})
                    .ToList();
        }

        public void DeleteCategory(string id)
        {
           _repository.Delete(a=>a.CategoryId==id);
            _context.SaveChanges();
        }

        public async Task<CategoryViewModel> GetCategoryViewModel(string id)
        {
            var entity =await _repository.SingleAsync(a => a.CategoryId == id);
            return entity.ToModel();
        }

        public async Task<PagedList<CategoryViewModel>> GetPagedList(int pageIndex, int pageSize)
        {
            return
               await _repository.Query()
                    .ProjectTo<CategoryViewModel>(AutoMapperConfiguration.MapperConfiguration)
                    .OrderByDescending(a => a.CreateDate)
                    .ToPagedListAsync(pageIndex, pageSize);
        }

        public CategoryViewModel SaveCategory(CategoryViewModel model)
        {
            var entity = model.ToEntity();
            entity.CategoryId = SequenceQueue.NewIdString("");
            entity.CreateDate=DateTime.Now;
            _repository.AddAsync(entity);
            _context.SaveChanges();
            return model;
        }

        public CategoryViewModel UpdateCategory(CategoryViewModel model)
        {
            var entity = model.ToEntity();
            _repository.Update(entity);
            _context.SaveChanges();
            return model;
        }
    }
}

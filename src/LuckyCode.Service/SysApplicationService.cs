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
using LuckyCode.Entity.OauthBase;
using LuckyCode.IService;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.SiteManager;
using Microsoft.EntityFrameworkCore;

namespace LuckyCode.Service
{
    public class SysApplicationService:ISysApplicationService
    {
        private IRepository<SysApplication> _repository;
        private ILiteCodeContext _context;
        public SysApplicationService(IRepository<SysApplication> repository,ILiteCodeContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task<List<ListItemEntity>> AppItemEntities()
        {
            return await _repository.Query().Select(a => new ListItemEntity() { ID = a.Id, ParentID = "0", Title = a.ApplicationName }).ToListAsync();
        }

        public async Task DeleteSysApplication(string id)
        {
            _repository.Delete( id);
            await _context.SaveChangesAsync();
        }

        public async Task<SysApplicationViewModel> GetApplicationViewModel(string id)
        {
            var entity =await _repository.SingleAsync(a => a.Id == id);
            SysApplicationViewModel model = new SysApplicationViewModel();
            model.Id = entity.Id;
            model.ApplicationName = entity.ApplicationName;
            model.ApplicationUrl = entity.ApplicationUrl;
            return model;
        }

        public async Task<PagedList<SysApplicationViewModel>> GetPagedList(int pageIndex, int pageSize)
        {
            return await _repository.Query().ProjectTo<SysApplicationViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateTime).ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<SysApplicationViewModel> SaveSysApplication(SysApplicationViewModel model)
        {
            SysApplication entity = new SysApplication();
            entity.ApplicationName = model.ApplicationName;
            entity.ApplicationUrl = model.ApplicationUrl;
            entity.CreateTime = DateTime.Now;
            entity.Id = SequenceQueue.NewIdString("");
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
            model.Id = entity.Id;
            model.CreateTime = entity.CreateTime;
            return model;
        }

        public async Task<SysApplicationViewModel> UpdateSysApplication(SysApplicationViewModel model)
        {
            SysApplication entity = new SysApplication();
            entity.ApplicationName = model.ApplicationName;
            entity.ApplicationUrl = model.ApplicationUrl;
            entity.Id = model.Id;
            _repository.Update(entity,  a => a.ApplicationName, a => a.ApplicationUrl);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}

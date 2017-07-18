using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LuckyCode.Core;
using LuckyCode.Core.Data;
using LuckyCode.Core.Service;
using LuckyCode.Core.Utility;
using LuckyCode.Core.Utility.Sequence;
using LuckyCode.Data;
using LuckyCode.Entity.OauthBase;
using LuckyCode.IService;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.SiteManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace LuckyCode.Service
{
    public class SysModulesService : ISysModulesService
    {
        private IRepository<SysModules> _repository;
        private IRepository<SysRoleModules> _roleModulesRepository;
        private ILiteCodeContext _context;
        private IMemoryCache _memoryCache;
        private ISignal _signal;

        public SysModulesService(ISignal signal,IMemoryCache memoryCache, IRepository<SysModules> repository, IRepository<SysRoleModules> roleModulesRepository, ILiteCodeContext context)
        {
            _repository = repository;
            _roleModulesRepository = roleModulesRepository;
            _context = context;
            _memoryCache = memoryCache;
            _signal = signal;
        }

        public async Task DeleteSysModule(string id)
        {
            await Task.Run(() => _repository.Delete(id));
        }

        public async Task<List<string>> GetControllerNameList()
        {
            return await _repository.Query().Where(a => a.ControllerName != "").GroupBy(a => a.ControllerName).Select(a => a.Key).ToListAsync();
        }

        public async Task<List<SysModuleBase>> GetModuleBases()
        {
            return await _repository.Query().Select(a => new SysModuleBase()
            {
                ControllerName = a.ControllerName,
                Id = a.Id,
                ModuleName = a.ModuleName,
                ParentId = a.ParentId,
                
                PurviewSum = a.PurviewSum,
                ActionName = a.ActionName,
                ModuleType = a.ModuleType,
                IsValidPurView = a.IsValidPurView

            }).ToListAsync();
        }

        public async Task<SysModuleViewModel> GetModuleViewModel(string id)
        {
            var entity = await _repository.SingleAsync(a => a.Id == id);
            return entity.ToModel();
        }

        public async Task<PagedList<SysModuleViewModel>> GetPagedList(int pageIndex, int pageSize)
        {
            return await _repository.Query().ProjectTo<SysModuleViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateTime).ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<List<ListItemEntity>> ModuleItemEntities()
        {
            return await _repository.Query().Where(a => a.ModuleType == 0 && a.ControllerName == "").Select(a => new ListItemEntity() {ID = a.Id, ParentID = "0", Title = a.ModuleName}).ToListAsync();
        }

        public async Task<SysModuleViewModel> SaveSysModule(SysModuleViewModel model)
        {
            int pnum = 0;
            if (model.ControllerName != null)
            {
                pnum = GetPurviewNum(model.ControllerName);
            }
            model.ControllerName = model.ControllerName == null ? model.ControllerName = "" : model.ControllerName;
            var entity = model.ToEntity();
            entity.Id = SequenceQueue.NewIdString("");
            entity.CreateTime = DateTime.Now;
            entity.Sort = 0;
            entity.PurviewNum = model.ControllerName == "" ? 0 : pnum + 1;
            entity.PurviewSum = model.ControllerName == "" ? 0 : 2L << pnum;
            entity.Application = null;
            try
            {
                await _repository.AddAsync(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return model;
        }

        public async Task<List<SysModuleViewModel>> GetSysModuleViewModels(string roleId)
        {
            try
            {
                var c = HttpContext.Current;
               return await _memoryCache.GetOrCreateAsync(roleId, async entity =>
                {
                    entity.ExpirationTokens.Add(_signal.GetToken(roleId));
                    var list = await _repository.Query().Where(a => a.IsDelete == false && a.ModuleType == 0).ProjectTo<SysModuleViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateTime).ToListAsync();
                    var rolePurviewlist = await _roleModulesRepository.ListAsync(a => a.RoleId == roleId);
                    List<SysModuleViewModel> tem = new List<SysModuleViewModel>();
                    foreach (var module in list)
                    {
                        if (module.ParentId != "0")
                        {
                            var p = rolePurviewlist.FirstOrDefault(a => a.ControllerName == module.ControllerName);
                            if ((module.PurviewSum & p?.PurviewSum) == module.PurviewSum)
                            {
                                tem.Add(module);
                                if (tem.All(a => a.Id != module.ParentId))
                                {
                                    tem.Add(list.FirstOrDefault(a => a.Id == module.ParentId));
                                }
                            }
                        }

                    }
                    return tem.OrderBy(a => a.Sort).ToList();
                });

            }
            catch (Exception ex)
            {
                
                
            }
            return await Task.FromResult(new List<SysModuleViewModel>());
        }
        public async Task<SysModuleViewModel> UpdateSysModule(SysModuleViewModel model)
        {
            var entity =await _repository.SingleAsync(a => a.Id == model.Id);
            model.ControllerName = model.ControllerName == null ? model.ControllerName = "" : model.ControllerName;
            entity = model.ToEntity(entity);
            _repository.Update(entity);
            return model;
        }

        private  int GetPurviewNum(string controlleName)
        {
            var list = _repository.Query(a => a.ControllerName == controlleName).OrderBy(a => a.PurviewNum).ToList();
            int z = 0;
            foreach (var modulese in list)
            {
                z = z + 1;
                if (modulese.PurviewNum != z)
                {
                    z = z - 1;
                    break;
                }
            }
            return z;
        }

        public async Task UpdateModuleSort(SysModuleSortViewModel[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var entity = new SysModules();
                entity.Id = items[i].Id;
                entity.Sort = i + 1;
                entity.ParentId = "0";
                entity.ModuleName = " ";
                await Task.Run(() =>_repository.Update(entity,  a => a.Sort ));
            }
            _context.SaveChanges();
        }

        public async Task<List<SysModuleSortViewModel>> GetModuleSortViewModels(string id)
        {
            return await _repository.Query().Where(a => a.ParentId == id && a.ModuleType == 0).Select(a => new SysModuleSortViewModel() { Id = a.Id, ModuleName = a.ModuleName }).ToListAsync();
        }

        public async Task<bool> ValidateActionName(string id, string controllerName, string actionName)
        {
            return await _repository.ExistsAsync(a => a.Id != id && a.ControllerName == controllerName && a.ActionName == actionName);
        }
    }
}

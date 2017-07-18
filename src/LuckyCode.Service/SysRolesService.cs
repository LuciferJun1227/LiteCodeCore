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
using LuckyCode.Entity.IdentityEntity;
using LuckyCode.Entity.OauthBase;
using LuckyCode.IService;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.SiteManager;
using Microsoft.EntityFrameworkCore;

namespace LuckyCode.Service
{
    public class SysRolesService : ISysRolesService
    {
        private ILiteCodeContext _context;
        private IRepository<SysRoles> _repository;
        private IRepository<SysRoleModules> _rolemoduleRepository;
        private ISignal _signal;
        public SysRolesService(ISignal signal, IRepository<SysRoles> repository, IRepository<SysRoleModules> rolemoduleRepository, ILiteCodeContext context)
        {
            _repository = repository;
            _rolemoduleRepository = rolemoduleRepository;
            _context = context;
            _signal = signal;
        }

        public async Task DeleteSysRole(string id)
        {
            await Task.Run(()=>_repository.Delete(id));
        }

        public async Task<List<SysRoleModulePurviewViewModel>> GetModulePurviewViewModel(string roleid)
        {
            return await _rolemoduleRepository.Query().Where(a => a.RoleId == roleid).Select(a => new SysRoleModulePurviewViewModel()
            {
                ControllerName = a.ControllerName,
                PurviewSum = a.PurviewSum,
                RoleId = a.RoleId
            }).ToListAsync();

        }

        public async Task<PagedList<SysRoleViewModel>> GetPagedList(int pageIndex, int pageSize)
        {
            return await _repository.Query().ProjectTo<SysRoleViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateTime).ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<List<ListItemEntity>> GetRoleItemEntities()
        {
            return await _repository.Query().Select(a => new ListItemEntity() { ID = a.Id, ParentID = "0", Title = a.RoleName }).ToListAsync();
        }

        public async Task<SysRoleViewModel> GetSysRoleViewModel(string id)
        {
            var entity = await _repository.SingleAsync(a => a.Id == id);
            return entity.ToModel();
        }

        public async Task SaveRoleModule(SysRoleModuleViewModel model)
        {
            _rolemoduleRepository.Delete(a => a.RoleId == model.RoleId);
            List<SysRoleModules> list = new List<SysRoleModules>();
            foreach (SysModuleBase moduleBase in model.ModuleBases)
            {
                list.Add(new SysRoleModules() {ModuleId =SequenceQueue.NewIdString(""), PurviewSum = moduleBase.PurviewSum, ApplicationId = "",  RoleId = model.RoleId, ControllerName = moduleBase.ControllerName });
            }
            await _rolemoduleRepository.AddRangeAsync(list);
            _context.SaveChanges();
            _signal.SignalToken(model.RoleId);//设置过期
        }

        public async Task<SysRoleViewModel> SaveSysRole(SysRoleViewModel model)
        {
            var entity = model.ToEntity();
            entity.Id = SequenceQueue.NewIdString("");
            entity.RoleName = model.Name;
            entity.CreateTime = DateTime.Now;
            entity.RoleType = (int)model.RoleType;
            entity.IsDelete = false;
            await _repository.AddAsync(entity);
            return model;
        }

        public async Task<SysRoleViewModel> UpdateSysModule(SysRoleViewModel model)
        {
            var entity = model.ToEntity();
            entity.Id = model.Id;
            entity.RoleName = model.Name;
            entity.IsDelete = model.IsDelete;
           await Task.Run(()=> _repository.Update(entity,  a => a.IsDelete, a => a.RoleType, a => a.RoleName, a => a.Name, a => a.RoleDescription ));
            return model;
        }
    }
}

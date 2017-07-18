using System.Reflection;
using LuckyCode.Core.Data;
using LuckyCode.Core.Data.Extensions;
using LuckyCode.Entity.IdentityEntity;
using LuckyCode.Entity.OauthBase;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LuckyCode.Data
{
    public interface ILiteCodeContext:IMainContext
    {
        DbSet<SysRoles> Roles { get; set; }
        DbSet<SysUsers> Users { get; set; }
    }
    
   
    public class LiteCodeContext : IdentityDbContext<SysUsers, SysRoles, string>, ILiteCodeContext
    {
        //public LiteCodeContext(IConfigurationRoot configuration)
        //{
        //    Configuration = configuration;
        //}
        public LiteCodeContext(DbContextOptions<LiteCodeContext> options):base(options)
        {
             
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //   // optionsBuilder.UseSqlServer(Configuration.GetConnectionString("mySqlConnection"));
        //}
       // public IConfigurationRoot Configuration { get; }
        public DbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public DbSet<SysApplication> SysApplications { get; set; }
        public DbSet<SysModules> SysModuleses { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SysUsers>().ToTable("Sys_Users");
            builder.Entity<SysRoles>().ToTable("Sys_Roles");
            //builder.Entity<SysRoleClaims>().ToTable("Sys_RoleClaims");
            //builder.Entity<SysUserClaims>().ToTable("Sys_UserClaims");
            //builder.Entity<SysUserLogins>().ToTable("Sys_UserLogins");
            //builder.Entity<SysUserRole>().ToTable("Sys_UserRole");
            //builder.Entity<SysUserTokens>().ToTable("Sys_UserToken");
            builder.AddEntityConfigurationsFromAssembly(typeof(SysUsers).GetTypeInfo().Assembly);
        }
    }
}

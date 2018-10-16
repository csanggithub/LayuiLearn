using Entitys.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.DBConfig
{
    public class MySqlDBContext : DbContext
    {
        public MySqlDBContext() : base("name=CRMDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //移除表名为复数
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //自动添加实现EntityTypeConfiguration的类
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// 用户表
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// 部门表
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// 功能表
        /// </summary>
        public DbSet<Function> Functions { get; set; }

        /// <summary>
        /// 角色表
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// 角色功能表
        /// </summary>
        public DbSet<RoleFunction> RoleFunctions { get; set; }

        /// <summary>
        /// 用户拥有功能
        /// </summary>
        public DbSet<UserFunction> UserFunctions { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <summary>
        /// 地市信息
        /// </summary>
        public DbSet<DistrictInfo> DistrictInfos { get; set; }

        /// <summary>
        /// 资源数据信息
        /// </summary>
        public DbSet<ResourceDataInfo> ResourceDataInfos { get; set; }

        /// <summary>
        /// 项目信息
        /// </summary>
        public DbSet<ProjectInfo> ProjectInfos { get; set; }

        /// <summary>
        /// 平台信息
        /// </summary>
        public DbSet<PlatformInfo> PlatformInfos { get; set; }

        /// <summary>
        /// 分类信息
        /// </summary>
        public DbSet<ClassifyInfo> ClassifyInfos { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        public DbSet<DepartmentInfo> DepartmentInfos { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace LayuiLearn.Entity.Models
{
    /// <summary>
    /// UserRole
    /// </summary>
    [TableName("userRole")]
    public class UserRole
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 最后编辑人
        /// </summary>
        public string Lmid { get; set; }

        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime Lmdt { get; set; }

        /// <summary>
        /// 停用状态
        /// 停用状态 默认0 未停用 1 停用
        /// </summary>
        public bool StopFlag { get; set; }

    }
}
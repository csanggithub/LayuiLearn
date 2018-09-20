using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace LayuiLearn.Entity.Models
{
    /// <summary>
    /// Pub_Role
    /// </summary>
    [TableName("Pub_Role")]
    public class Role
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 停用状态
        /// 停用状态 默认0  未停用 1 停用
        /// </summary>
        public bool StopFlag { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Crid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Crdt { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string Lmid { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime Lmdt { get; set; }

    }
}
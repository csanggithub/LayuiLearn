using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace LayuiLearn.Entity.Models
{
    /// <summary>
    /// Pub_RoleFunction
    /// </summary>
    [TableName("Pub_RoleFunction")]
    public class RoleFunction
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
        /// 权限编号
        /// </summary>
        public string FunctionCode { get; set; }

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
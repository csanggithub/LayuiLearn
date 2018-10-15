using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Entitys.Models
{
    /// <summary>
    /// Department
    /// </summary>
    [TableName("department")]
    public class Department
    {
        /// <summary>
        /// 部门编号
        /// </summary>
        [Key]
        public string DeptCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 上级部门编号
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 部门级别
        /// </summary>
        public int DeptLevel { get; set; }

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
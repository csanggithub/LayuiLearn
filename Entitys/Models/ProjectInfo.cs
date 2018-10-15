using Entitys.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Entitys.Models
{
    /// <summary>
    /// 项目信息表
    /// </summary>
    [TableName("projectinfo")]
    public class ProjectInfo
    {
        /// <summary>
        /// 项目信息Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目负责人
        /// </summary>
        public int ProjectPrincipal { get; set; }

        /// <summary>
        /// 归属部门Id
        /// </summary>
        public int BelongDepartmentId { get; set; }

        /// <summary>
        /// 项目状态 0-未立项 1-未启动 2-运营中 3-暂停 4-已完成 5-已结款
        /// </summary>
        public ProjectStatusEnum ProjectStatus { get; set; }

        /// <summary>
        /// 项目类型Id
        /// </summary>
        public int ProjectTypeId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public int ModifyUserId { get; set; }
    }
}

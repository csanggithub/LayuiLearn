using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Entitys.Models
{
    /// <summary>
    /// 平台信息表
    /// </summary>
    [TableName("platforminfo")]
    public class PlatformInfo
    {
        /// <summary>
        /// 平台信息Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 平台名称
        /// </summary>
        public string PlatformName { get; set; }

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

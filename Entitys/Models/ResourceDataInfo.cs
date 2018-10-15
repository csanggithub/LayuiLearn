using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Entitys.Models
{
    /// <summary>
    /// 资源数据表
    /// </summary>
    [TableName("resourcedatainfo")]
    public class ResourceDataInfo
    {
        /// <summary>
        /// 资源数据表Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        

        /// <summary>
        /// 客户姓名
        /// </summary>
        public bool CustomerName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public bool PhoneNumber { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public bool WeChat { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public bool QQ { get; set; }

        /// <summary>
        /// 省份编码
        /// </summary>
        public bool ProvinceId { get; set; }

        /// <summary>
        /// 城市编码
        /// </summary>
        public bool CityId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        public bool DepartmentId { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public bool ProjectId { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public bool PlatformId { get; set; }

        /// <summary>
        /// 分类类型
        /// </summary>
        public bool ClassifyId { get; set; }

        /// <summary>
        /// 来源链接
        /// </summary>
        public bool SourceUrl { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public bool KeyWord { get; set; }

        /// <summary>
        /// 是否下发
        /// </summary>
        public bool IsLowerHair { get; set; }

        /// <summary>
        /// 沟通状态  0未沟通  1沟通中 2二次沟通  3沟通结束
        /// </summary>
        public bool CommunicationStatus { get; set; }

        /// <summary>
        /// 沟通结果 0无效信息 1拒绝 2认可度低  3认可度中  4认可度高
        /// </summary>
        public bool CommunicationResult { get; set; }

        /// <summary>
        /// 沟通说明
        /// </summary>
        public bool CommunicationExplain { get; set; }

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

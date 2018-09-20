using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace LayuiLearn.Entity.Models
{
    /// <summary>
    /// Pub_User
    /// </summary>
    [TableName("user")]
    public class User
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 登录用户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 昵称/用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 部门编号
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 是否是管理员
        /// 是否是管理员 默认不是 0  是1
        /// </summary>
        public bool ManagerFlag { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 停用状态
        /// 停用状态 默认0 未停用 1停用
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

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LoginDate { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 区域编号
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string UserAddress { get; set; }

        /// <summary>
        /// 微信openid
        /// </summary>
        public string Wxcode { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string HeadUrl { get; set; }

    }
}
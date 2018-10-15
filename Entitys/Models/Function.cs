using System;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;

namespace Entitys.Models
{
    /// <summary>
    /// Pub_Function
    /// </summary>
    [TableName("function")]
    public class Function
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        [Key]
        public string FunctionCode { get; set; }

        /// <summary>
        /// 英文编号
        /// </summary>
        public string FunctionEnglish { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string FunctionChina { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string FunctionDescrip { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 是否为菜单
        /// 是否为菜单 1菜单 0权限
        /// </summary>
        public bool MenuFlag { get; set; }

        /// <summary>
        /// 是否停用
        /// </summary>
        public bool StopFlag { get; set; }

        /// <summary>
        /// 链接URL
        /// </summary>
        public string URLString { get; set; }

        /// <summary>
        /// 创建或修改时间
        /// </summary>
        public DateTime editdate { get; set; }

        /// <summary>
        /// 修改人
        /// 修改人 名字加工号 张三(000001)
        /// </summary>
        public string editor { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int sortidx { get; set; }

        /// <summary>
        /// navTab
        /// navTab 嵌套  _blank 新窗口 dialog 弹出窗
        /// </summary>
        public string target { get; set; }

        /// <summary>
        /// 菜单图标class
        /// </summary>
        public string MenuIcon { get; set; }

    }
}
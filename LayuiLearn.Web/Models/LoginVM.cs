using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LayuiLearn.Web.Models
{
    public class LoginVM
    {
        [Required]
        [Display(Name = "用户编号")]
        //[EmailAddress]
        public string Account { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required]
        public string ValidateCode { get; set; }

        /// <summary>
        /// 明文密码
        /// </summary>
        public string OriginalPassword { get; set; }

        /// <summary>
        /// 登录密钥
        /// </summary>
        public string LoginSecretKey { get; set; }

        /// <summary>
        /// 是否记住账号
        /// </summary>
        public bool IsRemember { get; set; }
    }
}
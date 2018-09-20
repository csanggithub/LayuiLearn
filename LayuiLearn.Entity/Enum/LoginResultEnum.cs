using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayuiLearn.Entity.Enum
{
    /// <summary>
    /// 登录结果枚举
    /// </summary>
    public enum LoginResultEnum
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        LoginSuccess,
        /// <summary>
        /// 用户不存在
        /// </summary>
        NoUser,
        /// <summary>
        /// 其他错误导致登录失败
        /// </summary>
        OtherError,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError,
        /// <summary>
        /// 密码已过期
        /// </summary>
        PasswordIsOverdue,
        /// <summary>
        /// 密码不符合复杂度要求
        /// </summary>
        PasswordIsSimple,
        /// <summary>
        /// 待激活
        /// </summary>
        Activation,
        /// <summary>
        /// 沉睡用户
        /// </summary>
        SleepingUser,
        /// <summary>
        /// 冻结用户
        /// </summary>
        LockUser,
        /// <summary>
        /// 冻结中用户
        /// </summary>
        LockUserIng,

        //操作处理
        Handle,

        /// <summary>
        /// 退出成功
        /// </summary>
        LoginOutSuccess,

    }
}

using LayuiLearn.Common;
using LayuiLearn.Entity.DTO;
using LayuiLearn.Entity.Enum;
using LayuiLearn.Entity.Models;
using LayuiLearn.IServices;
using LayuiLearn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayuiLearn.Web
{
    public class LoginUtil
    {
        private readonly IUserServices _iUserServices;

        public LoginUtil(IUserServices iUserServices)
        {
            _iUserServices = iUserServices;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">用户密码</param>
        /// <param name="msg">登录返回信息</param>
        /// <returns></returns>
        public static LoginResultEnum UserLogin(string userName, string password, out string msg)
        {
            msg = string.Empty;
            User user = null;
            //登录结果
            var loginResult = LoginResultEnum.LoginSuccess;
            try
            {
                //user = GetLoginUserInfo(userName, password);
                if (user == null)
                {
                    msg = "用户名或密码错误";
                    loginResult = LoginResultEnum.NoUser;
                    return loginResult;
                }
                //登录处理
                //LoginUserHandle(user);

                Log.Debug("系统用户鉴权", string.Format("当前用户{0}登录成功", userName));

                return loginResult;
            }
            catch (Exception ex)
            {
                LoggerLogError("UserLogin", "系统错误,无法登录", ex);
                msg = "系统错误,无法登录";
                loginResult = LoginResultEnum.OtherError;
                return loginResult;
            }
            finally
            {
                //用户登录日志
                //UserLoginCommonFacade.SaveUserLoginLog(user, loginResult, msg, userName);
            }
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="password"></param>
        /// <returns></returns>
        public  User GetLoginUserInfo(string userName, string password = "")
        {
            return _iUserServices.QueryWhere(a=>a.Account== userName&&a.UserPwd==password).FirstOrDefault();
        }
        public static void LoggerLogError(string menthodName, string message, Exception ex)
        {
            //Log.Error("LoginUtil", menthodName, AppError.EROR, string.Empty, ex, "写入登录日志失败", null);
        }
    }
}
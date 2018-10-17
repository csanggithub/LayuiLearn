using Common;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web;
using Entitys.ViewModels;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Entitys.Models;
using System.IO;
using System.Text;

namespace Web.Controllers
{
    public class AccountController : BaseAdminController
    {
        private readonly IUserServices _iUserServices;
        private readonly IFunctionServices _iFunctionServices;
        private readonly IUserFunctionServices _iUserFunctionServices;
        private readonly IDistrictInfoServices _iDistrictInfoServices;

        public AccountController(IUserServices iUserServices, IFunctionServices iFunctionServices, IUserFunctionServices iUserFunctionServices, IDistrictInfoServices iDistrictInfoServices)
        {
            _iUserServices = iUserServices;
            _iFunctionServices = iFunctionServices;
            _iUserFunctionServices = iUserFunctionServices;
            _iDistrictInfoServices = iDistrictInfoServices;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            ViewData["LoginValidateCode"] = "123qwertyuiopthnuj";
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return JavaScript("layer.msg('必填项未填写或数据格式不正确！');");
            }
            if (string.IsNullOrEmpty(model.Account))
            {
                return JavaScript("layer.msg('请输入用户名！');");
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                return JavaScript("layer.msg('请输入密码！');");
            }
            try
            {
                //var checkedPass = null != Session["Captcha"] && Session["Captcha"].ToString().ToLower() == model.ValidateCode;
                ////检验验证码
                //if (!checkedPass)
                //{
                //    return JavaScript("layer.msg('验证码失效或者错误！');changeCaptcha();");
                //}

                //解密的密码
                var pPassword = JsDes.UncMe(model.Password, model.LoginSecretKey);
                //将明文密码转化为MD5加密
                //password = CryptTools.HashPassword(pPassword);
                //string msg;
                //var loginResult = LoginUtil.UserLogin(StringSafeFilter.Filter(userName), StringSafeFilter.Filter(password.ToUpper()), out msg);

                //if (loginResult != Entity.Enum.LoginResultEnum.LoginSuccess)
                //{
                //    return Json(new { Success = false, ErrorMessage = msg }, JsonRequestBehavior.AllowGet);
                //}
                var dbUser = _iUserServices.QueryWhere(a => a.UserName == model.Account && a.UserPwd == pPassword && a.StopFlag == false).FirstOrDefault();
                if (dbUser == null)
                {
                    return JavaScript("layer.msg('用户名或密码错误！');changeCaptcha();");
                }
                //var userFunction=_iUserFunctionServices.QueryWhere(a => a.StopFlag == false && a.UserCode == dbUser.Account);
                //var function=_iFunctionServices.QueryWhere(a => a.StopFlag && userFunction.Any(b => b.StopFlag == false && b.FunctionCode == a.FunctionCode));
                //LoginUser.WriteUser(dbUser, function);
                LoginUser.WriteUser(dbUser.UserCode);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, model.Account, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, FormsAuthentication.FormsCookiePath);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
                {
                    Domain = FormsAuthentication.CookieDomain,
                    Path = ticket.CookiePath
                };
                Response.Cookies.Add(cookie);
                return JavaScript(string.Format("window.location.href='../Home/Index'"));
            }
            catch (Exception ex)
            {
                Log.Error("出现未处理异常", ex.ToString());
                return JavaScript("layer.msg('系统出错，请联系管理员！');");
            }
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["Admin"] = null;
            FormsAuthentication.SignOut();
            return JavaScript(string.Format("window.location.href='../Account/Login'"));
        }
    }
}
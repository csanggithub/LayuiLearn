using LayuiLearn.Common;
using LayuiLearn.IServices;
using LayuiLearn.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LayuiLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _iUserServices;

        public AccountController(IUserServices iUserServices)
        {
            _iUserServices = iUserServices;
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
            //var user=_iUserServices.QueryWhere(a => a.Id == 0).FirstOrDefault();
            ////_iUserServices.Add(new Entity.Models.User() {
            ////    Account = "123",
            ////    UserName="123",
            ////    IdentityNo="23",
            ////    UserPwd="123",
            ////    StopFlag=true,
            ////    ManagerFlag=true
            ////});
            //_iUserServices.Delete(user,true);
            //_iUserServices.SaverChanges();
            //Log.Error("出现未处理异常", "XXXXXXXError");
            var userName = model.Account;
            var password = model.Password; //DES加密密码
            if (string.IsNullOrEmpty(userName))
            {
                return JavaScript("layer.msg('请输入用户名！');");
            }
            if (string.IsNullOrEmpty(password))
            {
                return JavaScript("layer.msg('请输入密码！');");
            }
            var checkedPass = null != Session["Captcha"]&&Session["Captcha"].ToString().ToLower() == model.ValidateCode;
            //检验验证码
            if (!checkedPass)
            {
                return JavaScript("layer.msg('验证码失效或者错误！');changeCaptcha();");
            }

            //解密的密码
            var pPassword = JsDes.UncMe(password, model.LoginSecretKey);
            //将明文密码转化为MD5加密
            password = CryptTools.HashPassword(pPassword);
            string msg;
            //var loginResult = LoginUtil.UserLogin(StringSafeFilter.Filter(userName), StringSafeFilter.Filter(password.ToUpper()), out msg);

            //if (loginResult != Entity.Enum.LoginResultEnum.LoginSuccess)
            //{
            //    return Json(new { Success = false, ErrorMessage = msg }, JsonRequestBehavior.AllowGet);
            //}
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);



            //string captcha = vm.Captcha;
            //if (!ModelState.IsValid)
            //{
            //    return JavaScript("layer.msg('必填项未填写或数据格式不正确！');");
            //}
            //if (null != Session["Captcha"] && captcha.ToLower() != Session["Captcha"].ToString().ToLower())
            //{
            //    return JavaScript(" layer.msg('验证码不正确');changeCaptcha();");
            //}
            //var dbUser = vm;//userBLL.GetList(string.Format(" StopFlag=0 AND UserName='{0}' AND UserPwd='{1}' ",vm.UserCode, vm.Password)).FirstOrDefault();

            //if (dbUser == null)
            //{
            //    return JavaScript("layer.msg('用户名或密码错误！');changeCaptcha();");
            //}
            ////解密的密码
            //var pPassword = JsDes.UncMe(vm.Password, vm.LoginSecretKey);
            ////将明文密码转化为MD5加密
            //vm.Password=CryptTools.HashPassword(pPassword);
            ////NBCZUser.WriteUser(vm.Account);
            //FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, vm.Account, DateTime.Now, DateTime.Now.Add(FormsAuthentication.Timeout), true, FormsAuthentication.FormsCookiePath);
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            //cookie.Domain = FormsAuthentication.CookieDomain;
            //cookie.Path = ticket.CookiePath;
            //Response.Cookies.Add(cookie);
            //return JavaScript(string.Format("window.location.href='~/Home/Index'"));
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Session["Admin"] = null;
            FormsAuthentication.SignOut();
            return JavaScript(string.Format("window.location.href='../Login/Index'"));
        }
    }
}
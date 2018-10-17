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

        private void AddDis()
        {
            byte[] byData = new byte[130000];
            char[] charData = new char[130000];
            FileStream file = new FileStream("area.json", FileMode.Open);
            file.Seek(0, SeekOrigin.Begin);
            file.Read(byData, 0, 130000); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
            Decoder d = Encoding.Default.GetDecoder();
            d.GetChars(byData, 0, byData.Length, charData, 0);
            //Console.WriteLine(charData);
            var str = new string(charData); ;
            file.Close();
            var e = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
            var list = new List<DistrictInfo>();

            int j = 0;
            foreach (var i in e)
            {

                var a = (JObject)i.Value;
                list.Add(new DistrictInfo { Id = Convert.ToInt32(i.Key), DistrictName = a["name"].ToString(), Pid = 0 });
                var aqq = a["child"].ToString();
                if (!string.IsNullOrEmpty(a["child"].ToString()) && j < 31)
                {
                    var b = a["child"].ToString();
                    var aaa = JsonConvert.DeserializeObject<Dictionary<string, object>>(b);
                    foreach (var x in aaa)
                    {
                        var a1 = (JObject)x.Value;
                        list.Add(new DistrictInfo { Id = Convert.ToInt32(x.Key), DistrictName = a1["name"].ToString(), Pid = Convert.ToInt32(i.Key) });
                        if (!string.IsNullOrEmpty(a1["child"].ToString()))
                        {
                            var a3 = a1["child"].ToString();
                            var a4 = JsonConvert.DeserializeObject<Dictionary<string, string>>(a3);
                            foreach (var a5 in a4)
                            {
                                list.Add(new DistrictInfo { Id = Convert.ToInt32(a5.Key), DistrictName = a5.Value, Pid = Convert.ToInt32(x.Key) });
                            }
                        }
                    }
                }
                j++;
            }
            foreach(var m in list)
            {
                _iDistrictInfoServices.Add(m);
                _iDistrictInfoServices.SaverChanges();
            }
        }

        public ActionResult Login()
        {
            ViewData["LoginValidateCode"] = "123qwertyuiopthnuj";
            //AddDis();
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
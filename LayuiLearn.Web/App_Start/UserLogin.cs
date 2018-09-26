using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LayuiLearn.Entity.Models;
using LayuiLearn.IServices;
namespace LayuiLearn.Web
{
    public class UserLogin //: System.Web.SessionState.IRequiresSessionState
    {
        private static IUserServices _iUserServices;
        private static IFunctionServices _iFunctionServices;
        private static IUserFunctionServices _iUserFunctionServices;

        public UserLogin(IUserServices iUserServices, IFunctionServices iFunctionServices, IUserFunctionServices iUserFunctionServices)
        {
            _iUserServices = iUserServices;
            _iFunctionServices = iFunctionServices;
            _iUserFunctionServices = iUserFunctionServices;
        }

        public UserLogin() { }

        public static string UserCode
        {
            get
            {
                var user = GetPubUser();
                return user.UserCode;
            }
        }

        public static string UserName
        {
            get
            {
                var user = GetPubUser();
                return user.UserName;
            }
        }

        public static string DeptCode
        {
            get
            {
                var user = GetPubUser();
                return user.DeptCode;
            }
        }


        public static string MobilePhone
        {
            get
            {
                var user = GetPubUser();
                return user.MobilePhone;
            }
        }

        public static List<Function> UserFunctions
        {
            get
            {
                var user = GetPubUser();
                return user.UserFunctions;
            }
        }


        public static void WriteUser(string account)
        {
            LoginAdmin admin = new LoginAdmin();
            var pubUser = _iUserServices.QueryWhere(a => a.Account == account && a.StopFlag == false).FirstOrDefault();
            var context = HttpContext.Current;
            if (pubUser != null)
            {
                admin.UserCode = pubUser.Account;
                admin.UserName = pubUser.UserName;
                admin.MobilePhone = pubUser.Tel;
                admin.DeptCode = pubUser.DeptCode;
                var userFunction = _iUserFunctionServices.QueryWhere(a => a.StopFlag == false && a.UserCode == account);
                var function = _iFunctionServices.QueryWhere(a => a.StopFlag && userFunction.Any(b => b.StopFlag == false && b.FunctionCode == a.FunctionCode));
                admin.UserFunctions = function;
            }
            context.Session["Admin"] = admin;
        }


        private static LoginAdmin GetPubUser()
        {
            var context = HttpContext.Current;
            LoginAdmin admin = new LoginAdmin();
            if (context.Session["Admin"] == null)
            {
                if (!string.IsNullOrEmpty(context.User.Identity.Name))
                {
                    WriteUser(context.User.Identity.Name);
                }
            }
            admin = context.Session["Admin"] as LoginAdmin ?? (new LoginAdmin() { UserFunctions = new List<Function>() });

            return admin;
        }


        private class LoginAdmin
        {
            public string UserCode { get; set; }
            public string UserName { get; set; }

            public string DeptCode { get; set; }

            public string MobilePhone { get; set; }

            public List<Function> UserFunctions { get; set; }
        }
    }
}
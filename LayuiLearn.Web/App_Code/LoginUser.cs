using LayuiLearn.Entity.Models;
using LayuiLearn.IServices;
using LayuiLearn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayuiLearn.Web
{
    public class LoginUser
    {
        private readonly IUserServices _iUserServices;
        private readonly IFunctionServices _iFunctionServices;
        private readonly IUserFunctionServices _iUserFunctionServices;

        private static LoginUser loginUser = new LoginUser();

        public LoginUser(IUserServices iUserServices, IFunctionServices iFunctionServices, IUserFunctionServices iUserFunctionServices)
        {
            _iUserServices = DependencyResolver.Current.GetService<IUserServices>();
            _iFunctionServices = DependencyResolver.Current.GetService<IFunctionServices>(); ;
            _iUserFunctionServices = DependencyResolver.Current.GetService<IUserFunctionServices>(); ;
        }

        public LoginUser() { }

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
            var pubUser = loginUser.GetUser(account);
            var context = HttpContext.Current;
            if (pubUser != null)
            {
                admin.UserCode = pubUser.UserCode;
                admin.UserName = pubUser.UserName;
                admin.MobilePhone = pubUser.Tel;
                admin.DeptCode = pubUser.DeptCode;
                var userFunction = loginUser.GetUserFunction(pubUser.UserCode);
                var function = loginUser.GetFunction(userFunction);
                admin.UserFunctions = function;
            }
            context.Session["Admin"] = admin;
        }

        private User GetUser(string account)
        {
            var service = DependencyResolver.Current.GetService<IUserServices>();
            return service.QueryWhere(a => a.UserCode == account && a.StopFlag == false).FirstOrDefault();
        }

        private List<UserFunction> GetUserFunction(string account)
        {
            var service = DependencyResolver.Current.GetService<IUserFunctionServices>();
            return service.QueryWhere(a => a.StopFlag == false && a.UserCode == account);
        }

        private List<Function> GetFunction(List<UserFunction> userFunction)
        {
            var service = DependencyResolver.Current.GetService<IFunctionServices>();
            return service.QueryWhere(a => a.StopFlag && userFunction.Any(b => b.StopFlag == false && b.FunctionCode == a.FunctionCode));
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
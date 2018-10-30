using Entitys.Models;
using IServices;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    public class LoginUser
    {
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
            var pubUser = GetUser(account);
            var context = HttpContext.Current;
            if (pubUser != null)
            {
                admin.UserCode = pubUser.UserCode;
                admin.UserName = pubUser.UserName;
                admin.MobilePhone = pubUser.Tel;
                admin.DeptCode = pubUser.DeptCode;
                var userFunction = GetUserFunction(pubUser.UserCode);
                var function = GetFunction(userFunction);
                admin.UserFunctions = function;
            }
            context.Session["Admin"] = admin;
        }

        private static User GetUser(string account)
        {
            var service = DependencyResolver.Current.GetService<IUserServices>();
            return service.QueryWhere(a => a.UserName == account && a.StopFlag == false).FirstOrDefault();
        }

        private static List<UserFunction> GetUserFunction(string account)
        {
            var service = DependencyResolver.Current.GetService<IUserFunctionServices>();
            return service.QueryWhere(a => a.StopFlag == false && a.UserCode == account);
        }

        private static List<Function> GetFunction(List<UserFunction> userFunction)
        {
            var service = DependencyResolver.Current.GetService<IFunctionServices>();
            var function = service.QueryWhere(a => a.StopFlag == false).ToList();
            var functionCodes = function.Select(a => a.FunctionCode).ToList();
            return function.Where(a => a.StopFlag == false && functionCodes.Contains(a.FunctionCode)).ToList();
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
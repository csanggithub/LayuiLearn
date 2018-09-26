using LayuiLearn.Entity.Models;
using LayuiLearn.IServices;
using LayuiLearn.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LayuiLearn.Web
{
    public class LoginUser
    {
        private readonly IUserServices _iUserServices;
        private readonly IFunctionServices _iFunctionServices;
        private readonly IUserFunctionServices _iUserFunctionServices;

        private static LoginUser loginUser = new LoginUser();
        //private static UserServices userServices = new UserServices();
        //private static FunctionServoces functionServices = new FunctionServices();
        //private static UserFunctionServices userFunctionServices = new UserFunctionServices();

        public LoginUser(IUserServices iUserServices, IFunctionServices iFunctionServices, IUserFunctionServices iUserFunctionServices)
        {
            _iUserServices = iUserServices;
            _iFunctionServices = iFunctionServices;
            _iUserFunctionServices = iUserFunctionServices;
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
                admin.UserCode = pubUser.Account;
                admin.UserName = pubUser.UserName;
                admin.MobilePhone = pubUser.Tel;
                admin.DeptCode = pubUser.DeptCode;
                var userFunction = loginUser.GetUserFunction(account);
                var function = loginUser.GetFunction(userFunction);
                admin.UserFunctions = function;
            }
            context.Session["Admin"] = admin;
        }

        private User GetUser(string account)
        {
            return _iUserServices.QueryWhere(a => a.Account == account && a.StopFlag == false).FirstOrDefault();
        }

        private List<UserFunction> GetUserFunction(string account)
        {
            return _iUserFunctionServices.QueryWhere(a => a.StopFlag == false && a.UserCode == account);
        }

        private List<Function> GetFunction(List<UserFunction> userFunction)
        {
            return _iFunctionServices.QueryWhere(a => a.StopFlag && userFunction.Any(b => b.StopFlag == false && b.FunctionCode == a.FunctionCode));
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
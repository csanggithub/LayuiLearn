using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LayuiLearn.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult UserList()
        {
            var page =Convert.ToInt32( Request.QueryString["page"]);
            var limit = Convert.ToInt32(Request.QueryString["limit"]);
            //page = 4 & limit = 20
            var vm = new List<UserInfo>();
            for (int i = page *10+ 1; i <= page * 10 + 10; i++)
            {
                vm.Add(new UserInfo() { Id = i + 1, UserName = "张三" + i, Phone = "13725748394", City = "广州", WeiXin =  "微信", QQ = "1236253", CanalType = "58同城", ForumType = "58客服", KeyWord = "烧腊加盟", ProvinceName = "广东", DepartmentName="网络部" , ProjectName="和福记", CraateUserName="某某人", IsLowerHair="是", LowerHairTime="2018-09-29", SourceLink="http://www.mxm.com", Remark="已经处理" });
            }
            var ro = new ResultObject<UserInfo>();
            ro.code = 0;
            ro.msg = "";
            ro.count = 1000;
            ro.data = vm;
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TestLayuiPage()
        {
            return View();
        }

        public ActionResult TestLayuiPageQuery()
        {
            return View();
        }

        public ActionResult TestLayuiPageQuery2()
        {
            return View();
        }

        public ActionResult UserListInfo()
        {
            return View();
        }

        public  ActionResult TestLayuiPageList()
        {
            //var page = Convert.ToInt32(Request.QueryString["page"]);
            //var limit = Convert.ToInt32(Request.QueryString["limit"]);
            //var teid = Convert.ToInt32(Request.QueryString["teid"]);
            //var tname = Convert.ToInt32(Request.QueryString["tname"]);
            //var testqq = Convert.ToInt32(Request.QueryString["testqq"]);
            var page = Convert.ToInt32(Request.Form["page"]);
            var limit = Convert.ToInt32(Request.Form["limit"]);
            var teid = Convert.ToInt32(Request.Form["teid"]);
            var tname = Convert.ToInt32(Request.Form["tname"]);
            var testqq = Convert.ToInt32(Request.Form["testqq"]);
            var vm = new List<UserInfo>();
            for (int i = page * 10 + 1; i <= page * 10 + 10; i++)
            {
                vm.Add(new UserInfo() { Id = i + 1, UserName = "张三" + i, Phone = "13725748394", City = "广州", WeiXin = "微信", QQ = "1236253", CanalType = "58同城", ForumType = "58客服", KeyWord = "烧腊加盟", ProvinceName = "广东", DepartmentName = "网络部", ProjectName = "和福记", CraateUserName = "某某人", IsLowerHair = "是", LowerHairTime = "2018-09-29", SourceLink = "http://www.mxm.com", Remark = "已经处理" });
            }
            var ro = new ResultObject<UserInfo>();
            ro.code = 0;
            ro.msg = "";
            ro.count = 1000;
            ro.data = vm;
            return Json(ro, JsonRequestBehavior.AllowGet);
        }
    }

    public class ResultObject<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public List<T> data { get; set; }
    }

    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string WeiXin { get; set; }
        public string CanalType { get; set; }
        public string ForumType { get; set; }
        public string KeyWord { get; set; }
        public string ProvinceName { get; set; }
        public string City { get; set; }
        public string DepartmentName { get; set; }
        public string ProjectName { get; set; }
        public string CraateUserName { get; set; }
        public string IsLowerHair { get; set; }
        public string LowerHairTime { get; set; }
        public string SourceLink { get; set; }
        public string Remark { get; set; }

    }
}
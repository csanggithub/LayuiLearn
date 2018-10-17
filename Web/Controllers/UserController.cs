using Common;
using Entitys.ViewModels;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UserController : BaseAdminController
    {

        private readonly IDistrictInfoServices _iDistrictInfoServices;

        public UserController(IDistrictInfoServices iDistrictInfoServices)
        {
            _iDistrictInfoServices = iDistrictInfoServices;
        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取地区信息
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetAreaList(int areaCode = 0)
        {
            try
            {
                var districtList = _iDistrictInfoServices.QueryWhere(p => p.Pid == areaCode).ToList();
                var vm = new List<RegionInfoVM>();
                if (districtList != null && districtList.Any())
                {
                    vm = districtList.Select(p => new RegionInfoVM()
                    {
                        Pid = p.Pid,
                        RegionCode = p.Id,
                        RegionName = p.DistrictName
                    }).ToList();
                }
                return Json(vm, JsonRequestBehavior.DenyGet);
                
            }
            catch(Exception ex)
            {
                Log.Error("获取地区列表出现未处理异常", ex.ToString());
                return Json(new List<RegionInfoVM>(), JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult PCA()
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
            var deptList = new List<Dept>();
            deptList.Add(new Dept { DeptCode = "001", DeptName = "技术部" });
            deptList.Add(new Dept { DeptCode = "002", DeptName = "人事部" });
            deptList.Add(new Dept { DeptCode = "003", DeptName = "行政部" });
            deptList.Add(new Dept { DeptCode = "004", DeptName = "运维部" });
            deptList.Add(new Dept { DeptCode = "005", DeptName = "销售部" });
            deptList.Add(new Dept { DeptCode = "006", DeptName = "IT部" });
            deptList.Add(new Dept { DeptCode = "007", DeptName = "客服部" });
            var selectList = new SelectList(deptList, "DeptCode", "DeptName", "006");
            ViewBag.DepartmentSelectList = selectList;
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

    public class Dept
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
    }
}
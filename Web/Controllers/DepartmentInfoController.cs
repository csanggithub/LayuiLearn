using Common;
using Entitys.Models;
using Entitys.ViewModels;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web;

namespace Web.Controllers
{
    public class DepartmentInfoController : BaseAdminController
    {
        private readonly IUserServices _iUserServices;
        public DepartmentInfoController(IUserServices iUserServices)
        {
            _iUserServices = iUserServices;
        }
        // GET: DepartmentInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDepartmentInfoList(FormCollection form)
        {
            try
            {
                var page = Convert.ToInt32(Request.Form["page"]);//页码
                var limit = Convert.ToInt32(Request.Form["limit"]);//每页数量
                var ro = new ResponseVM<DepartmentInfo>();
                ro.ResultCode = "0";
                ro.ResultMessage = "";
                ro.IsSuccess = true;
                ro.Count = 1000;
                ro.Data = new DepartmentInfo();
                return Json(ro, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error("获取部门信息列表出现未处理异常", ex.ToString());
                return Json(new { IsSuccess = false, ErrorMessage = "获取部门信息列表失败，请稍后重试！" }, JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult EditDepartmentInfo()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error("编辑部门信息出现未处理异常", ex.ToString());
            }
            return View();
        }

        public ActionResult DelDepartmentInfo(int id)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.Error("删除部门信息出现未处理异常", ex.ToString());
            }
            return Json(new { IsSuccess = false, ErrorMessage = "删除部门信息失败，请稍后重试！" }, JsonRequestBehavior.DenyGet);
        }
    }
}
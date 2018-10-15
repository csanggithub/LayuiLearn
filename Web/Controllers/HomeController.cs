using Entitys.Models;
using Entitys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : BaseAdminController
    {
        // GET: Home
        public ActionResult Index()
        {
            var function = LoginUser.UserFunctions.Where(p => p.MenuFlag).ToList();
            List<MenuVM> model = new List<MenuVM>();
            foreach (var sub in function)
            {
                MenuVM s = new MenuVM();
                s.MenuId = sub.FunctionCode;
                s.MenuName = sub.FunctionChina;
                s.MenuIcon = string.IsNullOrWhiteSpace(sub.MenuIcon) ? "fa-list" : sub.MenuIcon;
                if (sub.ParentCode == "0")
                {
                    s.MenuIcon = string.IsNullOrWhiteSpace(sub.MenuIcon) ? "fa-cog" : sub.MenuIcon;
                }
                else
                {
                    if (CheckSubMenuList(sub.FunctionCode, function))
                    {
                        s.MenuIcon = string.IsNullOrWhiteSpace(sub.MenuIcon) ? "&#xe68e;" : sub.MenuIcon;
                    }
                }
                s.MenuHref = sub.URLString;
                s.ParentMenuId = sub.ParentCode;
                model.Add(s);
            }
            return View(model);
        }

        /// <summary>
        /// 验证是否有下级菜单
        /// </summary>
        /// <param name="FunctionCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool CheckSubMenuList(string FunctionCode, List<Function> list)
        {
            foreach (var sub in list)
            {
                if (sub.ParentCode == FunctionCode && sub.MenuFlag)
                {
                    return true;
                }
            }
            return false;
        }

        public ActionResult MyDesktop()
        {
            return View();
        }
    }
}
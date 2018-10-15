using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.ViewModels
{
    public class MenuVM
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuIcon { get; set; }
        public string MenuHref { get; set; }
        public string ParentMenuId { get; set; }
    }
}

using Bill2Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;



namespace Bill2Pay.Web
{
    public class MenuGenerator
    {
        private static Menu menu;

        public static Menu Menu
        {
            get
            {
                if (menu == null)
                {
                    menu = new Menu();
                    menu.UserName = HttpContext.Current.User.Identity.Name;
                }


                if (!menu.UserName.Equals(HttpContext.Current.User.Identity.Name, StringComparison.OrdinalIgnoreCase))
                {
                    menu = new Menu();
                    menu.UserName = HttpContext.Current.User.Identity.Name;
                }

                return menu;
            }
        }

        public static string IsActive(string controllerName, bool HasSubMenu)
        {
            string value = "";
            if (HasSubMenu)
            {
                value = "dropdown";
            }
            var currentController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            //var currentAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            if (controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase))
            {
                return "active " + value;
            }

            return "";
        }

        public static string CurrentUrl
        {
            get
            {
                var currentController = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                var currentAction = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

                var path = string.Format("/{0}/{1}/", currentController, currentAction);
                return path;
            }
        }

        public static string IsSelected(int item)
        {
            var value = "";
            var selectedItem = -1;
            if (HttpContext.Current.Request.RequestContext.RouteData.Values["ID"] != null)
            {
                selectedItem = Convert.ToInt32(HttpContext.Current.Request.RequestContext.RouteData.Values["ID"]);
            }

            if (selectedItem == item)
            {
                value = "selected";
            }
            return value;
        }

        public static string IsSelectedPayer(int item)
        {
            var value = "";
            var selectedItem = -1;
            if (HttpContext.Current.Request["payer"] != null)
            {
                selectedItem = Convert.ToInt32(HttpContext.Current.Request["payer"]);
            }

            if (selectedItem == item)
            {
                value = "selected";
            }
            return value;
        }
    }

    public class Menu
    {

        public List<MenuItem> Items { get; internal set; }
        public string UserName { get; internal set; }

        public Menu()
        {
            this.Items = new List<MenuItem>();
            SiteMapNode rootNode = SiteMap.RootNode;
            ProcessNode(this.Items, rootNode);
        }

        private void ProcessNode(List<MenuItem> items, SiteMapNode parentNode)
        {
            if (parentNode.HasChildNodes)
            {
                foreach (SiteMapNode node in parentNode.ChildNodes)
                {
                    MenuItem menuItem = new MenuItem()
                    {
                        LinkText = node.Title,
                        ActionName = node["ActionName"],
                        ControllerName = node["ControllerName"],
                        FontClass = node["FontClass"]
                    };
                    if (node.HasChildNodes)
                    {
                        menuItem.SubMenu = new List<Web.MenuItem>();
                        ProcessNode(menuItem.SubMenu, node);
                    }

                    var roles = node.Roles;
                    bool IsInRole = false;
                    foreach (var role in roles)
                    {
                        if (HttpContext.Current.User.IsInRole(role.ToString()))
                        {
                            IsInRole = true;
                            break;
                        }
                    }

                    if (IsInRole)
                    {
                        items.Add(menuItem);
                    }
                }
            }
        }
    }

    public class MenuItem
    {
        public string ActionName { get; internal set; }
        public string ControllerName { get; internal set; }
        public string FontClass { get; internal set; }
        public string LinkText { get; internal set; }
        public List<MenuItem> SubMenu { get; internal set; }
    }

    public class YearSelection
    {
        public bool AllowPostback { get; set; }
    }

    public static class TinStatus
    {
        public static string GetName(string id)
        {
            string value = "";
            if (string.IsNullOrEmpty(id))
            {
                value = "Not Verified";
                return value;
            }

            int statusId = Convert.ToInt32(id);
            var status = ApplicationDbContext.Instence.TINStatus.FirstOrDefault(p => p.Id == statusId);
            if (status != null)
            {
                value = status.Name;
            }
            else
            {
                value = "Not Verified";
            }
            return value;
        }
    }

    public static class SubmissionType
    {
        public static string GetName(int? id)
        {
            string value = "";
            if (id == null)
            {
                value = "Not Submitted";
                return value;
            }

            int statusId = Convert.ToInt32(id);
            var status = ApplicationDbContext.Instence.Status.FirstOrDefault(p => p.Id == statusId);
            if (status != null)
            {
                value = status.Name;
            }
            else
            {
                value = "Not Submitted";
            }
            return value;
        }

        public static string FormatAmount(decimal? amount)
        {
            if(amount == null)
            {
                return "0.00";
            }

            return string.Format("{0:0.00}", amount);
        }
    }
}
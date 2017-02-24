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
}
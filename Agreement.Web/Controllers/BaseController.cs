using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agreement.Web.Controllers
{
    public class BaseController : Controller
    {
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    //filterContext.ExceptionHandled = true;
        //    //// Redirect on error:
        //    //filterContext.Result = RedirectToAction("Index", "Error");
        //    //// OR set the result without redirection:
        //    //filterContext.Result = new ViewResult
        //    //{
        //    //    ViewName = "~/Views/Error/Index.cshtml"
        //    //};
        //    if (!filterContext.ExceptionHandled)
        //    {
        //        string controllerName = (string)filterContext.RouteData.Values["controller"];
        //        string actionName = (string)filterContext.RouteData.Values["action"];
        //        Exception custException = new Exception("There is some error");
        //        //Log.Error(filterContext.Exception.Message + " in " + controllerName);
        //        var model = new HandleErrorInfo(custException, controllerName, actionName);

        //        filterContext.Result = new ViewResult
        //        {
        //            ViewName = "~/Views/Error/Index.cshtml",
        //            ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
        //            TempData = filterContext.Controller.TempData
        //        };
        //        filterContext.ExceptionHandled = true;
        //    }
        //}
    }
}

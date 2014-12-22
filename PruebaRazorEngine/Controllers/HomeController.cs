using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorEngine;
using RazorEngine.Templating;

namespace PruebaRazorEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this defaultTemplate to jump-start your ASP.NET MVC application.";
            return View();
        }

        public string DynamicHtml()
        {
            try
            {
                var templateService = new TemplateService();
                templateService.AddNamespace("PruebaRazorEngine.Controllers");
                Razor.SetTemplateService(templateService);
                string result = Razor.Parse(InMemoryStorage.editViewModel.Code, new { Name = "World" });
                return result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public ActionResult ShowEditForm()
        {
            return View(InMemoryStorage.editViewModel);
        }

        public ActionResult ResetTemplate()
        {
            InMemoryStorage.SetDefaultTemplate();
            return RedirectToAction("Index");
        }

        [ValidateInput(false)]
        public ActionResult EditView(EditViewModel editModel)
        {
            InMemoryStorage.editViewModel.Code = editModel.Code;
            return RedirectToAction("Index");
        }
    }

    public static class InMemoryStorage
    {
        public const string defaultTemplate = "Hello @Model.Name, welcome to RazorEngine! Razor Dice @MyHelper.PintarPorongo()";

        public static EditViewModel editViewModel;

        public static void SetDefaultTemplate()
        {
            editViewModel.Code = defaultTemplate;
        }

        static InMemoryStorage()
        {
            editViewModel = new EditViewModel();
            SetDefaultTemplate();
        }
    }

    public static class MyHelper
    {
        public static string PintarPorongo()
        {
            return "Porongo";
        }
    }

    public class EditViewModel
    {
        public string Code { get; set; }
    }
}

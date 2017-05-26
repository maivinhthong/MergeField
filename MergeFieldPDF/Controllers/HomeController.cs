using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MergeFieldPDF.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (!fileName.EndsWith(".pdf"))
                {
                    return RedirectToAction("Index");
                }

                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                file.SaveAs(path);

                var data = PdfService.MergeFiled(path);

                var pathNew = Path.Combine(Server.MapPath("~/App_Data/uploads"), Path.GetFileNameWithoutExtension(file.FileName) + "_new.pdf");
                System.IO.File.WriteAllBytes(pathNew, data);

                return File(pathNew, "application/force-download", Path.GetFileName(pathNew));
            }

            return RedirectToAction("Index");
        }

        public FileResult Download(string ImageName)  
        {  
            var FileVirtualPath = "~/App_Data/uploads/" + ImageName;  
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));  
        }  
    }
}
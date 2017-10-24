using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TaskAsync2.Models;

namespace TaskAsync2.Controllers
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
        public ActionResult DownloadSite(UrlModel model)
        {
            Downloader.CreateNew(model);

            return PartialView(model);
        }

        public ActionResult Cancel(UrlModel model)
        {
            return PartialView(Downloader.Cancel(model));
        }

        public async Task<string> StartDownload(Guid Id)
        {
            return await Downloader.StartNew(Id);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLIENTVIDEO.ServiceReferenceVideo;

namespace CLIENTVIDEO.Controllers
{
    public class HomeController : Controller
    {
        SVideoClient model = new SVideoClient();
        //ServiceReferenceVideo.User CurrentUser = (ServiceReferenceVideo.User)(Session["UtilisateurConnecte"]);
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

        // GET: Videos
        public ActionResult videos()
        {
            Video[] videos = model.getVideos();
            return View(videos);
        }
       
    

        [HttpPost]
        public ActionResult Chearch()
        {
            string nom = Request.QueryString["nom"] ;
            Video [] videos = model.getVideosByTittle(nom);


            //RedirectToAction("");
            return View(videos);
        }
    }
}
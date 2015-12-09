using CLIENTVIDEO.ServiceReferenceVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLIENTVIDEO.Controllers
{
    public class RegisterController : Controller
    {
        SVideoClient model = new SVideoClient() ; 
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SignIn(User user=null)
        {
            if (user.email != null)
            {
                
              model.addUser(user);
              return Redirect("/Login/Index");
                
            }
            
            return RedirectToAction("Index");
        }
    }
}
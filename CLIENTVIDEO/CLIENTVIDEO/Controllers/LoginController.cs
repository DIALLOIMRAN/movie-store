using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLIENTVIDEO.ServiceReferenceVideo;
namespace CLIENTVIDEO.Controllers
{
    public class LoginController : Controller
    {

        SVideoClient model = new SVideoClient();
        
        // GET: Login
        public ActionResult Index(User user = null)
        {
            ViewBag.Title = "Connexion";
            return View(user);
        }

        [HttpPost]        
        public ActionResult Connect(User user=null)
        {
            try
            {
                if (user != null)
                {
                    User utilisateur = model.gerUser(user.email, user.password);
                    if (utilisateur != null)
                    {
                        Session["UtilisateurConnecte"] = utilisateur;
                        if (utilisateur.role == "admin")
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        return RedirectToAction("Index", "Videos");
                    }
                    else
                    {
                        return View("error");
                    }

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            
        }
        
        public ActionResult Disconnect()
        {
            Session["UtilisateurConnecte"] = null;
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
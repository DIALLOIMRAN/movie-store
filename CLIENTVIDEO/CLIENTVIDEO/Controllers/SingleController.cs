using CLIENTVIDEO.ServiceReferenceVideo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CLIENTVIDEO.Controllers
{
    public class SingleController : Controller
    {
        // GET: Single
        public ActionResult Index(int? id)
        {
            if (id.HasValue)
            {
                using (SVideoClient v = new SVideoClient())
                {
                    Video video = v.getVideo(id.Value);

                    if (video == null)
                    {
                        return View("Error");
                    }
                    return View(video);
                }
            }
            else
            {
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult commenter(int? id)
        {
            if (id.HasValue)
            {
                using (SVideoClient v = new SVideoClient())
                {
                    Video movie = v.getVideo(id.Value);

                    if (movie == null)
                    {

                        return View("Error");

                    }
                    string message = Request.Form["commentaire"];
                    User user = (User) Session["UtilisateurConnecte"];

                    Comments commentaire = new Comments();
                    commentaire.USERID = user.code;
                    commentaire.MOVIEID = movie.code;
                    commentaire.content = message;
                    commentaire.datePost = new DateTime();
                    v.addCommentaire(commentaire);
                    return RedirectToAction("index");
                }
            }
            
            //String nom = Request.QueryString("nomAuteur");
            string nom = Request.Form["nomAuteur"];
            string email = Request.Form["emailAuteur"];
            

            return RedirectToAction("Index");
        }

    }

}

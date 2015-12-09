using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLIENTVIDEO.ServiceReferenceVideo;
using System.IO;
using System.Net;
namespace CLIENTVIDEO.Controllers
{
    public class VideosController : Controller
    {
        SVideoClient dc = new SVideoClient();
        // GET: Videos
        public ActionResult Index()
        {
            Video[] videos = dc.getVideos();
            //Comments[] v=dc.getCommantaires(1);
            return View(videos);
        }

        public ActionResult Create(Video video)
        {
            video = new Video();
           
            return View(video);
        }
        
        [HttpPost]
        public ActionResult Add(Video video=null)
        {
            try
            {
                string sortie = "";
                foreach (string path in Request.Files)
                {

                    HttpPostedFileBase fileUpload = Request.Files[path];
                    if (fileUpload != null && fileUpload.ContentLength > 0)
                    {
                        ISVideo clientUpload = new SVideoClient();
                        FileInfo fileInfo = new FileInfo(fileUpload.FileName);
                        string fileName = Path.GetFileName(Request.Files[path].FileName);
                        int fileLength = Request.Files[path].ContentLength;
                        RemoteFileInfo uploadRequestInfo = new RemoteFileInfo();
                        using (FileStream stream = new FileStream(fileUpload.FileName, FileMode.Open, FileAccess.Read))
                        {
                            uploadRequestInfo.FileName = fileName;
                            uploadRequestInfo.Length = fileLength;
                            uploadRequestInfo.FileByteStream = stream;
                            clientUpload.uploader(uploadRequestInfo);
                            sortie += path + " : "+fileName + "<br/>" ;
                            
                        }
                        if (path == "data") video.data = fileName;
                        else if (path == "poster") video.poster = fileName;
                    }                   
                    
                }
                dc.addVideo(video);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }            
        }

        
        public ActionResult Edit(int? id)
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

        
        public ActionResult Delete(int? id)
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

        // POST: /Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
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
                    dc.deleteVideo(video.code);
                    return RedirectToAction("Index");
                }
            }

            return View("Error");
        }
    }
}
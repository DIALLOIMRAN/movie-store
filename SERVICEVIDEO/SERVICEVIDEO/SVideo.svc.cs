using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace SERVICEVIDEO
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "SVideo" à la fois dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez SVideo.svc ou SVideo.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SVideo : ISVideo
    {
        DBmovieEntities1 data = new DBmovieEntities1()
        {
            Configuration =
            {
                LazyLoadingEnabled = false,
                ProxyCreationEnabled = false
            }
        };
        public Video getVideo(int id)
        {
            return (from video in data.Video where video.code == id select video).Single<Video>();
        }
        public List<Video> getVideos()
        {
            return (from video in data.Video select video).ToList();
        }
        
        public List<Video> getVideosByTittle(string tittle)
        {
            return (from video in data.Video
                    where video.tittle == tittle
                    select video
                    ).ToList();
        }

        public List<Video> getVideosByCategory(string categorie)
        {
            return ( from video in data.Video 
                     where video.category == categorie 
                     select video
                    ).ToList();
        }

        public List<Video> getVideoByCode(int code)
        {
            return (from video in data.Video
                    where video.code == code
                    select video
                    ).ToList();
        }

        public string addVideo(Video video)
        {
            data.Video.Add(video);
            data.SaveChanges();
            return null;
        }

        public string deleteVideo(int code)
        {
            Video video = data.Video.Single(v => v.code == code);
            data.Video.Remove(video);
            data.SaveChanges();
            return null;
        }

        //===========================
        public void addUser(User user)
        {
            data.User.Add(user);
            data.SaveChanges();
        }
        public User gerUser(string email, string password)
        {         
            return data.User.FirstOrDefault((a => a.email == email && a.password == password));            
        }

        public bool isValidEmail(string email)
        {
            string msg = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            
            return (email == msg);
        }

        public bool isValidPassword(string password)
        {
            return false;
        }

        // ==================================

        public List <Comments> getCommantaires(int codeFilm)
        {
            return (from video in data.Video
                    from user in data.User
                    from comment in data.Comments
                    where video.code == comment.MOVIEID &&
                          user.code == comment.USERID &&
                          video.code == codeFilm
                    select comment
                   ).ToList();
        }


        public string addCommentaire(Comments commentaire)
        {
            data.Comments.Add(commentaire);
            data.SaveChanges();
            return null;
        }

        public string updateCommentaire(Comments commentaire)
        {
            Comments com = data.Comments.Single(c => c.MOVIEID == commentaire.MOVIEID &&
                                                      c.USERID == commentaire.USERID);
            com.content = commentaire.content;
            com.datePost = commentaire.datePost;
            data.SaveChanges();
            return null;
        }


        public RemoteFileInfo telecharger(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            try
            {
                // get some info about the input file
                string filePath = System.IO.Path.Combine(@"C:\Files\Uploads\", request.FileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);

                // check if exists
                if (!fileInfo.Exists) throw new System.IO.FileNotFoundException("File not found", request.FileName);

                // open stream
                System.IO.FileStream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // return result

                result.FileName = request.FileName;
                result.Length = fileInfo.Length;
                result.FileByteStream = stream;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public void uploader(RemoteFileInfo request)
        {
            FileStream targetStream = null;
            Stream sourceStream = request.FileByteStream;

            string uploadFolder = @"C:\Files\Uploads\";
            string filePath = Path.Combine(uploadFolder, request.FileName);

            using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //read from the input stream in 6K chunks
                //and save to output stream
                const int bufferLen = 65536;
                byte[] buffer = new byte[bufferLen];
                int count = 0;
                while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                {
                    targetStream.Write(buffer, 0, count);
                }
                targetStream.Close();
                sourceStream.Close();
            }

        } 
    }
}

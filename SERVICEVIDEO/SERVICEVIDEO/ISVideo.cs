using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SERVICEVIDEO
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "ISVideo" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface ISVideo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        Video getVideo(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<Video> getVideos();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tittle"></param>
        /// <returns></returns>
        [OperationContract]
        List<Video> getVideosByTittle(string tittle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categorie"></param>
        /// <returns></returns>
        [OperationContract]
        List<Video> getVideosByCategory(string categorie);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        List<Video> getVideoByCode(int code);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [OperationContract]
        string addVideo(Video video);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [OperationContract]
        string deleteVideo(int code);

        //==================================================

        [OperationContract]
        void addUser(User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [OperationContract]
        User gerUser(string email, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool isValidEmail(string email);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        bool isValidPassword(string password);


        //==================================================

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codeFilm"></param>
        /// <returns></returns>
        [OperationContract]
        List<Comments> getCommantaires(int codeFilm);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentaire"></param>
        /// <returns></returns>
        [OperationContract]
        string addCommentaire(Comments commentaire);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentaire"></param>
        /// <returns></returns>
        [OperationContract]
        string updateCommentaire(Comments commentaire);

        /// <summary>
        /// Methode permettant de télécharger une video de notre serveur
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        RemoteFileInfo telecharger(DownloadRequest request);

        /// <summary>
        /// Methode permettant à l'administrateur d'ajouter des videos dans le serveur
        /// </summary>
        /// <param name="request"></param>
        [OperationContract]
        void uploader(RemoteFileInfo request); 

    }

    [MessageContract]
    public class DownloadRequest
    {
        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class RemoteFileInfo : IDisposable
    {
        [MessageHeader(MustUnderstand = true)]
        public string FileName;

        [MessageHeader(MustUnderstand = true)]
        public long Length;

        [MessageBodyMember(Order = 1)]
        public System.IO.Stream FileByteStream;

        public void Dispose()
        {
            if (FileByteStream != null)
            {
                FileByteStream.Close();
                FileByteStream = null;
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SERVICEVIDEO
{
    using System;
    using System.Collections.Generic;
    
    public partial class Video
    {
        public Video()
        {
            this.Comments = new HashSet<Comments>();
            this.Watch = new HashSet<Watch>();
        }
    
        public int code { get; set; }
        public string tittle { get; set; }
        public string category { get; set; }
        public string describe { get; set; }
        public string poster { get; set; }
        public string data { get; set; }
        public Nullable<int> vote { get; set; }
    
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Watch> Watch { get; set; }
    }
}

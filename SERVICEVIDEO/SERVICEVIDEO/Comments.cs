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
    
    public partial class Comments
    {
        public int USERID { get; set; }
        public int MOVIEID { get; set; }
        public Nullable<System.DateTime> datePost { get; set; }
        public string content { get; set; }
    
        public virtual User User { get; set; }
        public virtual Video Video { get; set; }
    }
}

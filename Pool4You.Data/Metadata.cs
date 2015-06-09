using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pool4You.Data
{
    public partial class UmfrageMetadata
    {
        [Display(Name = "Id")]
        public int Id;

        [Display(Name = "Titel")]
        public string Title;

        [Display(Name = "Beschreibung")]
        public string Beschreibung;

        [Display(Name = "Starttermin")]
        public Nullable<System.DateTime> Start_Termin;

        [Display(Name = "Endtermin")]
        public Nullable<System.DateTime> End_Termin;

        [Display(Name = "Benutzer Id")]
        public string AspNetUsersId;

        [Display(Name = "EmaBenutzeril")]
        public AspNetUsers AspNetUsers;

        [Display(Name = "Fragen")]
        public ICollection<Frage> Frage;
    }
}
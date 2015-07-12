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

        [DataType(DataType.Date)]
        [Display(Name = "Starttermin")]
        public Nullable<System.DateTime> Start_Termin;

        [DataType(DataType.Date)]
        [Display(Name = "Endtermin")]
        public Nullable<System.DateTime> End_Termin;

        [Display(Name = "Benutzer Id")]
        public string AspNetUsersId;

        [Display(Name = "EmaBenutzeril")]
        public AspNetUsers AspNetUsers;

        [Display(Name = "Fragen")]
        public ICollection<Frage> Frage;
    }

    public partial class AntwortMetadata
    {

        [Display(Name = "Id")]
        public int Id;

        [Display(Name = "Antworttext")]
        public string Antowrttext;

        [Display(Name = "Kommentar")]
        public string Kommentar;

        [Display(Name = "Frage")]
        public int FrageId;
    }
}
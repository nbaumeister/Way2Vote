using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pool4You.Data;

namespace Pool4You.Models
{
    public class UmfrageBearbeitenViewModel
    {
        public Umfrage Umfrage { get; set; }
        public List<Votum> Vota { get; set; }
    }
}
using Pool4You.Data;
using Pool4You.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Pool4You.Controllers
{
    public class UmfragenVerwaltenController : Controller
    {

        UmfragenLogic umfrageLogic;

        public UmfragenVerwaltenController()
        {
            Entities context = new Entities();
            umfrageLogic = new UmfragenLogic(context);
        }

        // GET: UmfragenVerwalten/Create
        [Authorize]
        public ActionResult Erstellen()
        {
            
            return View(umfrageLogic.UmfrageErstellen());
        }

        // POST: UmfragenVerwalten/Create
        [HttpPost]
        [Authorize]
        public ActionResult Erstellen(Umfrage u)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                umfrageLogic.UmfrageErstellen(u, userId);

                return RedirectToAction("Index", "ZugaenglicheUmfragenAuswaehlen");
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Loeschen(int id)
        {
            try
            {
                umfrageLogic.UmfrageLoeschen(id);
                return RedirectToAction("Index", "ZugaenglicheUmfragenAuswaehlen");
            }
            catch
            {
                return RedirectToAction("Index", "ZugaenglicheUmfragenAuswaehlen");
            }
        }
    }
}

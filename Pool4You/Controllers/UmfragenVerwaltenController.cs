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

        ZugaenglicheUmfragenAuswaehlenK zugaenglicheUmfragenAuswaehlenK;

        public UmfragenVerwaltenController()
        {
            Entities context = new Entities();
            zugaenglicheUmfragenAuswaehlenK = new ZugaenglicheUmfragenAuswaehlenK(context);
        }

        // GET: UmfragenVerwalten/Create
        [Authorize]
        public ActionResult BeendeteUmfragen()
        {

            return View(zugaenglicheUmfragenAuswaehlenK.BeendeteUmfragen());
        }

        [Authorize]
        public ActionResult Anzeigen(int id)
        {
            try
            {
                var u = zugaenglicheUmfragenAuswaehlenK.UmfrageAnzeigen(id);
                if (u != null)
                {
                    return View(zugaenglicheUmfragenAuswaehlenK.UmfrageAnzeigen(id));
                }
                else
                {
                    return RedirectToAction("BeendeteUmfragen");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("BeendeteUmfragen");

            }
        }


        // GET: UmfragenVerwalten/Create
        [Authorize]
        public ActionResult Erstellen()
        {
            
            return View(zugaenglicheUmfragenAuswaehlenK.UmfrageErstellen());
        }

        // POST: UmfragenVerwalten/Create
        [HttpPost]
        [Authorize]
        public ActionResult Erstellen(Umfrage u)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                zugaenglicheUmfragenAuswaehlenK.UmfrageErstellen(u, userId);

                return RedirectToAction("PraesentiereUmfragen", "ZugaenglicheUmfragenAuswaehlen");
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
                zugaenglicheUmfragenAuswaehlenK.UmfrageLoeschen(id);
                return RedirectToAction("PraesentiereUmfragen", "ZugaenglicheUmfragenAuswaehlen");
            }
            catch
            {
                return RedirectToAction("PraesentiereUmfragen", "ZugaenglicheUmfragenAuswaehlen");
            }
        }
    }
}

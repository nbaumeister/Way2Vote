using Pool4You.Data;
using Pool4You.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Pool4You.Models;

namespace Pool4You.Controllers
{
    public class ZugaenglicheUmfragenAuswaehlenController : Controller
    {
        UmfragenLogic umfrageLogic;

        public ZugaenglicheUmfragenAuswaehlenController()
        {
            Entities context = new Entities();
            umfrageLogic = new UmfragenLogic(context);
        }

        // GET: ZugaenglicheUmfragenAuswaehlen
        [Authorize]
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var model = umfrageLogic.ZugaenglicheUmfragenAuswaehlen(userId);

            return View(model);
        }

        // GET: Teilnehmen
        [Authorize]
        public ActionResult Teilnehmen(int id)
        {
            string userId = User.Identity.GetUserId();

            var model = new UmfrageBearbeitenViewModel();
            model.Vota = umfrageLogic.VotumVeraendern(userId, id);

            return View(model);
        }

        // Post: Teilnehmen
        [HttpPost]
        [Authorize]
        public ActionResult Teilnehmen(int id, UmfrageBearbeitenViewModel vmodel)
        {
            string userId = User.Identity.GetUserId();

            umfrageLogic.VotumVeraendern(userId, id, vmodel.Vota);

            return RedirectToAction("Index");
        }
    }
}
using Pool4You.Data;
using Pool4You.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;

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
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();

            var model = umfrageLogic.ZugaenglicheUmfragenAuswaehlen(userId);

            return View(model);
        }

        // GET: Teilnehmen
        public ActionResult Teilnehmen(int id)
        {
            string userId = User.Identity.GetUserId();

            var model = umfrageLogic.VotumVeraendern(userId, id);

            return View(model);
        }

        // Post: Teilnehmen
        [HttpPost]
        public ActionResult Teilnehmen(int id, FormCollection form)
        {
            string userId = User.Identity.GetUserId();

            var model = umfrageLogic.VotumVeraendern(userId, id);

            return View(model);
        }
    }
}
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
        ZugaenglicheUmfragenAuswaehlenK ZugaenglicheUmfragenAuswaehlenK;

        public ZugaenglicheUmfragenAuswaehlenController()
        {
            Entities context = new Entities();
            ZugaenglicheUmfragenAuswaehlenK = new ZugaenglicheUmfragenAuswaehlenK(context);
        }

        // GET: ZugaenglicheUmfragenAuswaehlen
        [Authorize]
        public ActionResult PraesentiereUmfragen()
        {
            string userId = User.Identity.GetUserId();

            var model = ZugaenglicheUmfragenAuswaehlenK.GibUmfragen(userId);

            return View(model);
        }
    }
}
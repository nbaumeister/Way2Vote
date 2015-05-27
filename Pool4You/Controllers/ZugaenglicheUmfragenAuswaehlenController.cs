using Pool4You.Data;
using Pool4You.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pool4You.Controllers
{
    public class ZugaenglicheUmfragenAuswaehlenController : Controller
    {
        // GET: ZugaenglicheUmfragenAuswaehlen
        public ActionResult Index()
        {
            Entities context = new Entities();
            UmfragenLogic umfrageLogic = new UmfragenLogic(context);

            //int userId = (int)Membership.GetUser().ProviderUserKey;

            var model = umfrageLogic.ZugaenglicheUmfragenAuswaehlen(0);

            return View(model);
        }
    }
}
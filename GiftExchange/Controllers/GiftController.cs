using GiftExchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace GiftExchange.Controllers
{
    public class GiftController : Controller
    {

        // GET: Gift
        public ActionResult Index()
        {
            //get all gifts
            var gift = new GiftService().GetAllGifts();
            //pass them to the view
            return View(gift);
        }

       
    }
}

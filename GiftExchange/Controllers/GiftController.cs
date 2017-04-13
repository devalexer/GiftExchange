using GiftExchange.Models;
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
        GiftService giftService = new GiftService();

        // GET: Gift
        public ActionResult Index()
        {
            //get all gifts
            var gift = new GiftService().GetAllGifts();
            //pass them to the view
            return View(gift);
        }

        [HttpGet]
        public ActionResult AdminView()
        {
            var gift = new GiftService().GetAllGifts();
            return View(gift);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var newGift = new Gift(collection);
            giftService.CreateGift(newGift);
            return RedirectToAction("Index");
        }
        //
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var gift = giftService.GetAllGifts().First(f => f.Id == id);
            return View(gift);
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //accept & parse the input (formcollection)
            var updatedGift = new Gift
            {
                Id = id,
                Contents = collection["Contents"].ToString(),
                GiftHint = collection["GiftHint"].ToString(),
                ColorWrappingPaper = collection["ColorWrappingPaper"].ToString(),
                Height = double.Parse(collection["Height"]),
                Width = double.Parse(collection["Width"]),
                Depth = double.Parse(collection["Depth"]),
                Weight = double.Parse(collection["Weight"]),
                IsOpened = bool.Parse(collection["IsOpened"])
            };
            //save it to our database
            //redirect to the correct page
            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public ActionResult Open(int id)
        //{
        //    var gift = GiftService.GetGift(id);
        //    return View(gift);
        //}

        [HttpPost]
        public ActionResult Open(int id, FormCollection collection)
        {

            GiftService.OpenGift(id);
            return RedirectToAction("Index");
        }

        //MARK: I don't really know what I'm doing here. Will you please explain pathways I'm trying to connect?
    }

   
}

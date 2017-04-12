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


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var gift = GiftService.GetGift(id);
        //    return View(Gift);
        //}

        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    //accept & parse the input (formcollection)
        //    var updatedGift = new Gift
        //    {
        //        Id = id,
        //        Contents = collection["Contents"].ToString(),
        //        GiftHint = collection["GiftHint"].ToString(),
        //        ColorWrappingPaper = collection["ColorWrappingPaper"].ToString(),
        //        Height = double.Parse(collection["Height"]),
        //        Width = double.Parse(collection["Width"]),
        //        Depth = double.Parse(collection["Depth"]),
        //        Weight = double.Parse(collection["Weight"]),
        //        IsOpened = double.Parse(collection["IsOpened"])
        //    };
        //    //save it to our database
        //    //redirect to the correct page
        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public ActionResult Open(int id)
        //{
        //    var gift = GiftService.GetGift(id);
        //      return View(gift);
        //}

        //[HttpPost]
        //public ActionResult Open(int id)
        //{
        //    GiftService.OpenGift(id);
        //    return RedirectToAction();
        //}

    }

   
}

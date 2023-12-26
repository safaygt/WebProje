using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using B201200001_WEB;

namespace B201200001_WEB.Controllers
{
    public class SiparisController : Controller
    {
        private web_programlamaEntities db = new web_programlamaEntities();

        // GET: Siparis
        public ActionResult Index()
        {
            var siparis = db.Siparis.Include(s => s.Kullanici).Include(s => s.Magaza).Include(s => s.Oyuncak);
            return View(siparis.ToList());
        }

        public ActionResult AddToCart(int ID)
        {

            return View();
        }

        public ActionResult GetCart()
        {

            return View();
        }


        public ActionResult RemoveFromCard(int ID)
        {
            return View();
        }

    }
}


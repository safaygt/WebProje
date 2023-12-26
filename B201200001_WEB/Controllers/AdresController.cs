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
    public class AdresController : Controller
    {
        private web_programlamaEntities db = new web_programlamaEntities();

        // GET: Adres
        public ActionResult Index()
        {
            var adres = db.Adres.Include(a => a.ilce).Include(a => a.Kullanici).Include(a => a.Mahalle).Include(a => a.Sehir).Include(a => a.Sokak);
            return View(adres.ToList());
        }

        // GET: Adres/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = db.Adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }

        // GET: Adres/Create
        public ActionResult Create()
        {

            ViewBag.Sehir_ID = new SelectList(db.Sehir, "Sehir_ID", "Sehir_Ad");
            ViewBag.ilce_ID = new SelectList(db.ilce, "Sehir_ID", "ilce_Ad");
            ViewBag.Mahalle_ID = new SelectList(db.Mahalle, "ilce_ID", "Mahalle_Ad");
            ViewBag.Sokak_ID = new SelectList(db.Sokak, "Mahalle_ID", "Sokak_Ad");
            ViewBag.Kullanici_ID = new SelectList(db.Kullanici, "Kullanici_ID", "Kullanici_Ad");
            return View();
        }

        public JsonResult ilceGetir(int p)
        {
            var ilceler = (from x in db.ilce
                           join y in db.Sehir on x.Sehir.Sehir_ID equals y.Sehir_ID
                           where x.Sehir.Sehir_ID == p
                           select new
                           {
                               text = x.ilce_Ad,
                               value = x.ilce_ID.ToString()
                           }).ToList();
            
            return Json(ilceler,JsonRequestBehavior.AllowGet);
        }



        // POST: Adres/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Adres_ID,Sehir_ID,ilce_ID,Mahalle_ID,Sokak_ID,Bina_No,Kullanici_ID")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                db.Adres.Add(adres);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ilce_ID = new SelectList(db.ilce, "ilce_ID", "ilce_Ad", adres.ilce_ID);
            ViewBag.Kullanici_ID = new SelectList(db.Kullanici, "Kullanici_ID", "Kullanici_Ad", adres.Kullanici_ID);
            ViewBag.Mahalle_ID = new SelectList(db.Mahalle, "Mahalle_ID", "Mahalle_Ad", adres.Mahalle_ID);
            ViewBag.Sehir_ID = new SelectList(db.Sehir, "Sehir_ID", "Sehir_Ad", adres.Sehir_ID);
            ViewBag.Sokak_ID = new SelectList(db.Sokak, "Sokak_ID", "Sokak_Ad", adres.Sokak_ID);
            return View(adres);
        }

        // GET: Adres/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = db.Adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            ViewBag.ilce_ID = new SelectList(db.ilce, "ilce_ID", "ilce_Ad", adres.ilce_ID);
            ViewBag.Kullanici_ID = new SelectList(db.Kullanici, "Kullanici_ID", "Kullanici_Ad", adres.Kullanici_ID);
            ViewBag.Mahalle_ID = new SelectList(db.Mahalle, "Mahalle_ID", "Mahalle_Ad", adres.Mahalle_ID);
            ViewBag.Sehir_ID = new SelectList(db.Sehir, "Sehir_ID", "Sehir_Ad", adres.Sehir_ID);
            ViewBag.Sokak_ID = new SelectList(db.Sokak, "Sokak_ID", "Sokak_Ad", adres.Sokak_ID);
            return View(adres);
        }

        // POST: Adres/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Adres_ID,Sehir_ID,ilce_ID,Mahalle_ID,Sokak_ID,Bina_No,Kullanici_ID")] Adres adres)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adres).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ilce_ID = new SelectList(db.ilce, "ilce_ID", "ilce_Ad", adres.ilce_ID);
            ViewBag.Kullanici_ID = new SelectList(db.Kullanici, "Kullanici_ID", "Kullanici_Ad", adres.Kullanici_ID);
            ViewBag.Mahalle_ID = new SelectList(db.Mahalle, "Mahalle_ID", "Mahalle_Ad", adres.Mahalle_ID);
            ViewBag.Sehir_ID = new SelectList(db.Sehir, "Sehir_ID", "Sehir_Ad", adres.Sehir_ID);
            ViewBag.Sokak_ID = new SelectList(db.Sokak, "Sokak_ID", "Sokak_Ad", adres.Sokak_ID);
            return View(adres);
        }

        // GET: Adres/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adres adres = db.Adres.Find(id);
            if (adres == null)
            {
                return HttpNotFound();
            }
            return View(adres);
        }

        // POST: Adres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adres adres = db.Adres.Find(id);
            db.Adres.Remove(adres);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

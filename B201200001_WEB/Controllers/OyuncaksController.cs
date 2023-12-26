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
    public class OyuncaksController : Controller
    {
        private web_programlamaEntities db = new web_programlamaEntities();

        // GET: Oyuncaks
        public ActionResult Index()
        {
            var oyuncak = db.Oyuncak.Include(o => o.Magaza);
            return View(oyuncak.ToList());
        }

        

        // GET: Oyuncaks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oyuncak oyuncak = db.Oyuncak.Find(id);
            if (oyuncak == null)
            {
                return HttpNotFound();
            }
            return View(oyuncak);
        }

        // GET: Oyuncaks/Create
        public ActionResult Create()
        {
            ViewBag.Magaza_ID = new SelectList(db.Magaza, "Magaza_ID", "Magaza_Ad");
            return View();
        }

        // POST: Oyuncaks/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Oyuncak_ID,Oyuncak_Tur,Oyuncak_Agirlik,Oyuncak_Boy,Oyuncak_Renk,Siparis_ID,Magaza_ID")] Oyuncak oyuncak)
        {
            if (ModelState.IsValid)
            {
                db.Oyuncak.Add(oyuncak);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Magaza_ID = new SelectList(db.Magaza, "Magaza_ID", "Magaza_Ad", oyuncak.Magaza_ID);
            return View(oyuncak);
        }

        // GET: Oyuncaks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oyuncak oyuncak = db.Oyuncak.Find(id);
            if (oyuncak == null)
            {
                return HttpNotFound();
            }
            ViewBag.Magaza_ID = new SelectList(db.Magaza, "Magaza_ID", "Magaza_Ad", oyuncak.Magaza_ID);
            return View(oyuncak);
        }

        // POST: Oyuncaks/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Oyuncak_ID,Oyuncak_Tur,Oyuncak_Agirlik,Oyuncak_Boy,Oyuncak_Renk,Siparis_ID,Magaza_ID")] Oyuncak oyuncak)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oyuncak).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Magaza_ID = new SelectList(db.Magaza, "Magaza_ID", "Magaza_Ad", oyuncak.Magaza_ID);
            return View(oyuncak);
        }

        // GET: Oyuncaks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oyuncak oyuncak = db.Oyuncak.Find(id);
            if (oyuncak == null)
            {
                return HttpNotFound();
            }
            return View(oyuncak);
        }

        // POST: Oyuncaks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oyuncak oyuncak = db.Oyuncak.Find(id);
            db.Oyuncak.Remove(oyuncak);
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

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Futebol.Models;

namespace Futebol.Controllers
{
    public class ComicaoTecnicasController : Controller
    {
        private FutebolDBContext db = new FutebolDBContext();

        // GET: ComicaoTecnicas
        public ActionResult Index()
        {
            var comicaoTecnicas = db.ComicaoTecnicas.Include(c => c.Time);
            return View(comicaoTecnicas.ToList());
        }

        // GET: ComicaoTecnicas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicaoTecnica comicaoTecnica = db.ComicaoTecnicas.Find(id);
            if (comicaoTecnica == null)
            {
                return HttpNotFound();
            }
            return View(comicaoTecnica);
        }

        // GET: ComicaoTecnicas/Create
        public ActionResult Create()
        {
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime");
            return View();
        }

        // POST: ComicaoTecnicas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeDaComicao,TimeID")] ComicaoTecnica comicaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.ComicaoTecnicas.Add(comicaoTecnica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comicaoTecnica.TimeID);
            return View(comicaoTecnica);
        }

        // GET: ComicaoTecnicas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicaoTecnica comicaoTecnica = db.ComicaoTecnicas.Find(id);
            if (comicaoTecnica == null)
            {
                return HttpNotFound();
            }
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comicaoTecnica.TimeID);
            return View(comicaoTecnica);
        }

        // POST: ComicaoTecnicas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeDaComicao,TimeID")] ComicaoTecnica comicaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comicaoTecnica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comicaoTecnica.TimeID);
            return View(comicaoTecnica);
        }

        // GET: ComicaoTecnicas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ComicaoTecnica comicaoTecnica = db.ComicaoTecnicas.Find(id);
            if (comicaoTecnica == null)
            {
                return HttpNotFound();
            }
            return View(comicaoTecnica);
        }

        // POST: ComicaoTecnicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComicaoTecnica comicaoTecnica = db.ComicaoTecnicas.Find(id);
            db.ComicaoTecnicas.Remove(comicaoTecnica);
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

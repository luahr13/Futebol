using Futebol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace Futebol.Controllers
{
    public class JogadorController : Controller
    {
        private FutebolDBContext db = new FutebolDBContext();

        // GET: Jogador
        public ActionResult Index()
        {
            var jogadores = db.Jogadores.Include(j => j.Time).ToList();
            return View(jogadores);
        }

        // GET: Jogador/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jogador jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // GET: Jogador/Create
        public ActionResult Create()
        {
            // Preenche a lista de times para o dropdown
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime");
            return View();
        }

        // POST: Jogador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Jogadores.Add(jogador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Preenche o dropdown novamente se ocorrer erro de validação
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", jogador.TimeID);
            return View(jogador);
        }

        // GET: Jogador/Edit/id
        public ActionResult Edit(int id)
        {
            var jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }

            // Preencher o ViewBag com a lista de times
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", jogador.TimeID);
            return View(jogador);
        }

        // POST: Jogador/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jogador jogador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogador).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jogador);
        }

        // GET: Jogador/Delete/id
        public ActionResult Delete(int id)
        {
            var jogador = db.Jogadores.Find(id);
            if (jogador == null)
            {
                return HttpNotFound();
            }
            return View(jogador);
        }

        // POST: Jogador/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var jogador = db.Jogadores.Find(id);
            db.Jogadores.Remove(jogador);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
using System;
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
    public class ComissaoTecnicaController : Controller
    {
        private FutebolDBContext db = new FutebolDBContext();

        // GET: ComicaoTecnicas
        public ActionResult Index()
        {
            var comissaoTecnicas = db.ComissaoTecnica.Include(c => c.Time);
            return View(comissaoTecnicas.ToList());
        }

        // GET: ComicaoTecnicas/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comissaoTecnica = db.ComissaoTecnica
                .Include(c => c.Time) // Carrega explicitamente o Time relacionado
                .FirstOrDefault(c => c.ID == id);

            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }

            return View(comissaoTecnica);
        }

        // GET: ComicaoTecnicas/Create
        public ActionResult Create()
        {
            // Preenche as opções dos Times
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime");
            // Preenche as opções do enum Cargo
            ViewBag.Cargos = Enum.GetValues(typeof(Cargo)).Cast<Cargo>();
            return View();
        }

        // POST: ComicaoTecnicas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NomeDaComicao,TimeID,Cargo,DataDeNascimento")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.ComissaoTecnica.Add(comissaoTecnica);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comissaoTecnica.TimeID);
            ViewBag.Cargos = Enum.GetValues(typeof(Cargo)).Cast<Cargo>();
            return View(comissaoTecnica);
        }

        // GET: ComicaoTecnicas/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comissaoTecnica = db.ComissaoTecnica
                .Include(c => c.Time) // Carrega explicitamente o Time relacionado
                .FirstOrDefault(c => c.ID == id);

            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }

            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comissaoTecnica.TimeID);
            ViewBag.Cargos = Enum.GetValues(typeof(Cargo)).Cast<Cargo>();
            return View(comissaoTecnica);
        }

        // POST: ComicaoTecnicas/Edit/id
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NomeDaComicao,TimeID,Cargo,DataDeNascimento")] ComissaoTecnica comissaoTecnica)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comissaoTecnica).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", comissaoTecnica.TimeID);
            ViewBag.Cargos = Enum.GetValues(typeof(Cargo)).Cast<Cargo>();
            return View(comissaoTecnica);
        }

        // GET: ComicaoTecnicas/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comissaoTecnica = db.ComissaoTecnica
                .Include(c => c.Time) // Carrega explicitamente o Time relacionado
                .FirstOrDefault(c => c.ID == id);

            if (comissaoTecnica == null)
            {
                return HttpNotFound();
            }

            return View(comissaoTecnica);
        }

        // POST: ComicaoTecnicas/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ComissaoTecnica comissaoTecnica = db.ComissaoTecnica.Find(id);
            db.ComissaoTecnica.Remove(comissaoTecnica);
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

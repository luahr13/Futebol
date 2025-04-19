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
    public class PartidasController : Controller
    {
        private FutebolDBContext db = new FutebolDBContext();

        // GET: Partidas
        public ActionResult Index()
        {
            var partidas = db.Partidas.Include(p => p.TimeCasa).Include(p => p.TimeVisitante);
            return View(partidas.ToList());
        }

        // GET: Partidas/Details/id
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var partidas = db.Partidas
                .Include(p => p.TimeCasa)       // Carrega o Time da Casa
                .Include(p => p.TimeVisitante) // Carrega o Time Visitante
                .FirstOrDefault(p => p.ID == id);

            if (partidas == null)
            {
                return HttpNotFound();
            }

            return View(partidas);
        }

        // GET: Partidas/Create
        public ActionResult Create()
        {
            ViewBag.TimeCasaID = new SelectList(db.Times, "ID", "NomeDoTime");
            ViewBag.TimeVisitanteID = new SelectList(db.Times, "ID", "NomeDoTime");
            // Obter a lista de estádios únicos cadastrados nos times
            var estadios = db.Times.Select(t => t.Estadio).Distinct().ToList();
            ViewBag.Estadios = new SelectList(estadios);

            return View();
        }

        // POST: Partidas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Partidas partida)
        {
            if (partida.TimeCasaID == partida.TimeVisitanteID)
            {
                ModelState.AddModelError("", "O Time da Casa e o Time Visitante não podem ser iguais.");
            }

            if (ModelState.IsValid)
            {
                db.Partidas.Add(partida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Recarregar a lista de times para popular os dropdowns na view
            ViewBag.TimeCasaID = new SelectList(db.Times, "ID", "NomeDoTime", partida.TimeCasaID);
            ViewBag.TimeVisitanteID = new SelectList(db.Times, "ID", "NomeDoTime", partida.TimeVisitanteID);
            // Obter a lista de estádios únicos cadastrados nos times
            ViewBag.Estadios = new SelectList(db.Times.Select(t => t.Estadio).Distinct().ToList(), partida.Estadio);

            return View(partida);
        }

        // GET: Partidas/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var partidas = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeVisitante)
                .FirstOrDefault(p => p.ID == id);

            if (partidas == null)
            {
                return HttpNotFound();
            }

            ViewBag.TimeCasaID = new SelectList(db.Times, "ID", "NomeDoTime", partidas.TimeCasaID);
            ViewBag.TimeVisitanteID = new SelectList(db.Times, "ID", "NomeDoTime", partidas.TimeVisitanteID);
            // Preencha a lista de estádios com os valores disponíveis
            ViewBag.Estadios = new SelectList(db.Times.Select(t => t.Estadio).Distinct().ToList(), partidas.Estadio);
            return View(partidas);
        }

        // POST: Partidas/Edit/id
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,TimeCasaID,TimeVisitanteID, Estadio")] Partidas partidas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partidas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TimeCasaID = new SelectList(db.Times, "ID", "NomeDoTime", partidas.TimeCasaID);
            ViewBag.TimeVisitanteID = new SelectList(db.Times, "ID", "NomeDoTime", partidas.TimeVisitanteID);
            // Obter a lista de estádios únicos cadastrados nos times
            ViewBag.Estadios = new SelectList(db.Times.Select(t => t.Estadio).Distinct().ToList(), partidas.Estadio);
            return View(partidas);
        }

        // GET: Partidas/Delete/id
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var partidas = db.Partidas
                .Include(p => p.TimeCasa)
                .Include(p => p.TimeVisitante)
                .FirstOrDefault(p => p.ID == id);

            if (partidas == null)
            {
                return HttpNotFound();
            }

            return View(partidas);
        }

        // POST: Partidas/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Partidas partidas = db.Partidas.Find(id);
            db.Partidas.Remove(partidas);
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

        public ActionResult AddEstatistica(int partidaId)
        {
            var partida = db.Partidas.Include(p => p.TimeCasa).Include(p => p.TimeVisitante).FirstOrDefault(p => p.ID == partidaId);

            if (partida == null)
            {
                return HttpNotFound();
            }

            ViewBag.PartidaID = partidaId;
            ViewBag.Times = new SelectList(new[] { partida.TimeCasa, partida.TimeVisitante }, "ID", "NomeDoTime");
            ViewBag.Jogadores = new SelectList(db.Jogadores.Where(j => j.TimeID == partida.TimeCasaID || j.TimeID == partida.TimeVisitanteID), "ID", "NomeDoJogador");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEstatistica(EstatisticasPartida estatistica)
        {
            if (ModelState.IsValid)
            {
                estatistica.SequenciaGol = db.EstatisticasPartidas.Count(e => e.PartidaID == estatistica.PartidaID) + 1;

                db.EstatisticasPartidas.Add(estatistica);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = estatistica.PartidaID });
            }

            var partida = db.Partidas.Include(p => p.TimeCasa).Include(p => p.TimeVisitante).FirstOrDefault(p => p.ID == estatistica.PartidaID);
            ViewBag.Times = new SelectList(new[] { partida.TimeCasa, partida.TimeVisitante }, "ID", "NomeDoTime");
            ViewBag.Jogadores = new SelectList(db.Jogadores.Where(j => j.TimeID == partida.TimeCasaID || j.TimeID == partida.TimeVisitanteID), "ID", "NomeDoJogador");

            return View(estatistica);
        }
    }
}

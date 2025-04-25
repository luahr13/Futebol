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
    public class EstatisticasPartidasController : Controller
    {
        private FutebolDBContext db = new FutebolDBContext();

        // GET: EstatisticasPartidas
        public ActionResult Index()
        {
            var estatisticasPartidas = db.EstatisticasPartidas.Include(e => e.Jogador).Include(e => e.Partida).Include(e => e.Time);
            return View(estatisticasPartidas.ToList());
        }

        // GET: EstatisticasPartidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatisticasPartida estatisticasPartida = db.EstatisticasPartidas.Find(id);
            if (estatisticasPartida == null)
            {
                return HttpNotFound();
            }
            return View(estatisticasPartida);
        }

        public JsonResult GetTimesDaPartida(int partidaId)
        {
            var partida = db.Partidas.Include(p => p.TimeCasa).Include(p => p.TimeVisitante).FirstOrDefault(p => p.ID == partidaId);
            var times = new List<Time> { partida.TimeCasa, partida.TimeVisitante };
            return Json(times.Select(t => new { t.ID, t.NomeDoTime }), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJogadoresDoTime(int timeId)
        {
            var jogadores = db.Jogadores.Where(j => j.TimeID == timeId).ToList();
            return Json(jogadores.Select(j => new { j.ID, j.NomeDoJogador }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddGol(int partidaId)
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
        public ActionResult AddGol(EstatisticasPartida estatisticasPartida)
        {
            if (ModelState.IsValid)
            {
                // Definir o número do gol automaticamente
                estatisticasPartida.SequenciaGol = db.EstatisticasPartidas.Count(r => r.PartidaID == estatisticasPartida.PartidaID) + 1;

                db.EstatisticasPartidas.Add(estatisticasPartida);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = estatisticasPartida.PartidaID });
            }

            var partida = db.Partidas.Include(p => p.TimeCasa).Include(p => p.TimeVisitante).FirstOrDefault(p => p.ID == estatisticasPartida.PartidaID);
            ViewBag.Times = new SelectList(new[] { partida.TimeCasa, partida.TimeVisitante }, "ID", "NomeDoTime");
            ViewBag.Jogadores = new SelectList(db.Jogadores.Where(j => j.TimeID == partida.TimeCasaID || j.TimeID == partida.TimeVisitanteID), "ID", "NomeDoJogador");

            return View(estatisticasPartida);
        }

        // GET: EstatisticasPartidas/Create
        public ActionResult Create()
        {
            ViewBag.PartidaID = new SelectList(db.Partidas, "ID", "NomeDaPartida");

            // Apenas os times que participam da partida selecionada serão carregados via JS
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime");

            // Apenas jogadores do time selecionado serão carregados via JS
            ViewBag.JogadorID = new SelectList(db.Jogadores, "ID", "NomeDoJogador");

            return View();
        }

        // POST: EstatisticasPartidas/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PartidaID,TimeID,JogadorID,SequenciaGol")] EstatisticasPartida estatisticasPartida)
        {
            if (ModelState.IsValid)
            {
                db.EstatisticasPartidas.Add(estatisticasPartida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JogadorID = new SelectList(db.Jogadores, "ID", "NomeDoJogador", estatisticasPartida.JogadorID);
            ViewBag.PartidaID = new SelectList(db.Partidas, "ID", "NomeDaPartida", estatisticasPartida.PartidaID);
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", estatisticasPartida.TimeID);
            return View(estatisticasPartida);
        }

        // GET: EstatisticasPartidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatisticasPartida estatisticasPartida = db.EstatisticasPartidas.Find(id);
            if (estatisticasPartida == null)
            {
                return HttpNotFound();
            }
            ViewBag.JogadorID = new SelectList(db.Jogadores, "ID", "NomeDoJogador", estatisticasPartida.JogadorID);
            ViewBag.PartidaID = new SelectList(db.Partidas, "ID", "NomeDaPartida", estatisticasPartida.PartidaID);
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", estatisticasPartida.TimeID);
            return View(estatisticasPartida);
        }

        // POST: EstatisticasPartidas/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PartidaID,TimeID,JogadorID,SequenciaGol")] EstatisticasPartida estatisticasPartida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estatisticasPartida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JogadorID = new SelectList(db.Jogadores, "ID", "NomeDoJogador", estatisticasPartida.JogadorID);
            ViewBag.PartidaID = new SelectList(db.Partidas, "ID", "NomeDaPartida", estatisticasPartida.PartidaID);
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", estatisticasPartida.TimeID);
            return View(estatisticasPartida);
        }

        // GET: EstatisticasPartidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstatisticasPartida estatisticasPartida = db.EstatisticasPartidas.Find(id);
            if (estatisticasPartida == null)
            {
                return HttpNotFound();
            }
            return View(estatisticasPartida);
        }

        // POST: EstatisticasPartidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstatisticasPartida estatisticasPartida = db.EstatisticasPartidas.Find(id);
            db.EstatisticasPartidas.Remove(estatisticasPartida);
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

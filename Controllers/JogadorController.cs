﻿using Futebol.Models;
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
        public ActionResult Index(string searchString)
        {
            ViewBag.CurrentFilter = searchString;
            var jogadores = db.Jogadores.Include(j => j.Time); // Carrega os jogadores com seu relacionamento Time

            // Se o parâmetro de pesquisa for informado, aplica o filtro
            if (!String.IsNullOrEmpty(searchString))
            {
                jogadores = jogadores.Where(j => j.NomeDoJogador.Contains(searchString));
            }

            return View(jogadores.ToList()); // Retorna os jogadores (filtrados ou não)
        }

        // GET: Jogador/Details/id
        public ActionResult Details(int id)
        {
            var jogador = db.Jogadores
                .Include(j => j.Time) // Carrega explicitamente o Time relacionado
                .FirstOrDefault(j => j.ID == id);

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
            // Preenche as opções do enum Posicao
            ViewBag.Posicoes = Enum.GetValues(typeof(Posicao)).Cast<Posicao>();
            // Preenche as opções do enum PeDominante
            ViewBag.PesDominantes = Enum.GetValues(typeof(PeDominante)).Cast<PeDominante>();

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
            ViewBag.Posicoes = Enum.GetValues(typeof(Posicao)).Cast<Posicao>();
            ViewBag.PesDominantes = Enum.GetValues(typeof(PeDominante)).Cast<PeDominante>();

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
            // Preenche as opções do enum Posicao
            ViewBag.Posicoes = Enum.GetValues(typeof(Posicao)).Cast<Posicao>();
            // Preenche as opções do enum PeDominante
            ViewBag.PesDominantes = Enum.GetValues(typeof(PeDominante)).Cast<PeDominante>();

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

            // Preenche o dropdown de times
            ViewBag.TimeID = new SelectList(db.Times, "ID", "NomeDoTime", jogador.TimeID);
            // Preenche as opções do enum Posicao
            ViewBag.Posicoes = Enum.GetValues(typeof(Posicao)).Cast<Posicao>();
            // Preenche as opções do enum PeDominante
            ViewBag.PesDominantes = Enum.GetValues(typeof(PeDominante)).Cast<PeDominante>();

            return View(jogador);
        }

        // GET: Jogador/Delete/id
        public ActionResult Delete(int id)
        {
            var jogador = db.Jogadores
                .Include(j => j.Time) // Carrega explicitamente o relacionamento com Time
                .FirstOrDefault(j => j.ID == id);

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
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SharedModels.Models;

namespace NameLess.Controllers
{
    [Authorize]
    public class PesquisasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pesquisas
        public ActionResult DashBoard()
        {
            List<DashBoard> pesquisa = db.Pesquisas.Join(db.Tags,
                pes => pes.TagId,
                tag => tag.TagId,
                (pes, tag) => new DashBoard
                {
                    DescricaoTag = tag.DescricaoTag,
                    TermoPesquisado = pes.TermoPesquisado,
                    DataPesquisa = pes.DataPesquisa
                }).Take(10).ToList();

            return View(pesquisa);
        }

        // GET: Pesquisas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesquisas pesquisas = db.Pesquisas.Find(id);
            if (pesquisas == null)
            {
                return HttpNotFound();
            }
            return View(pesquisas);
        }

        // GET: Pesquisas/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "NomeCliente");
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag");
            return View();
        }

        // POST: Pesquisas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PesquisaId,TermoPesquisado,Localizacao,DataPesquisa,ClienteId,TagId")] Pesquisas pesquisas)
        {
            if (ModelState.IsValid)
            {
                db.Pesquisas.Add(pesquisas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "NomeCliente", pesquisas.ClienteId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", pesquisas.TagId);
            return View(pesquisas);
        }

        // GET: Pesquisas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesquisas pesquisas = db.Pesquisas.Find(id);
            if (pesquisas == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "NomeCliente", pesquisas.ClienteId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", pesquisas.TagId);
            return View(pesquisas);
        }

        // POST: Pesquisas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PesquisaId,TermoPesquisado,Localizacao,DataPesquisa,ClienteId,TagId")] Pesquisas pesquisas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pesquisas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "NomeCliente", pesquisas.ClienteId);
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", pesquisas.TagId);
            return View(pesquisas);
        }

        // GET: Pesquisas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pesquisas pesquisas = db.Pesquisas.Find(id);
            if (pesquisas == null)
            {
                return HttpNotFound();
            }
            return View(pesquisas);
        }

        // POST: Pesquisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pesquisas pesquisas = db.Pesquisas.Find(id);
            db.Pesquisas.Remove(pesquisas);
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

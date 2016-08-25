using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SharedModels.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Reflection;
using System.Configuration;
using System.Text;

namespace NameLess.Controllers
{
    [Authorize]
    public class CamposPesquisasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CamposPesquisas
        public ActionResult Index()
        {
            var camposPesquisa = db.CamposPesquisa.Include(c => c.Tags);
            return View(camposPesquisa.ToList());
        }

        // GET: CamposPesquisas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CamposPesquisa camposPesquisa = db.CamposPesquisa.Find(id);
            if (camposPesquisa == null)
            {
                return HttpNotFound();
            }
            return View(camposPesquisa);
        }

        // GET: CamposPesquisas/Create
        public ActionResult Create()
        {
            MontagemViewBag();
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag");
            return View();
        }

        // POST: CamposPesquisas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CamposPesquisa camposPesquisa)
        {
            camposPesquisa.UsuarioId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.CamposPesquisa.Add(camposPesquisa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", camposPesquisa.TagId);
            return View(camposPesquisa);
        }

        // GET: CamposPesquisas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CamposPesquisa camposPesquisa = db.CamposPesquisa.Find(id);
            if (camposPesquisa == null)
            {
                return HttpNotFound();
            }
            MontagemViewBag();
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", camposPesquisa.TagId);
            return View(camposPesquisa);
        }

        // POST: CamposPesquisas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CamposPesquisaId,TagId,IdCampo,TipoCampo")] CamposPesquisa camposPesquisa)
        {
            camposPesquisa.UsuarioId = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                db.Entry(camposPesquisa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TagId = new SelectList(db.Tags, "TagId", "Tag", camposPesquisa.TagId);
            return View(camposPesquisa);
        }

        // GET: CamposPesquisas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CamposPesquisa camposPesquisa = db.CamposPesquisa.Find(id);
            if (camposPesquisa == null)
            {
                return HttpNotFound();
            }
            return View(camposPesquisa);
        }

        // POST: CamposPesquisas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CamposPesquisa camposPesquisa = db.CamposPesquisa.Find(id);
            db.CamposPesquisa.Remove(camposPesquisa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileStreamResult Script(int id)
        {
            StreamReader a = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(@"NameLess.TemplateScript.ScriptById.js"));
            String script = a.ReadToEnd();

            var dadosscript = db.CamposPesquisa.Join(db.Users,
                  x => x.UsuarioId,
                  y => y.Id,
                  (x, y) => new { CamposPesquisaId = x.CamposPesquisaId, ClienteId = x.Usuario.ClienteId, TagId = x.TagId, IdCampoTexto = x.IdCampoTexto, IdCampoBotao = x.IdCampoBotao })
                  .Where(x => x.CamposPesquisaId == id).FirstOrDefault();

            script = script.Replace("{IdCampoBotao}", dadosscript.IdCampoBotao);
            script = script.Replace("{IdCampoTexto}", dadosscript.IdCampoTexto);
            script = script.Replace("{ClienteId}", dadosscript.ClienteId.ToString());
            script = script.Replace("{TagId}", dadosscript.TagId.ToString());
            script = script.Replace("{UrlApi}", ConfigurationManager.AppSettings["UrlApi"].ToString());

            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(script));
            return File(stream, "text/javascript", "NameLess.js");
        }

        #region Métodos Genéricos
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void MontagemViewBag()
        {
            ViewBag.TipoCampo = new SelectList(new CamposPesquisa().RetornaTipoCampos(), "TipoCampoId", "Nome");
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SharedModels.Models;
using Newtonsoft.Json;
using System.Data.Entity.Spatial;

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


        public ActionResult PointMaps(string DataInicial, string DataFinal, string Latitude, string Longitude)
        {
            DateTime datainicio = DateTime.Parse(DataInicial);
            DateTime datafim = DateTime.Parse(DataFinal);

            var coord = DbGeography.FromText($"POINT ({Latitude} {Longitude})");

            var json = new
            {
                Result = db.Pesquisas
                .Where(x => x.DataPesquisa > datainicio && x.DataPesquisa <= datafim)
                .OrderBy(x=>x.Localizacao.Distance(coord))
                .Take(10)
                .Select(x => new
                {
                    Latitude = x.Localizacao.Latitude,
                    Longitude = x.Localizacao.Longitude,
                    TermoPesquisado = x.TermoPesquisado
                })
            };

            return Json(json, JsonRequestBehavior.AllowGet);
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

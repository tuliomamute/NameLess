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
using PagedList;

namespace NameLess.Controllers
{
    [Authorize]
    public class PesquisasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pesquisas
        public ActionResult DashBoard(int? page, string searchString)
        {
            List<DashBoard> pesquisaMaterializada = null;

            var pesquisa = db.Pesquisas.Join(db.Tags,
                pes => pes.TagId,
                tag => tag.TagId,
                (pes, tag) => new
                {
                    DescricaoTag = tag.DescricaoTag,
                    TermoPesquisado = pes.TermoPesquisado,
                })
                .GroupBy(x => new { x.DescricaoTag, x.TermoPesquisado })
                .Select(y => new DashBoard { DescricaoTag = y.Key.DescricaoTag, TermoPesquisado = y.Key.TermoPesquisado, Quantidade = y.Count() });

            if (!String.IsNullOrEmpty(searchString))
                pesquisaMaterializada = pesquisa.Where(s => s.TermoPesquisado.Contains(searchString)).ToList();
            else
                pesquisaMaterializada = pesquisa.ToList();

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pesquisaMaterializada.ToPagedList(pageNumber, pageSize));

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
                .OrderBy(x => x.Localizacao.Distance(coord))
                .Take(10)
                .Select(x => new
                {
                    Latitude = x.Localizacao.Latitude,
                    Longitude = x.Localizacao.Longitude,
                    TermoPesquisado = x.TermoPesquisado,
                    DataPesquisa = x.DataPesquisa
                })
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ColorMap(string DataInicial, string DataFinal)
        {
            DateTime datainicio = DateTime.Parse(DataInicial);
            DateTime datafim = DateTime.Parse(DataFinal);

            var json = new
            {
                Result = db.Pesquisas.Where(x => x.DataPesquisa > datainicio && x.DataPesquisa <= datafim)
                  .GroupBy(x => x.SiglaEstado).Select(x => new { SiglaEstado = x.Key, Count = x.Count() })
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

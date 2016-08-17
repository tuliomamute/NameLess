using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SharedModels.Models;
using System.Data.Entity.Spatial;

namespace NameLess.WebApi.Controllers
{
    public class PesquisasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Pesquisas
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Pesquisas/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Pesquisas
        public void Post(string Termo, DateTime DataPesquisa, int CodCliente, int CodTag, double Latitude, double Longitude)
        {
            Pesquisas pesquisa = new Pesquisas();
            pesquisa.DataPesquisa = DataPesquisa;
            pesquisa.ClienteId = CodCliente;
            pesquisa.TagId = CodTag;
            pesquisa.Localizacao = DbGeography.FromText($"POINT({Latitude} {Longitude})");

            db.Pesquisas.Add(pesquisa);
            db.SaveChanges();

        }

        // PUT: api/Pesquisas/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Pesquisas/5
        public void Delete(int id)
        {
        }
    }
}

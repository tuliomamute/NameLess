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

        /// <summary>
        /// Inserção de uma nova pesquisa
        /// </summary>
        /// <param name="Termo">Termo Pesquisado</param>
        /// <param name="DataPesquisa">Data Pesquisada</param>
        /// <param name="CodCliente">Código do Cliente</param>
        /// <param name="CodTag">Código da Tag</param>
        /// <param name="Latitude">Latitude onde foi feita a pesquisa</param>
        /// <param name="Longitude">Longitude onde foi feita a pesquisa</param>
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

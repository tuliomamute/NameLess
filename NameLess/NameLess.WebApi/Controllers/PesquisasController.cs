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
    /// <summary>
    /// Gerenciamento das Pesquisas Inclusas
    /// </summary>
    public class PesquisasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retorna a lista de pesquisa
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Recupera uma pesquisa dado o Id
        /// </summary>
        /// <param name="id">Código da Pesquisa</param>
        /// <returns></returns>
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Inserção de uma nova pesquisa
        /// </summary>
        /// <param name="pesquisa">Objeto de Pesquisa</param>

        [HttpPost, AllowAnonymous]
        public void Post(Pesquisas pesquisa)
        {
            pesquisa.Localizacao = DbGeography.FromText($"POINT({pesquisa.Latitude} {pesquisa.Longitude})".ToString().Replace(",", "."));

            db.Pesquisas.Add(pesquisa);
            db.SaveChanges();


        }
    }
}

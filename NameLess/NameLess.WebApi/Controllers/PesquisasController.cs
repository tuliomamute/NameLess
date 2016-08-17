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
    }
}

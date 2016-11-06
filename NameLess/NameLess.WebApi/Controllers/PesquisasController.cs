using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SharedModels.Models;
using System.Data.Entity.Spatial;
using Newtonsoft.Json;
using SharedModels;

namespace NameLess.WebApi.Controllers
{
    /// <summary>
    /// Gerenciamento das Pesquisas Inclusas
    /// </summary>
    public class PesquisasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Inserção de uma nova pesquisa
        /// </summary>
        /// <param name="pesquisa">Objeto de Pesquisa</param>

        [HttpPost, AllowAnonymous]
        public void Post(Pesquisas pesquisa)
        {
            pesquisa.Localizacao = DbGeography.FromText($"POINT({pesquisa.Latitude} {pesquisa.Longitude})".ToString().Replace(",", "."));
            pesquisa.SiglaEstado = RetornaSiglaEstado(pesquisa.Latitude, pesquisa.Longitude);

            db.Pesquisas.Add(pesquisa);
            db.SaveChanges();
        }

        /// <summary>
        /// Método para retornar o estado dada latitude e longitude
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        private string RetornaSiglaEstado(double latitude, double longitude)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://maps.googleapis.com/");
                var result = client.GetAsync($"/maps/api/geocode/json?latlng={latitude.ToString().Replace(",", ".")},{longitude.ToString().Replace(",", ".")}&result_type=street_address&key=AIzaSyCoPaQDmuU37-jQ17kYmYfcpBjNAPKujKI").Result;

                //Validação de status code
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Conversão de Json para objeto, para retorno da sigla de estado
                    Rootobject root = JsonConvert.DeserializeObject<Rootobject>(result.Content.ReadAsStringAsync().Result);
                    return $"br-{(root.results[0]).address_components[5].short_name.ToString().ToLower()}";
                }
            }
            return null;
        }
    }
}

using Newtonsoft.Json;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;

namespace CarregaBase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://namelesswebapi.azurewebsites.net/");

                List<Pesquisa> model = LerArquivo();
                int contador = 1;
                Pesquisas pesquisa;
                foreach (var user in model)
                {
                    pesquisa = new Pesquisas();

                    pesquisa.TagId = user.TagId;
                    pesquisa.Latitude = double.Parse(user.Latitude, CultureInfo.InvariantCulture);
                    pesquisa.Longitude = double.Parse(user.Longitude, CultureInfo.InvariantCulture);
                    pesquisa.TermoPesquisado = user.TermoPesquisado;
                    pesquisa.DataPesquisa = user.DataPesquisa;
                    pesquisa.ClienteId = 1;

                    var result = client.PostAsJsonAsync("/api/Pesquisas/", pesquisa).Result;
                    Console.WriteLine($"Item:{contador} de {model.Count} - Status Code:{result.StatusCode}");
                    contador++;
                }
            }
        }
        private static List<Pesquisa> LerArquivo()
        {
            StreamReader reader = new StreamReader($@"{Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).FullName) }\DadosCliente.json");
            return JsonConvert.DeserializeObject<List<Pesquisa>>(reader.ReadToEnd());

        }

    }
}


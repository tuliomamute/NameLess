using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SharedModels.Models
{
    [NotMapped]
    public class DashBoard
    {
        [DisplayName("Nome Tag")]
        public string DescricaoTag { get; set; }
        [DisplayName("Data de Pesquisa")]
        public DateTime DataPesquisa { get; set; }

        [DisplayName("Termo Pesquisado")]
        public string TermoPesquisado { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    public class Pesquisas
    {
        [Key]
        public int PesquisaId { get; set; }
        public string TermoPesquisado { get; set; }
        public DbGeography Localizacao { get; set; }
        public string DataPesquisa { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int TagId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("TagId")]
        public virtual Tags Tags { get; set; }
    }
}
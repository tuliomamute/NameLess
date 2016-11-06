using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace SharedModels.Models
{
    public class Pesquisas
    {
        [Key]
        public int PesquisaId { get; set; }
        public string TermoPesquisado { get; set; }
        public DbGeography Localizacao { get; set; }
        public DateTime DataPesquisa { get; set; }
        public string SiglaEstado { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int TagId { get; set; }

        [NotMapped]
        public double Latitude { get; set; }
        [NotMapped]
        public double Longitude { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("TagId")]
        public virtual Tags Tags { get; set; }

    }
}
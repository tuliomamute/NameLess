using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    public class CamposPesquisa
    {
        [Key]
        public int CamposPesquisaId { get; set; }
        [Required]
        public int TagId { get; set; }
        [DisplayName("Nome do Campo")]
        public string IdCampo { get; set; }
        [DisplayName("Tipo do Campo")]
        public string TipoCampo { get; set; }

        public string UsuarioId { get; set; }
        [ForeignKey("TagId")]
        public virtual Tags Tags { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser Usuario { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    [Table("Pesquisa")]
    public class PesquisasModels
    {
        [Key]
        public int PesquisaId { get; set; }
        public string TermoPesquisado { get; set; }
        public string Localizacao { get; set; }
        public string DataPesquisa { get; set; }
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public int TagId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual ClienteModels Cliente { get; set; }

        [ForeignKey("TagId")]
        public virtual TagsModels Tags { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    [Table("Cliente")]
    public class TagsModels
    {
        public TagsModels()
        {
            this.CamposPesquisa = new HashSet<CamposPesquisaModels>();
            this.Pesquisa = new HashSet<PesquisasModels>();
        }

        [Key]
        [DisplayName("ID")]
        public int TagId { get; set; }
        [DisplayName("Nome Tag")]
        public string Tag { get; set; }
        [DisplayName("Descrição")]
        public string DescricaoTag { get; set; }
        [DisplayName("Observação")]
        public string ObservacaoTag { get; set; }
        public virtual ICollection<CamposPesquisaModels> CamposPesquisa { get; set; }
        public virtual ICollection<PesquisasModels> Pesquisa { get; set; }
    }
}
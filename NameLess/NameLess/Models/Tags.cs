using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    public class Tags
    {
        public Tags()
        {
            this.CamposPesquisa = new HashSet<CamposPesquisa>();
            this.Pesquisa = new HashSet<Pesquisas>();
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
        public string UsuarioId { get; set; }
        public virtual ICollection<CamposPesquisa> CamposPesquisa { get; set; }
        public virtual ICollection<Pesquisas> Pesquisa { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser Usuario { get; set; }

    }
}
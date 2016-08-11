using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    public class Cliente
    {
        public Cliente()
        {
            this.Pesquisa = new HashSet<Pesquisas>();
        }

        [Key]
        public int ClienteId { get; set; }
        [DisplayName("Nome Cliente")]
        public string NomeCliente { get; set; }
        [DisplayName("Endereço")]
        public string EnderecoCliente { get; set; }
        [DisplayName("CNPJ")]
        public string CnpjCliente { get; set; }

        public virtual ICollection<Pesquisas> Pesquisa { get; set; }

        public virtual ICollection<ApplicationUser> Usuario { get; set; }

    }
}
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
    public class ClienteModels
    {
        public ClienteModels()
        {
            this.Pesquisa = new HashSet<PesquisasModels>();
        }

        [Key]
        public int ClienteId { get; set; }
        [DisplayName("Nome Cliente")]
        public string NomeCliente { get; set; }
        [DisplayName("Endereço")]
        public string EnderecoCliente { get; set; }
        [DisplayName("CNPJ")]
        public string CnpjCliente { get; set; }

        public virtual ICollection<PesquisasModels> Pesquisa { get; set; }
        public virtual ICollection<ApplicationUser> Usuario { get; set; }
    }
}
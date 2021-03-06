﻿using Utilitario.Utilitario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SharedModels.Models
{
    public class CamposPesquisa
    {
        [Key]
        public int CamposPesquisaId { get; set; }
        [Required, DisplayName("Tag")]
        public int TagId { get; set; }
        [DisplayName("Nome do Campo Texto")]
        public string IdCampoTexto { get; set; }
        [DisplayName("Nome do Campo Botão")]
        public string IdCampoBotao { get; set; }
        [DisplayName("Tipo do Campo")]
        public string TipoCampo { get; set; }

        public string UsuarioId { get; set; }
        [ForeignKey("TagId"), DisplayName("Tag")]
        public virtual Tags Tags { get; set; }
        [ForeignKey("UsuarioId")]
        public virtual ApplicationUser Usuario { get; set; }

        public List<TipoCampo> RetornaTipoCampos()
        {
            return new List<TipoCampo>
            {
                new TipoCampo { TipoCampoId = "ID", Nome = "Localizar campo por id" },
              new TipoCampo { TipoCampoId = "NAME", Nome = "Localizar campo por name" }
            };

        }

    }
}
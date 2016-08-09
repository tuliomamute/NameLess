using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NameLess.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<NameLess.Models.Tags> Tags { get; set; }
        public System.Data.Entity.DbSet<NameLess.Models.CamposPesquisa> CamposPesquisa { get; set; }
        public System.Data.Entity.DbSet<NameLess.Models.Pesquisas> Pesquisas { get; set; }
        public System.Data.Entity.DbSet<NameLess.Models.Cliente> Cliente { get; set; }

    }
}
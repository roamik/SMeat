using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS
{
    public class ApplicationContext : DbContext
    {
        
        #region Config
        private readonly IOptions<AppConnectionStrings> _options;

        public ApplicationContext() {
        }

        public ApplicationContext( IOptions<AppConnectionStrings> options ) {
            _options = options;
        }

        protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder ) {
            optionsBuilder.UseSqlServer(_options.Value.DefaultConnection);
          //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SMSNv1;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        #endregion
        

        public DbSet<User> Users { get; set; }
    }
}

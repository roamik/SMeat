using Microsoft.EntityFrameworkCore;
using SMeat.MODELS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.DAL
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}

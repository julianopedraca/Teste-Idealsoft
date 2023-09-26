using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_teste_idealsoft.Controllers;
using Microsoft.EntityFrameworkCore;

namespace backend_teste_idealsoft.models
{
    public class LivrosContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

        public LivrosContext(DbContextOptions options) : base (options)
        {
            
        }
    }
}
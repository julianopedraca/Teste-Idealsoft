using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend_teste_idealsoft.Controllers;
using Microsoft.EntityFrameworkCore;

namespace backend_teste_idealsoft.models
{
    public class AgendaContext : DbContext
    {
        public DbSet<Agenda> Agendas { get; set; }

        public AgendaContext(DbContextOptions options) : base (options)
        {
            
        }
    }
}
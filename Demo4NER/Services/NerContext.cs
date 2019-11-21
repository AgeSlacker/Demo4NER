using Demo4NER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo4NER.Services
{
    public class NerContext : DbContext
    {
        private string myDbConnection = "Server=81.88.52.17;port=3306;Database=qm3jt7qs_4ner_app;User=qm3jt7qs_admin;Password=admin4NER;";

        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Establishment> Establishments { get; set; }
        public NerContext() : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(myDbConnection);
        }

    }
}

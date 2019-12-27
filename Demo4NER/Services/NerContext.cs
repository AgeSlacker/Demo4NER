using Demo4NER.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
            try
            {
                Database.EnsureCreated();
                Database.CreateExecutionStrategy(); // Todo https://github.com/aspnet/EntityFrameworkCore/issues/12526

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(myDbConnection);
        }
    }
}

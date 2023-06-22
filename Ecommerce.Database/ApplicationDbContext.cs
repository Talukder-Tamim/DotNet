﻿using Ecommerce.Models.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=IT-TAMIM; Database=SampleCommerceDB; Trusted_Connection=True; TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}

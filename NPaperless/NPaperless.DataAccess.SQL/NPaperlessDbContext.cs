﻿using Microsoft.EntityFrameworkCore;
using NPaperless.DataAccess.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPaperless.DataAccess.SQL
{
    public class NPaperlessDbContext : DbContext
    {
        public DbSet<CorrespondentDAL> Correspondents { get; set; }
        public DbSet<DocumentDAL> Documents { get; set; }
        public DbSet<DocumentTypeDAL> DocumentTypes { get; set; }
        public DbSet<TagDAL> Tags { get; set; }
        public NPaperlessDbContext() { }
        public NPaperlessDbContext(DbContextOptions<NPaperlessDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var isConfigured = optionsBuilder.IsConfigured;
            if (!isConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=dev;Password=dev;Database=npaperless");
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        }
    }
}

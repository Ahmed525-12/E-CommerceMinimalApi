using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace E_Commerce.Infastrcture;

 public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContexts>
    {
        public AppDbContexts CreateDbContext(string[] args)
        {
            // build config to read your connection string
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "E-Commerce.Api"))     // points to the E-Commerce.Api directory where appsettings.json is located
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<AppDbContexts>();
            var connStr = config.GetConnectionString("DefaultConnection");
            builder.UseNpgsql(connStr);

            return new AppDbContexts(builder.Options);
        }
    }

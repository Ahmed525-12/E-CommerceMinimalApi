using System;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infastrcture;

public class AppDbContexts : DbContext
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
        }
}

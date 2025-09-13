// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using SecureUserApi.Models;

namespace SecureUserApi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
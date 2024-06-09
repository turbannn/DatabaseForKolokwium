﻿using Kolokwium.Model;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.DAL.EF;
public class ApplicationDbContext : DbContext
{
    public DbSet<Department> departments { get; set; } = null!;
    public DbSet<Employee> employees { get; set; } = null!;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}

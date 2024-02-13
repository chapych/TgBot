using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public sealed class ApplicationContext : DbContext
{
    public DbSet<User> Users;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public ApplicationContext()
    {

    }
}
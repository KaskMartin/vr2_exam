using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected ApplicationDbContext()
        {
        }
    }
}

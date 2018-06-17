using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Answer>()
                .HasKey(x => new {x.QuestionId});

            builder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(d => d.Answers)
                .HasForeignKey(a => a.QuestionId);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(builder);

            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // Implement soft Delete for all Entities
                // https://www.meziantou.net/2017/07/10/entity-framework-core-soft-delete-using-query-filters
                // 1. Add the Active property
                if (!IsLinkingEntity(entityType.ClrType))
                {
                    entityType.GetOrAddProperty("Active", typeof(bool));

                    // 2. Create the query filter
                    var parameter = Expression.Parameter(entityType.ClrType);

                    // EF.Property<bool>(post, "Active")
                    var boolPropertyMethodInfo = typeof(Microsoft.EntityFrameworkCore.EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                    var activeProperty = Expression.Call(boolPropertyMethodInfo, parameter, Expression.Constant("Active"));

                    // EF.Property<bool>(post, "Active") == true
                    BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, activeProperty, Expression.Constant(true));

                    // post => EF.Property<bool>(post, "Active") == true
                    var lambda = Expression.Lambda(compareExpression, parameter);

                    builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }


                // Add default values to timestamps
                entityType.GetOrAddProperty("AddTime", typeof(DateTime));
                entityType.GetOrAddProperty("UpdateTime", typeof(DateTime));
            }
        }

        private static bool IsLinkingEntity(Type entityType)
        {
            return entityType == typeof(IdentityUserRole<string>) ||
                   entityType == typeof(IdentityRoleClaim<string>) ||
                   entityType == typeof(IdentityUserClaim<string>);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (!IsLinkingEntity(entry.Entity.GetType()))
                        {
                            entry.CurrentValues["Active"] = true;
                        }
                        entry.CurrentValues["AddTime"] = DateTime.UtcNow;
                        entry.CurrentValues["UpdateTime"] = DateTime.UtcNow;
                        break;

                    case EntityState.Deleted:
                        if (!IsLinkingEntity(entry.Entity.GetType()))
                        {
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["Active"] = false;
                            entry.CurrentValues["UpdateTime"] = DateTime.UtcNow;
                        }
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues["UpdateTime"] = DateTime.UtcNow;
                        break;
                }
            }
        }
        protected ApplicationDbContext()
        {
        }
    }
}

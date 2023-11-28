using Common.Utilities;
using Entities.Common;
using Entities.User;
using Entities.Vehicle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<User, Role, int>
    {

        public ApplicationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entitiesAssembly = typeof(IEntity).Assembly;

            modelBuilder.RegisterAllEntities<IEntity>(entitiesAssembly);
            modelBuilder.RegisterEntityTypeConfiguration(entitiesAssembly);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.Entity<TollFreeVehicleEntity>(entity =>
            {
                entity.ToTable(name: "TollFreeVehicle");
                entity.HasKey(x => x.CityId);
                entity.HasKey(x => x.VehicleId);
            });


        }

        public override int SaveChanges()
        {
            _cleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            _cleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);
            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var val = (string)property.GetValue(item.Entity, null);

                    if (!val.HasValue()) continue;

                    var newVal = val.Fa2En().FixPersianChars();
                    if (newVal == val)
                        continue;
                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }


    public static class EFCoreLinqExtensions
    {
        public static DbContextOptionsBuilder UseMemberReplacement<TValue>(this DbContextOptionsBuilder optionsBuilder, Expression<Func<TValue>> whatToReplace, Expression<Func<TValue>> replacement)
        {
            AddMemberReplacement(optionsBuilder, whatToReplace, replacement);
            return optionsBuilder;
        }

        public static DbContextOptionsBuilder UseMemberReplacement<TObject, TValue>(this DbContextOptionsBuilder optionsBuilder, Expression<Func<TObject, TValue>> whatToReplace, Expression<Func<TObject, TValue>> replacement)
        {
            AddMemberReplacement(optionsBuilder, whatToReplace, replacement);
            return optionsBuilder;
        }

        public static DbContextOptionsBuilder UseMemberReplacement<TParam1, TParam2, TResult>(this DbContextOptionsBuilder optionsBuilder,
            Expression<Func<TParam1, TParam2, TResult>> whatToReplace,
            Expression<Func<TParam1, TParam2, TResult>> replacement)
        {
            AddMemberReplacement(optionsBuilder, whatToReplace, replacement);
            return optionsBuilder;
        }

        public static DbContextOptionsBuilder UseMemberReplacement<TParam1, TParam2, TParam3, TResult>(this DbContextOptionsBuilder optionsBuilder,
            Expression<Func<TParam1, TParam2, TParam3, TResult>> whatToReplace,
            Expression<Func<TParam1, TParam2, TParam3, TResult>> replacement)
        {
            AddMemberReplacement(optionsBuilder, whatToReplace, replacement);
            return optionsBuilder;
        }

        static void AddMemberReplacement(DbContextOptionsBuilder optionsBuilder, LambdaExpression whatToReplace, LambdaExpression replacement)
        {
            var coreExtension = optionsBuilder.Options.GetExtension<CoreOptionsExtension>();

            QueryExpressionReplacementInterceptor? currentInterceptor = null;
            if (coreExtension.Interceptors != null)
            {
                currentInterceptor = coreExtension.Interceptors.OfType<QueryExpressionReplacementInterceptor>()
                    .FirstOrDefault();
            }

            if (currentInterceptor == null)
            {
                currentInterceptor = new QueryExpressionReplacementInterceptor();
                optionsBuilder.AddInterceptors(currentInterceptor);
            }

            MemberInfo member;

            if (whatToReplace.Body is MemberExpression memberExpression)
            {
                member = memberExpression.Member;
            }
            else
            if (whatToReplace.Body is MethodCallExpression methodCallExpression)
            {
                member = methodCallExpression.Method;
            }
            else
                throw new InvalidOperationException($"Expression '{whatToReplace.Body}' is not MemberExpression or MethodCallExpression");

            if (whatToReplace.Parameters.Count != replacement.Parameters.Count)
                throw new InvalidOperationException($"Replacement Lambda should have exact count of parameters as input expression '{whatToReplace.Parameters.Count}' but found {replacement.Parameters.Count}");

            currentInterceptor.AddMemberReplacement(member, replacement);
        }

        class MemberReplacementVisitor : ExpressionVisitor
        {
            private readonly List<(MemberInfo member, LambdaExpression replacenment)> _replacements;

            public MemberReplacementVisitor(List<(MemberInfo what, LambdaExpression replacenmet)> replacements)
            {
                _replacements = replacements;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                foreach (var (what, replacement) in _replacements)
                {
                    if (node.Member == what)
                    {
                        var args = new List<Expression>();

                        if (node.Expression != null)
                            args.Add(node.Expression);

                        var visitor = new ReplacingExpressionVisitor(replacement.Parameters, args);
                        var newNode = visitor.Visit(replacement.Body);

                        return Visit(newNode);
                    }
                }

                return base.VisitMember(node);
            }

            protected override Expression VisitMethodCall(MethodCallExpression node)
            {
                foreach (var (what, replacement) in _replacements)
                {
                    if (node.Method == what)
                    {
                        var args = new List<Expression>(node.Arguments);
                        if (node.Object != null)
                            args.Insert(0, node.Object);

                        var visitor = new ReplacingExpressionVisitor(replacement.Parameters, args);
                        var newNode = visitor.Visit(replacement.Body);

                        return Visit(newNode);
                    }
                }

                return base.VisitMethodCall(node);
            }

        }

        sealed class QueryExpressionReplacementInterceptor : IQueryExpressionInterceptor
        {
            readonly List<(MemberInfo member, LambdaExpression replacenment)> _memberReplacements = new();

            public Expression QueryCompilationStarting(Expression queryExpression, QueryExpressionEventData eventData)
            {
                if (_memberReplacements.Count == 0)
                    return queryExpression;

                var visitor = new MemberReplacementVisitor(_memberReplacements);

                var result = visitor.Visit(queryExpression);

                return result;
            }

            public void AddMemberReplacement(MemberInfo member, LambdaExpression replacement)
            {
                _memberReplacements.Add((member, replacement));
            }
        }
    }

}

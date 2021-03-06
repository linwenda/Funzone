﻿using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.PostDrafts;
using Funzone.Domain.Posts;
using Funzone.Domain.PostVotes;
using Funzone.Domain.Users;
using Funzone.Domain.ZoneMembers;
using Funzone.Domain.ZoneRules;
using Funzone.Domain.Zones;
using Funzone.Infrastructure.DataAccess.EntityConfigurations;
using Funzone.Infrastructure.Processing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Funzone.Infrastructure.DataAccess
{
    public class FunzoneDbContext : DbContext, ITransactionContext
    {
        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;

        public FunzoneDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityTypeConfiguration).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<ZoneMember> ZoneMembers { get; set; }
        public DbSet<ZoneRule> ZoneRules { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostDraft> PostDrafts { get; set; }
        public DbSet<PostVote> PostVotes { get; set; }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public bool HasActiveTransaction => _currentTransaction != null;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
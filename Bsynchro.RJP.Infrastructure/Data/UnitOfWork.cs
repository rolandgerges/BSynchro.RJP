using Bsynchro.RJP.Contracts.Data.Entities;
using Bsynchro.Migrations;
using Bsynchro.RJP.Infrastructure.Data.Repositories.Generic;

namespace Bsynchro.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        private readonly DatabaseContext _context;
        private Repository<Account> accountRepository;
        private Repository<Customer> customerRepository;
        private Repository<Transaction> transactionRepository;

        public Repository<Account> AccountRepository
        {
            get
            {
                accountRepository ??= new Repository<Account>(_context);
                return accountRepository;
            }
        }
        public Repository<Customer> CustomerRepository
        {
            get
            {
                customerRepository ??= new Repository<Customer>(_context);
                return customerRepository;
            }
        }
        public Repository<Transaction> TransactionRepository
        {
            get
            {
                transactionRepository ??= new Repository<Transaction>(_context);
                return transactionRepository;
            }
        }

        public UnitOfWork(DatabaseContext context) => _context = context;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
    
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

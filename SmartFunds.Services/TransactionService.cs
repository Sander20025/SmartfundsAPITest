using Microsoft.EntityFrameworkCore;
using SmartFunds.Core;
using SmartFunds.Model;

namespace SmartFunds.Services
{
    public class TransactionService
    {
        private readonly SmartFundsDbContext _dbContext;

        public TransactionService(SmartFundsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public IList<Transaction> Find(int? organizationId)
        {
            var query = _dbContext.Transactions.AsQueryable();
            if (organizationId.HasValue)
            {
                query = query.Where(t => t.OrganizationId == organizationId.Value);
            }
            return query
                .Include(t => t.Organization)
                .ToList();
        }

        //Get
        public Transaction? Get(int id)
        {
            return _dbContext.Transactions
                .SingleOrDefault(o => o.Id == id);
        }

        //Create
        public Transaction Create(Transaction transaction)
        {
            transaction.TimeStamp = DateTime.UtcNow;

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return transaction;
        }
        
        //Delete
        public void Delete(int id)
        {
            var dbTransaction = _dbContext
                .Transactions
                .SingleOrDefault(o => o.Id == id);

            if (dbTransaction is null)
            {
                return;
            }

            _dbContext.Transactions.Remove(dbTransaction);
            _dbContext.SaveChanges();
        }

    }
}

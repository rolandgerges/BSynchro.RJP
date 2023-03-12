using System;
using System.Transactions;

namespace Bsynchro.RJP.Contracts.Data.Entities
{
	public partial class Account
	{
        public int Id { get; set; }
        public float Balance { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Transaction>? SourceTransactions { get; set; }
        public ICollection<Transaction>? DestinationTransactions { get; set; }
    }
}
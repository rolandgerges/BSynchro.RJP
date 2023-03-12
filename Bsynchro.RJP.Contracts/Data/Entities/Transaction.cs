namespace Bsynchro.RJP.Contracts.Data.Entities
{
	public class Transaction
	{
        public int Id { get; set; }
        public float Amount {get;set;}
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public Account SourceAccount { get; set; }
        public Account DestinationAccount { get; set; }
    }
}


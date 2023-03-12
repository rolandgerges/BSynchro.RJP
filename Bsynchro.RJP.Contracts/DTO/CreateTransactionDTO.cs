namespace Bsynchro.Contracts.DTO
{
    public class CreateTransactionDTO
    {
        public float Amount { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
    }
}
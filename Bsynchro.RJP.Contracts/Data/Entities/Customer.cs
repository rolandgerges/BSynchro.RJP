namespace Bsynchro.RJP.Contracts.Data.Entities
{
    public class Customer
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}

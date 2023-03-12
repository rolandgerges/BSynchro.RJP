using  Bsynchro.RJP.Contracts.Data.Entities;

namespace Bsynchro.Contracts.DTO
{
    public class CustomerTransactionsDTO
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public  ICollection<AccountTransactionsDTO> Transactions { get; set; }
    
    }
}
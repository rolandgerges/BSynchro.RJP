
public class AccountTransactionsDTO{
    public int Id {get;set;}
    public float Amount {get;set;}
    public int SourceAccountId {get;set;}
    public int DestinationAccountId {get;set;}
    public int Status {get;set;}
    public DateTime CreatedAt {get;set;}
}
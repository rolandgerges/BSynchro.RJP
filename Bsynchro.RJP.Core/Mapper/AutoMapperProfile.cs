using AutoMapper;
using Bsynchro.Contracts.DTO;
using Bsynchro.RJP.Contracts.Data.Entities;

namespace Bsynchro.Core.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Account,CustomerTransactionsDTO>()
            .ForMember(dest=>dest.FirstName,opt=>opt.MapFrom(src=>src.Customer.FirstName))
            .ForMember(dest=>dest.SurName,opt=>opt.MapFrom(src=>src.Customer.SurName))
            .ForMember(dest=>dest.Transactions,opt=>opt.MapFrom(
                src=>src. SourceTransactions.Concat(src.DestinationTransactions)));
            CreateMap<Transaction,AccountTransactionsDTO>();
        }
    }
}

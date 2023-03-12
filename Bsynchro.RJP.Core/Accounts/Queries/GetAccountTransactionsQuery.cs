//using FluentValidation;
using MediatR;
using Bsynchro.Infrastructure;
using Bsynchro.Contracts.DTO;
using AutoMapper;
using Bsynchro.RJP.Contracts.Data.Entities;

namespace Bsynchro.RJP.Core.Accounts.Commands
{
    public class GetAccountTransactionsQuery : IRequest<CustomerTransactionsDTO>
    {
        public GetAccountTransactionsDTO AccountDTO { get; }
        public GetAccountTransactionsQuery(GetAccountTransactionsDTO accountDto)
        {
            AccountDTO = accountDto;
        }
    }

    public class GetAccountTransactionsQueryHandler : IRequestHandler<GetAccountTransactionsQuery, CustomerTransactionsDTO>
    {
        private readonly UnitOfWork _repository;
        private readonly IMapper _mapper;
        // private readonly IValidator<CreateAccountDTO> _validator;
        public GetAccountTransactionsQueryHandler(IMapper mapper,
             UnitOfWork repository)
        // ,IValidator<CreateAccountDTO> validator)
        {
            _mapper = mapper;
            _repository = repository;
            //_validator = validator;
        }

        public async Task<CustomerTransactionsDTO> Handle(GetAccountTransactionsQuery request, CancellationToken cancellationToken)
        {
            var accountId = request.AccountDTO.AccountId;
            var account =  _repository.AccountRepository
            .GetWithInclude(x => x.Id == accountId,
             includeProperties: string.Join(',', new string[]{nameof(Account.Customer),
             nameof(Account.SourceTransactions),nameof(Account.DestinationTransactions)})).First();

            return _mapper.Map<CustomerTransactionsDTO>(account);
        }
    }
}


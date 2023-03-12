using Bsynchro.RJP.Contracts.DTO;
using MediatR;
using Bsynchro.Infrastructure;
using Bsynchro.RJP.Contracts.Data.Entities;
using FluentValidation;
using Bsynchro.Core.Exceptions;

namespace Bsynchro.RJP.Core.Accounts.Commands
{
    public class CreateAccountCommand : IRequest<int>
    {
        public CreateAccountDTO AccountDTO { get; }
        public CreateAccountCommand(CreateAccountDTO accountDto)
        {
            AccountDTO = accountDto;
        }
    }

    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, int>
    {
        private readonly UnitOfWork _repository;
         private readonly IValidator<CreateAccountDTO> _validator;
        public CreateAccountCommandHandler(
             UnitOfWork repository,IValidator<CreateAccountDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }


        public async Task<int> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {

            CreateAccountDTO model = request.AccountDTO;

            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }
            var customer = _repository.CustomerRepository.Get(model.CustomerId);
            if(customer==null) throw new EntityNotFoundException("No customer found with the provided Id");
            var account = new Account
            {
                Balance = model.Balance,
                CustomerId = model.CustomerId,
                CreatedAt = DateTime.Now,
            };

            _repository.AccountRepository.Add(account);
            await _repository.CommitAsync();
            if (model.Balance > 0)
            {
                var transaction = new Transaction
                {
                    Amount = model.Balance,
                    SourceAccountId = account.Id,
                    DestinationAccountId = account.Id,
                    Status = true,
                    CreatedAt = DateTime.Now
                };
                _repository.TransactionRepository.Add(transaction);
                await _repository.CommitAsync();
            }
            return account.Id;
        }
    }
}

using MediatR;
using Bsynchro.Infrastructure;
using Bsynchro.Contracts.DTO;
using Bsynchro.RJP.Contracts.Data.Entities;

namespace Bsynchro.RJP.Core.Transactions.Commands{

public class CreateTransactionCommand : IRequest<int>
    {
        public CreateTransactionDTO TransactionDTO { get; }
        public CreateTransactionCommand(CreateTransactionDTO transactionDTO)
        {
            TransactionDTO = transactionDTO;
        }

    }

    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, int>
    {
        private readonly UnitOfWork _repository;
       // private readonly IValidator<CreateAccountDTO> _validator;
        public CreateTransactionCommandHandler(
             UnitOfWork repository)
        // ,IValidator<CreateAccountDTO> validator)
        {
            _repository = repository;
            //_validator = validator;
        }
        public async Task<int> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
              CreateTransactionDTO model = request.TransactionDTO;

              var transaction = new Transaction
                {
                    Amount = model.Amount,
                    SourceAccountId=model.SourceAccountId,
                    DestinationAccountId=model.DestinationAccountId,
                    Status = true,
                    CreatedAt = DateTime.Now

                };
                _repository.TransactionRepository.Add(transaction);
                await _repository.CommitAsync();
               return transaction.Id;
        }
    }
}


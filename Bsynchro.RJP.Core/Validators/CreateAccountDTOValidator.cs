using Bsynchro.RJP.Contracts.DTO;
using FluentValidation;

namespace Bsynchro.Core.Validators
{
    public class CreateAccountDTOValidator : AbstractValidator<CreateAccountDTO>
    {
        public CreateAccountDTOValidator()
        {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId required");
        RuleFor(x => x.Balance).GreaterThanOrEqualTo(0).WithMessage("Balance cannot be less than 0");
        }
    }
}

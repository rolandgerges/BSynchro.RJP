using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Bsynchro.Contracts.DTO;
using Bsynchro.Core.Exceptions;
using Bsynchro.RJP.Contracts.DTO;
using Bsynchro.RJP.Core.Accounts.Commands;

namespace Bsynchro.RJP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{accountId}/transactions")]
        [ProducesResponseType(typeof(CustomerTransactionsDTO), (int)HttpStatusCode.OK)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Get([FromRoute] int accountId)
        {
            try
            {
                var response = await _mediator.Send(new GetAccountTransactionsQuery(new GetAccountTransactionsDTO { AccountId = accountId }));
                return Ok(response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.Created)]
        [ProducesErrorResponseType(typeof(BaseResponseDTO))]
        public async Task<IActionResult> Post([FromBody] CreateAccountDTO model)
        {
            try
            {
                var response = await _mediator.Send(new CreateAccountCommand(model));
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (InvalidRequestBodyException ex)
            {
                return BadRequest(new BaseResponseDTO
                {
                    IsSuccess = false,
                    Errors = ex.Errors
                });
            }
        }


    }
}
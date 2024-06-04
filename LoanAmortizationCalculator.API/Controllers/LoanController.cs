using LoanAmortizationCalculator.Application.Commands.CreateLoan;
using LoanAmortizationCalculator.Application.Commands.RescheduleLoan;
using LoanAmortizationCalculator.Application.Queries.GetLoan;
using LoanAmortizationCalculator.Application.DTOs;
using LoanAmortizationCalculator.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoanAmortizationCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoansController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Existing actions...

        [HttpPost]
        public async Task<IActionResult> CreateLoan([FromBody] CreateLoanCommand request)
        {
            var result = await _mediator.Send(request);

            if (result == 0)
            {
                return BadRequest("Failed to create loan");
            }

            return Ok(result);
        }

        [HttpPut("reschedule")]
        public async Task<IActionResult> RescheduleLoan([FromBody] RescheduleLoanRequest request)
        {
            var command = new CreateRescheduleLoanCommand
            {
                LoanId = request.LoanId,
                NewInterestRate = request.NewInterestRate
            };

            var result = await _mediator.Send(command);

            if (result == 0)
            {
                return NotFound("Loan not found");
            }

            return Ok(result);
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoan(int loanId)
        {
            var query = new GetLoanQuery { LoanId = loanId };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound("Loan not found");
            }

            return Ok(result);
        }
    }
}
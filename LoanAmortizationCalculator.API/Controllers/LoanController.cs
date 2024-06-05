using LoanAmortizationCalculator.Application.Commands.CreateLoan;
using LoanAmortizationCalculator.Application.Commands.RescheduleLoan;
using LoanAmortizationCalculator.Application.Queries.GetLoan;
using LoanAmortizationCalculator.Application.DTOs;
using LoanAmortizationCalculator.API.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OfficeOpenXml;

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

        [HttpPost("upload-loans")]
        public async Task<IActionResult> UploadLoans([FromForm] UploadLoanFileDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var loans = new List<CreateLoanCommand>();
            using (var stream = new MemoryStream())
            {
                await dto.File.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                    {
                        var command = new CreateLoanCommand
                        {
                            Amount = decimal.Parse(worksheet.Cells[row, 1].Text),
                            DownPayment = decimal.Parse(worksheet.Cells[row, 2].Text),
                            InterestRate = double.Parse(worksheet.Cells[row, 3].Text),
                            StartDate = DateTime.Parse(worksheet.Cells[row, 4].Text),
                            LoanDurationValue = int.Parse(worksheet.Cells[row, 5].Text),
                            LoanDurationUnitId = int.Parse(worksheet.Cells[row, 6].Text),
                            PaymentFrequencyId = int.Parse(worksheet.Cells[row, 7].Text),
                            GracePeriodValue = int.Parse(worksheet.Cells[row, 8].Text),
                            GracePeriodUnitId = int.Parse(worksheet.Cells[row, 9].Text)
                        };
                        loans.Add(command);
                    }
                }
            }

            foreach (var loan in loans)
            {
                await _mediator.Send(loan);
            }

            return Ok(new { Message = "Loans created successfully." });
        }

    }
}
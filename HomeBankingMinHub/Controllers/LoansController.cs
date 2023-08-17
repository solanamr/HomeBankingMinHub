using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class LoansController : ControllerBase
    {
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ILoanRepository _loanRepository;
        private IClientLoanRepository _clientLoanRepository;
        private ITransactionRepository _transactionRepository;

        public LoansController(IClientRepository clientRepository,
            IAccountRepository accountRepository,
            ILoanRepository loanRepository,
            IClientLoanRepository clientLoanRepository,
            ITransactionRepository transactionRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _loanRepository = loanRepository;
            _transactionRepository = transactionRepository;
            _clientLoanRepository = clientLoanRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var loans = _loanRepository.GetAllLoans();
                var loansDTO = new List<LoanDTO>();
                foreach (Loan l in loans) 
                {
                    var newLoanDTO = new LoanDTO
                    {
                        Id = l.Id,
                        Name = l.Name,
                        MaxAmount = l.MaxAmount,
                        Payments = l.Payments
                    };
                    loansDTO.Add(newLoanDTO);
                }
                return Ok(loansDTO);
            }
            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LoanApplicationDTO loanApplicationDTO)
        {
            try 
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid();
                }

                Client client = _clientRepository.FindByEmail(email);

                if (client == null)
                {
                    return Forbid();
                }

                var loan = _loanRepository.FindById(loanApplicationDTO.LoanId);
                if(loan == null)
                {
                    return Forbid();
                }
                if(string.IsNullOrEmpty(loanApplicationDTO.Payments) || loanApplicationDTO.Amount <= 0 ||  string.IsNullOrEmpty(loanApplicationDTO.ToAccountNumber) || loanApplicationDTO.Payments == "0")
                {
                    return BadRequest("the fields must be completed");
                }

                if (loanApplicationDTO.Amount > loan.MaxAmount) 
                {
                    return BadRequest("the amount exceeds the permitted max");
                }

                var paymentList = loan.Payments.Split(',');

                if (!paymentList.Contains(loanApplicationDTO.Payments))
                {
                    return BadRequest("Error en las cuotas solicitadas");
                }

                var account = _accountRepository.FindByNumber(loanApplicationDTO.ToAccountNumber);
                if (account == null) 
                {
                    return Forbid();
                }

                //if (loanApplicationDTO.Type != LoanType.HIPOTECARIO.ToString() && loanApplicationDTO.Type != LoanType.PERSONAL.ToString() && loanApplicationDTO.Type != LoanType.AUTOMOTRIZ.ToString())
                //{
                //    return BadRequest("enter a valid loan type");
                //}

                var acc = client.Accounts.Where(acc => acc.Number == account.Number).FirstOrDefault();
                if(acc == null) 
                {
                    return BadRequest("accounts not matching");
                }

                account.Balance += loanApplicationDTO.Amount;
                _accountRepository.Save(account);

                Transaction transaction = new Transaction
                {
                    Type = TransactionType.CREDIT.ToString(),
                    Amount = loanApplicationDTO.Amount,
                    Description = loan.Name.ToString() + " loan approved",
                    Date = DateTime.Now,
                    AccountId = account.Id
                };

                _transactionRepository.Save(transaction);

                ClientLoan clientLoan = new ClientLoan
                {
                    Amount = loanApplicationDTO.Amount + loanApplicationDTO.Amount * 0.2,
                    Payments = loanApplicationDTO.Payments,
                    ClientId = client.Id,
                    LoanId = loan.Id
                };
                _clientLoanRepository.Save(clientLoan);

                return Ok(loanApplicationDTO);

            }
            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }
        }

    }
}

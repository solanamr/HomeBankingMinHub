﻿using HomeBankingMinHub.Models;
using HomeBankingMinHub.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace HomeBankingMinHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IAccountRepository _accountRepository;
        

        public AccountsController(IAccountRepository accountRepository)

        {

            _accountRepository = accountRepository;
            
        }

        [HttpGet]

        public IActionResult Get()

        {

            try

            {

                var accounts = _accountRepository.GetAllAccounts();

                var accountsDTO = new List<AccountDTO>();

                foreach (Account a in accounts)

                {

                    var newAccountDTO = new AccountDTO

                    {

                        Id = a.Id,

                        Number = a.Number,

                        CreationDate = a.CreationDate,

                        Balance = a.Balance,

                        Transactions = a.Transactions.Select(trans => new TransactionDTO

                        {

                            Id = trans.Id,

                            Type = trans.Type,

                            Amount = trans.Amount,

                            Description = trans.Description,

                            Date = trans.Date

                        }).ToList()

                    };

                    accountsDTO.Add(newAccountDTO);

                }

                return Ok(accountsDTO);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }

        [HttpGet("{id}")]

        public IActionResult Get(long id)

        {

            try

            {

                var acc = _accountRepository.FindById(id);

                if (acc == null)

                {

                    return NotFound();

                }

                var accDTO = new AccountDTO

                {

                    Id = acc.Id,

                    Number = acc.Number,

                    CreationDate = acc.CreationDate,

                    Balance = acc.Balance,

                    Transactions = acc.Transactions.Select(ac => new TransactionDTO

                    {

                        Id = ac.Id,

                        Type = ac.Type,

                        Amount = ac.Amount,

                        Description = ac.Description,

                        Date = ac.Date

                    }).ToList()

                };

                return Ok(accDTO);

            }

            catch (Exception ex)

            {

                return StatusCode(500, ex.Message);

            }

        }


        [HttpPost]
        [Authorize]

        public IActionResult Post(long clientId)
        {
            try
            {
                Account newAccount = new Account
                {
                    ClientId = clientId,
                    CreationDate = DateTime.Now,
                    Balance = 0,
                    Number = "S" + new Random().Next(100, 999).ToString()

                };

                _accountRepository.Save(newAccount);
                AccountDTO newaccDTO = new AccountDTO
                {
                    Id = newAccount.Id,
                    Balance = newAccount.Balance,
                    CreationDate = newAccount.CreationDate,
                    Number = newAccount.Number
                };
                return Created("", newaccDTO);


            }
            catch
            {
                return null;
            }
        }

    }
}

//en el acc contr retornar un iactionresult con el created con el accDTO y cuando se pasa al cliencontroller retornar lo que se recibe que es la variable account con el accountcontroller

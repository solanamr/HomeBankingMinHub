using System;
using System.Linq;

namespace HomeBankingMinHub.Models
{
    public class DbInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new Client[]
                {
                    new Client { Email = "vcoronado@gmail.com", FirstName="Victor", LastName="Coronado", Password="123456"},
                    new Client { Email = "sb@gmail.com", FirstName="señora", LastName="bart", Password="1111"},
                    new Client { Email = "chichi@gmail.com", FirstName="Chichi", LastName="Peralta", Password="6012"},
                };

                foreach (Client client in clients)
                {
                    context.Clients.Add(client);
                }

                //guardamos
                context.SaveChanges();
            }

            if (!context.Accounts.Any())
            {
                var clientVictor = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (clientVictor != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = clientVictor.Id, CreationDate = DateTime.Now, Number = "S239", Balance = 102340 },
                        new Account {ClientId = clientVictor.Id, CreationDate = DateTime.Now, Number = "S421", Balance = 5620045 },
                        new Account {ClientId = clientVictor.Id, CreationDate = DateTime.Now, Number = "S611", Balance = 95689349 }
                    };
                    foreach (Account clientAccount in accounts)
                    {
                        context.Accounts.Add(clientAccount);
                    }
                    context.SaveChanges();

                }
            }

            if (!context.Transactions.Any())

            {

                var account1 = context.Accounts.FirstOrDefault(c => c.Number == "S239");

                if (account1 != null)

                {

                    var transactions = new Transaction[]

                    {

                        new Transaction { AccountId= account1.Id, Amount = 10000, Date= DateTime.Now.AddHours(-5), Description = "Transferencia de Susanita recibida", Type = TransactionType.CREDIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -2000, Date= DateTime.Now.AddHours(-6), Description = "Compra en heladeria Cremolatti", Type = TransactionType.DEBIT.ToString() },

                        new Transaction { AccountId= account1.Id, Amount = -3000, Date= DateTime.Now.AddHours(-7), Description = "Compra en Mcdonald's", Type = TransactionType.DEBIT.ToString() },

                    };

                    foreach (Transaction t in transactions)

                    {

                        context.Transactions.Add(t);

                    }

                    context.SaveChanges();

                }

            }

            if (!context.Loans.Any())
            {
                //crearemos 3 prestamos Hipotecario, Personal y Automotriz
                var loans = new Loan[]
                {
                    new Loan { Name = "Hipotecario", MaxAmount = 500000, Payments = "12, 24, 36, 48, 60" },
                    new Loan { Name = "Personal", MaxAmount = 100000, Payments = "6, 12, 24" },
                    new Loan { Name = "Automotriz", MaxAmount = 300000, Payments = "6, 12, 24, 36" },
                };

                foreach (Loan loan in loans)
                {
                    context.Loans.Add(loan);
                }

                context.SaveChanges();

                //ahora agregaremos los clientloan (Prestamos del cliente)
                //usaremos al único cliente que tenemos y le agregaremos un préstamo de cada item
                var client1 = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (client1 != null)
                {
                    //ahora usaremos los 3 tipos de prestamos
                    var loan1 = context.Loans.FirstOrDefault(l => l.Name == "Hipotecario");
                    if (loan1 != null)
                    {
                        var clientLoan1 = new ClientLoan
                        {
                            Amount = 400000,
                            ClientId = client1.Id,
                            LoanId = loan1.Id,
                            Payments = "60"
                        };
                        context.ClientLoans.Add(clientLoan1);
                    }

                    var loan2 = context.Loans.FirstOrDefault(l => l.Name == "Personal");
                    if (loan2 != null)
                    {
                        var clientLoan2 = new ClientLoan
                        {
                            Amount = 50000,
                            ClientId = client1.Id,
                            LoanId = loan2.Id,
                            Payments = "12"
                        };
                        context.ClientLoans.Add(clientLoan2);
                    }

                    var loan3 = context.Loans.FirstOrDefault(l => l.Name == "Automotriz");
                    if (loan3 != null)
                    {
                        var clientLoan3 = new ClientLoan
                        {
                            Amount = 100000,
                            ClientId = client1.Id,
                            LoanId = loan3.Id,
                            Payments = "24"
                        };
                        context.ClientLoans.Add(clientLoan3);
                    }

                    //guardamos todos los prestamos
                    context.SaveChanges();

                }

            }


            if (!context.Cards.Any())
            {
                //buscamos al unico cliente
                var clientOne = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (clientOne != null)
                {
                    //le agregamos 2 tarjetas de crédito una GOLD y una TITANIUM, de tipo DEBITO Y CREDITO RESPECTIVAMENTE
                    var cards = new Card[]
                    {
                        new Card {
                            ClientId= clientOne.Id,
                            CardHolder = clientOne.FirstName + " " + clientOne.LastName,
                            Type = CardType.DEBIT.ToString(),
                            Color = CardColor.GOLD.ToString(),
                            Number = "3325-6745-7876-9995",
                            Cvv = 990,
                            FromDate= DateTime.Now,
                            ThruDate= DateTime.Now.AddYears(4),
                        },
                        new Card {
                            ClientId= clientOne.Id,
                            CardHolder = clientOne.FirstName + " " + clientOne.LastName,
                            Type = CardType.CREDIT.ToString(),
                            Color = CardColor.TITANIUM.ToString(),
                            Number = "2234-6745-552-5000",
                            Cvv = 750,
                            FromDate= DateTime.Now,
                            ThruDate= DateTime.Now.AddYears(5),
                        },
                    };

                    foreach (Card c in cards)
                    {
                        context.Cards.Add(c);
                    }
                    context.SaveChanges();
                }
            }

        }
    }
}

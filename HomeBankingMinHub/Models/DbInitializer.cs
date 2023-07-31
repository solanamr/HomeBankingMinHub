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

        }
    }
}

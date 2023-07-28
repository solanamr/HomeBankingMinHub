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

        }
    }
}

﻿using HomeBankingMindHub.Models;
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
                    
                };

                foreach (Client client in clients)
                {
                    context.Clients.Add(client);
                }

                //guardamos
                context.SaveChanges();
            }

        }
    }
}

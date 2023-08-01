using HomeBankingMinHub.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace HomeBankingMinHub.Models
{
    public class ClientDTO
    {
        //solo ignora la línea que sigue y nada más o sea solo va a ignorar el id
        [JsonIgnore]

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public ICollection<AccountDTO> Accounts { get; set; }
        public ICollection<ClientLoanDTO> Credits { get; set; }
        public ICollection<CardDTO> Cards { get; set; }
    }
}

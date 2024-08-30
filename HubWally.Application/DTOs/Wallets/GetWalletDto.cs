using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.DTOs.Wallets
{
    public class GetWalletDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public string AccountScheme { get; set; }
        public string Owner { get; set; }
        public DateTime? Created_At { get; set; }
    }
}

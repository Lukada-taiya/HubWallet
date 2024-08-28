using HubWally.Domain.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Domain.Models
{
    public class Wallet : BaseModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public string AccountScheme { get; set; } 
        public string Owner { get; set; }
    }
}

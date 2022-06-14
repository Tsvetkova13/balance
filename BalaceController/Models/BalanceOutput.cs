using System;
using System.Collections.Generic;

namespace BalaceController.Models
{
    public class BalanceOutput
    {
        public List<ArrowOutput> arrowOutput { get; set; }
        public List<string> exeption { get; set; }
        public string Status { get => status; set => status = value; }

        string status;
    }
}

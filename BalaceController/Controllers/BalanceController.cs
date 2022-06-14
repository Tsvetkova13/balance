using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BalaceController.Services;
using BalaceController.Models;





// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BalaceController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
            [HttpPost]
            public BalanceOutput Post([FromBody] BalanceInput balanceInput)
            {
                BalanceService balanceService = new();
                return balanceService.balance(balanceInput);
            }
        
      
    }
}

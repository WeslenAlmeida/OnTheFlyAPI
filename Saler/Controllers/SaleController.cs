using DomainAPI.Models.Sale;
using DomainAPI.Services.Sale;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Saler.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SaleController(SalesService salesServices)
        {
            _salesService = salesServices;
        }

        [HttpGet]
        public ActionResult<List<Sales>> Get() => _salesService.Get();
    }
}

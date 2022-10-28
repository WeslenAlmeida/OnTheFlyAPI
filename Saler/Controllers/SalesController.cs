using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saler.Model;
using Saler.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saler.Controllers
{
    [Route("apiSaler/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService salesServices)
        {
            _salesService = salesServices;
        }

        [HttpGet]
        public ActionResult<List<Sales>> Get() => _salesService.Get();

        [HttpGet("{id:length(24)}", Name = "GetSale")]
        public ActionResult<Task<Sales>> GetAsync(string id)
        {
            //var adopter = _salesService.Get(id);

            //if (adopter == null)
            //    return NotFound();

            //return Ok(adopter);
        }

        [HttpPost]
        public ActionResult<Task<Sales>> CreateAsync()
        {
            //_salesService.Create(sale);
            //return CreatedAtRoute("GetAdopter", new { id = sale.Id.ToString() }, sale);
        }

        [HttpPut("{id:length(24)}")]
        public ActionResult<Task<Sales>> UpdateAsync()
        {
            //var adopter = _salesService.Get(id);

            //if (adopter == null)
            //{
            //    return NotFound();
            //}

            //adopterIn.Id = id;

            //_salesService.Update(id, adopterIn);

            //return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult<Task<Sales>> DeleteAsync()
        {
            //var adopter = _salesService.Get(id);

            //if (adopter == null)
            //{
            //    return NotFound();
            //}

            //_salesService.Remove(id);

            //return NoContent();
        }
    }
}


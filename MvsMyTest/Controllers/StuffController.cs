using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvsMyTest.Models;
using MvsMyTest.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvsMyTest.Controllers
{
    [Route("api/[controller]")]
    public class StuffController : Controller
    {
        private readonly IStuffService _stuffService;

        public StuffController(IStuffService stuffService)
        {
            _stuffService = stuffService;
        }

        // GET api/stuff
        [HttpGet]
        public string Get()
        {
            return "This is .net core 2.0 MVC";
        }

        //[HttpGet]
        //public IEnumerable<StuffItem> GetAll()
        //{
        //    return _stuffContext.StuffItems.ToList();
        //}

        [HttpGet("{id}", Name = "GetStuff")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _stuffService.GetByIdAsync(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StuffItem item)
        {
            if (item == null)
                return BadRequest();

            await _stuffService.UpdateAsync(item);

            return CreatedAtRoute("GetStuff", new { id = item.Id }, item);
            //return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stuffService.DeleteAsync(id);
            return new NoContentResult();
        }
    }
}

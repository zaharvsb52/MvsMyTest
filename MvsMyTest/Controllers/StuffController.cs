﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvsMyTest.Data;
using MvsMyTest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvsMyTest.Controllers
{
    [Route("api/[controller]")]
    public class StuffController : Controller
    {
        private readonly IStuffRepository _stuffService;

        public StuffController(IStuffRepository stuffService)
        {
            _stuffService = stuffService;
        }

        // GET api/stuff
        //[HttpGet]
        //public string Get()
        //{
        //    return "This is .net core 2.0 MVC";
        //}

        [HttpGet]
        public async Task<IEnumerable<StuffItem>> GetAll()
        {
            return await _stuffService.GetAll();
        }

        [HttpGet("{id}", Name = "GetStuff")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _stuffService.Get(id.ToString());
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] StuffItem item)
        {
            if (item == null)
                return BadRequest();

            await _stuffService.Update(item);

            return CreatedAtRoute("GetStuff", new { id = item.Id }, item);
            //return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stuffService.Remove(id.ToString());
            return new NoContentResult();
        }
    }
}

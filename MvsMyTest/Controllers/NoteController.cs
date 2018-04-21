using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvsMyTest.Data;
using MvsMyTest.Models;

namespace MvsMyTest.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NoteController : Controller
    {
        private readonly INoteRepository _noteRepository;

        public NoteController(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        //[NoCache]
        [HttpGet]
        public Task<IEnumerable<Note>> Get()
        {
            return GetNoteInternal();
        }

        private async Task<IEnumerable<Note>> GetNoteInternal()
        {
            return await _noteRepository.GetAll();
        }

        // GET api/note/5
        [HttpGet("{id}")]
        public Task<Note> Get(string id)
        {
            return GetNoteByIdInternal(id);
        }

        private async Task<Note> GetNoteByIdInternal(string id)
        {
            return await _noteRepository.Get(id) ?? new Note();
        }

        // POST api/note
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _noteRepository.Add(new Note() { Body = value, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now });
        }

        // PUT api/note/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _noteRepository.Update(id, value);
        }

        // DELETE api/note/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _noteRepository.Remove(id);
        }
    }
}

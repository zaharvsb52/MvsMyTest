using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MvsMyTest.Data;
using MvsMyTest.Models;

namespace MvsMyTest.Controllers
{
    [Route("api/[controller]")]
    public class MongoInitController : Controller
    {
        private readonly INoteRepository _noteRepository;
        private readonly IStuffRepository _stuffRepository;

        public MongoInitController(INoteRepository noteRepository, IStuffRepository stuffRepository)
        {
            _noteRepository = noteRepository;
            _stuffRepository = stuffRepository;
        }

        // Call an initialization - api/mongoinit/init note
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            switch (setting)
            {
                case "init note":
                    _noteRepository.RemoveAll();
                    _noteRepository.Add(new Note() { Id = "1", Body = "Test note 1", 
                        CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
                    _noteRepository.Add(new Note() { Id = "2", Body = "Test note 2", 
                        CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
                    _noteRepository.Add(new Note() { Id = "3", Body = "Test note 3", 
                        CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
                    _noteRepository.Add(new Note() { Id = "4", Body = "Test note 4", 
                        CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
                    break;

                case "init stuff":
                    _stuffRepository.RemoveAll();
                    _stuffRepository.Add(new StuffItem
                    {
                        Name = "name1",
                        Description = "d1",
                        Tags = new List<TagItem> {new TagItem {Id = 1, Value = "2"}}
                    });
                    break;
                default:
                    return "Unknown";
            }

            return "Done";
        }
    }
}

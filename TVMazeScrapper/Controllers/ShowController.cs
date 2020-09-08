using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using TVMazeScrapper.Models;
using TVMazeScrapper.Services;

namespace TVMazeScrapper.Controllers
{
    [ApiController]
    public class ShowController : ControllerBase
    {
        private readonly IShowService _showService;
        public ShowController()
        {
            _showService = new ShowService();
        }
        // GET: api/<ShowController>
        [HttpGet]
        [Route("api/Show")]
        public ActionResult<List<Show>> Get(int page)
        {
            List<Show> show = _showService.GetAll(page == 0 ? 1 : page).ToList();
            if(show.Count() > 0)
            {
                return show.Select(s => s.OrderByBirthday()).ToList();
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET api/<ShowController>/5
        [HttpGet("{id}")]
        [Route("api/Show/GetByID/{id}")]
        public ActionResult<Show> GetByID(int id)
        {
            var show = _showService.Get(id);
            if (show == null)
            {
                return NotFound();
            }

            return show;
        }

        // POST api/<ShowController>
        [HttpPost]
        [Route("api/AddShow")]
        public void Post([FromBody] Show show)
        {
            _showService.Insert(show);
        }

        // PUT api/<ShowController>/5
        [HttpPut("{id}")]
        [Route("api/EditShow/{id}")]
        public void Put(int id, [FromBody] Show show)
        {
            _showService.Update(show, id);
        }

        // DELETE api/<ShowController>/5
        [HttpDelete("{id}")]
        [Route("api/RemoveShow/{id}")]
        public void Delete(int id)
        {
            _showService.Delete(id);
        }
    }
}

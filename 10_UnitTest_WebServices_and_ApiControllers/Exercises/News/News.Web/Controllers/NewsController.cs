
namespace News.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using News.Data;
    using News.Data.Models;

    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly NewsDbContext db;

        public NewsController(NewsDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllMessages()
        {
            return Ok(this.db.Messages);
        }

        [HttpGet("{id}")]
        public IActionResult GetMessage([FromRoute] int id)
        {
            var message = this.db.Messages.Find(id);

            if (message == null)
            {
                return NotFound();
            }

            return this.Ok(message);
        }
        
        [HttpPost]
        public IActionResult PostMessage([FromBody]Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.db.Messages.Add(message);
            this.db.SaveChanges();

            return CreatedAtAction("GetMessage", new { id = message.Id }, message);
            //return StatusCode(201);           
        }
        
        [HttpPut("{id}")]
        public IActionResult PutMessage([FromRoute] int id, [FromBody]Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentmessage = this.db.Messages.Find(id);

            if (currentmessage == null)
            {
                return BadRequest();
            }

            currentmessage.Title = message.Title;
            currentmessage.Content = message.Content;
            currentmessage.PublishDate = message.PublishDate;

            this.db.SaveChanges();

            return Ok();
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteMessage([FromRoute] int id)
        {
            
            var currentmessage = this.db.Messages.Find(id);

            if (currentmessage == null)
            {
                return BadRequest();
            }

            this.db.Messages.Remove(currentmessage);
            this.db.SaveChanges();

            return Ok();
        }
    }
}


namespace ApiDemo.Controllers
{
    using ApiDemo.DataDb;
    using ApiDemo.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    [Route("api/[controller]")]
    public class CatsApiController: Controller
    {
        private readonly IData data;

        public CatsApiController(IData data)
        {
            this.data = data;
        }

        [HttpGet]
        public Cat[] Get()
        {
           return data.All().ToArray();
        }

        [HttpGet("{id}")]
        public Cat Get(int id)
        {
            return data.Find(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody]Cat cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = this.data.Add(cat);

            return Ok(id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Cat cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataCat = this.data.Find(id);

            if (dataCat == null)
            {
                return NotFound();
            }

            dataCat.Name = cat.Name;
            dataCat.Age = cat.Age;
            dataCat.Color = cat.Color;

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cat = this.data.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            this.data.Delete(id);

            return Ok();
        }
    }
}

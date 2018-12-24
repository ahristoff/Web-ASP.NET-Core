
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Linq;

    public class CarsController: Controller
    {
        private readonly ICarService cars;
        private readonly IPartService parts;

        public CarsController(ICarService cars, IPartService parts)
        {
            this.cars = cars;
            this.parts = parts;
        }

        [Route("cars/{make?}")]
        public IActionResult ByMake(string make)
        {
             return View();         
        }

        [HttpPost]
        [Route("cars/{make?}")]
        public IActionResult ByMake(string make, int num)
        {
            var cars = this.cars.ByMake(make);

            return View(new CarsByMakeModel
            {
                Make = make,
                Cars = cars
            });
        }

        [Route("cars/parts")]
        public IActionResult Parts()
        {
            var cars = this.cars.WithParts();

            return View(new CarWithParts
            {
                CarParts = cars
            });
        }

        [Route("cars/create")]
        public IActionResult Create()
        {
            return View(new CreateCarModel
            {
                AllParts = this.parts.All()
                .Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                })             
            });
        }

        [HttpPost]
        [Route("cars/create")]
        public IActionResult Create(CreateCarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(new CreateCarModel
                {
                    AllParts = this.parts.All()
                    .Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    })
                });
            }

            this.cars.Create(carModel.Make, carModel.Model, carModel.TravelledDistance, carModel.SelectedParts);

            return RedirectToAction(nameof(Parts));
        }
    }
}

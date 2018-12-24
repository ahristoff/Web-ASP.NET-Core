
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models.Parts;
    using CarDealer.Web.Models.Parts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PartsController: Controller
    {
        private const int PageSize = 25;

        private readonly IPartService parts;
        private readonly ISupplierService suppliers;

        public PartsController(IPartService parts, ISupplierService suppliers)
        {
            this.parts = parts;
            this.suppliers = suppliers;
        }

        [Route("parts/all/{page?}")]
        public IActionResult All(int page = 1)
        {
            var parts = this.parts.AllListings(page, PageSize);

            return View(new PartsPaginationModel
            {
                Parts = parts,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.parts.Total() / (double)PageSize)
            });
        }

        [Route("/parts/create")]
        public IActionResult Create()
        {
            return View(new CreatePartModel
            {
                Suppliers = this.suppliers.All().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
            });
        }

        [HttpPost]
        [Route("/parts/create")]
        public IActionResult Create(CreatePartModel model)
        {
            if (!ModelState.IsValid)
            {
                //ModelState.AddModelError("Price", "тъп ли се бе");

                return View(new CreatePartModel
                {
                    Suppliers = this.suppliers.All()
                    .Select(s => new SelectListItem
                    {
                        Text = s.Name,
                        Value = s.Id.ToString()
                    })
                });
            }

            parts.Create(model.Name, model.Price, model.SupplierId, model.Quantity);

            return RedirectToAction(nameof(All), new { page = 1 });
        }

        [Route("parts/delete/{id}")]
        public IActionResult Delete(int id)
        {
            return View(id);
        }

        [Route("parts/destroy/{id}")]
        public IActionResult Destroy(int id)
        {
            this.parts.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Route("parts/edit/{id}")]    //[Route(nameof(Edit) + "/{id}")]
        public IActionResult Edit(int id)
        {
            var customer = parts.Edit(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost]
        [Route("parts/edit/{id}")]
        public IActionResult SaveEdit(int id, EditPartModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool partExists = this.parts.Exists(id);

            if (!partExists)
            {
                return NotFound();
            }
            parts.SaveEdit(model.Id, model.Price, model.Quantity);

            return RedirectToAction(nameof(All));
        }
    }
}

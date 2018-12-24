
namespace CarDealer.Web.Controllers
{
    using CarDealer.Services;
    using CarDealer.Services.Models.Suppliers;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    using System.Linq;

    public class SuppliersController: Controller
    {
        private readonly ISupplierService suppliers;
        private readonly IPartService parts;

        public SuppliersController(ISupplierService suppliers, IPartService parts)
        {
            this.suppliers = suppliers;
            this.parts = parts;
        }

        public IActionResult Local()
        {
            return this.View("Suppliers", this.GetSuppliers(false));
        }

        public IActionResult Importers()
        {
            return View("Suppliers", this.GetSuppliers(true));
        }

        private SuppliersModel GetSuppliers(bool importers)
        {
            var type = importers ? "Importers" : "Local";

            var suppliers = this.suppliers.AllListing(importers);

            return new SuppliersModel
            {
                Type = type,
                Suppliers = suppliers
            };
        }

        //pgination
        [Route("suppliers/all")]
        public IActionResult AllSuppliers(int page = 1, int pageSize = 5)
        {
            var suppliers = this.suppliers.AllSuppliersWithParts(page, pageSize);

            return View(new SuppliersPaginationModel
            {
                Suppliers = suppliers,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(this.suppliers.Total() / (double)pageSize)
            });
        }

        [Route("suppliers/create")]
        public IActionResult Create()
        {
            return View(new CreateSupplierModel
            {
                AllParts = this.parts.AllParts().Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                })
            });
        }

        [HttpPost]
        [Route("suppliers/create")]
        public IActionResult Create(CreateSupplierModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(new CreateSupplierModel
                {
                    AllParts = this.parts.AllParts().Select(p => new SelectListItem
                    {
                        Text = p.Name,
                        Value = p.Id.ToString()
                    })
                });
            }

            suppliers.CreateSupplier(model.Name, model.IsImporter, model.PartIds);

            return RedirectToAction(nameof(AllSuppliers), new { page = 1 });
        }

        [Route("/suppliers/edit/{id}")]
        public IActionResult Edit(int id)
        {
            var supplier = suppliers.Edit(id);

            return View(new EditSuppliersModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                IsImporter = supplier.IsImporter,

                AllParts = this.parts.All().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
            });
        }

        [HttpPost]
        [Route("/suppliers/edit/{id}")]
        public IActionResult Edit(int id, EditSuppliersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool supplierExists = this.parts.Exists(id);

            if (!supplierExists)
            {
                return NotFound();
            }
            suppliers.SaveEdit(model.Id, model.Name, model.IsImporter, model.PartIds);

            return RedirectToAction(nameof(AllSuppliers), new { page = 1 });
        }

        [Route("suppliers/delete/{id}")]
        public IActionResult Delete(int id)
        {
            return View(id);
        }

        [Route("suppliers/destroy/{id}")]
        public IActionResult Destroy(int id)
        {
            this.suppliers.Delete(id);

            return RedirectToAction(nameof(AllSuppliers), new { page = 1 });
        }
    }
}

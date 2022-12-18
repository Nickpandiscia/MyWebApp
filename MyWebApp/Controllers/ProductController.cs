using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
	public class ProductController : Controller
	{
		private readonly ApplicationDbContext _db;

		public ProductController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{

			IEnumerable<Products> products = _db.Products;
			return View(products);
		}

		public IActionResult Create()
		{
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]

		public IActionResult Create(Products obj)
		{

			if (obj.Name == obj.type.ToString())
			{
				ModelState.AddModelError("name", "The type cannot exactly match the name.");
			}

			if (ModelState.IsValid)
			{

				_db.Products.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Product Created Successfully!";
				return RedirectToAction("Index");

			}
			return View(obj);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			
			var productFromDb = _db.Products.Find(id);
			if (productFromDb == null)
			{
				return NotFound();
			}

			return View(productFromDb);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Products obj)
		{
			if (obj.Name == obj.type.ToString())
			{
				ModelState.AddModelError("name", "The type cannot exactly match the name.");
			}

			if (ModelState.IsValid)
			{
				_db.Products.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Product Updated Successfully!";
				return RedirectToAction("Index");

			}
			return View(obj);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			var productFromDb = _db.Products.Find(id);
			if (productFromDb == null)
			{
				return NotFound();
			}

			return View(productFromDb);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Products.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Products.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Product Deleted Successfully!";
			return RedirectToAction("Index");



		}
	}
}

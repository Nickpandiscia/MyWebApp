using Microsoft.AspNetCore.Mvc;
using MyWebApp.Data;
using MyWebApp.Models;

namespace MyWebApp.Controllers
{
	public class CategoryController : Controller
	{
		//Grabbing all the info from the table.
		private readonly ApplicationDbContext _db;

		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			IEnumerable<Category> objCategoryList = _db.Categories;
			return View(objCategoryList);
		}

		//A GET action method
		public IActionResult Create()
		{
			return View();
		}

		//Using a POST method
		[HttpPost]
		[ValidateAntiForgeryToken]
		//this token protects your database
		public IActionResult Create(Category obj)
		{
			//Makes sure the name of the two categories are not the same.
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name.");
			}
			//This if statements makes sure the program doesn't crash when you click create without typing anything.
			if (ModelState.IsValid)
			{
				//adding to database
				_db.Categories.Add(obj);
				//making sure it saves to the database.
				_db.SaveChanges();
				TempData["success"] = "Category Created Successfully!";
				//Making sure it is valid
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

			//find the category based on the ID from the table.
			var categoryFromDb = _db.Categories.Find(id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//Using a POST method
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name.");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();
				TempData["success"] = "Category Updated Successfully!";
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

			var categoryFromDb = _db.Categories.Find(id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}

			return View(categoryFromDb);
		}

		//Using a POST method
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Categories.Find(id);
			if (obj == null)
			{
				return NotFound();
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category Deleted Successfully!";
			return RedirectToAction("Index");



		}
	}
}

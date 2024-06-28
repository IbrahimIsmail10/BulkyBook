using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;

using Microsoft.AspNetCore.Mvc;
using Bulky.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Bulky.DataAccess.Repository;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ICategoryRepository _repository;

        public CategoryController(ApplicationDbContext db,ICategoryRepository repository) { 
            _db = db;
            _repository = repository;
        }


        public IActionResult Index()
        {
            List<Category> categoryList = _repository.GetAll();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(obj);
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _repository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _repository.Remove(obj);

            return RedirectToAction("Index");
        }


    }
}

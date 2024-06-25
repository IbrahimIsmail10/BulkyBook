﻿using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Bulky.Models;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) { 
            _db = db;
        }


        public IActionResult Index()
        {
            List<Category> categoryList = _db.Categories.ToList();
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
            var obj = _db.Categories.FirstOrDefault(u=>u.Id == id);
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
                _db.Categories.Update(obj);
                _db.SaveChanges();
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
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost ,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();  

            return RedirectToAction("Index");
        }


    }
}

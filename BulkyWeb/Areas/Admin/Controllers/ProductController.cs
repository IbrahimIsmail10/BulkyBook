using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IUnitOfWork _repository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext db, IUnitOfWork repository, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Product> productslist = _repository.Product.GetAll();
            return View(productslist);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                categoryList = _repository.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);  
            }
            else
            {
                productVM.product = _repository.Product.Get(u => u.Id == id);
                return View(productVM);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            int maxId = _db.Products.Max(e => e.Id);
            maxId++;
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = maxId.ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    obj.product.ImageUrl = @"\images\product\" + fileName;
                }
                obj.product.Id = maxId;
                _repository.Product.Add(obj.product);
                return RedirectToAction("Index");
            }
            else 
            {
                obj.categoryList = _repository.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _repository.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var obj = _repository.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _repository.Product.Remove(obj);

            return RedirectToAction("Index");
        }

    }
}

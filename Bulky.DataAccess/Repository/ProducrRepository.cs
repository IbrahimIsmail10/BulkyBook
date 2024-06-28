using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProducrRepository : Repository<Product>, IProducrRepository
    {
        private readonly ApplicationDbContext _db;
        public ProducrRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product entity)
        {
            var objForm = _db.Products.FirstOrDefault(u => u.Id == entity.Id);
            if (objForm != null) { 

                objForm.Title = entity.Title;
                objForm.Description = entity.Description;
                objForm.Price = entity.Price;
                objForm.ListPrice = entity.ListPrice;
                objForm.Price50 = entity.Price50;
                objForm.Price100 = entity.Price100;
                objForm.ISBN = entity.ISBN;
                objForm.CategoryId = entity.CategoryId;
                objForm.Author = entity.Author;
                if (objForm.ImageUrl != null) { 
                    objForm.ImageUrl = objForm.ImageUrl;
                }
            }
        }
    }
}

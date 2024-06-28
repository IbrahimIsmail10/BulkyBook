using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext db) { 
            _db = db;
            this.dbset = db.Set<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>>? Fillter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (Fillter != null)
            {
                query = query.Where(Fillter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            List <T> result = query.ToList();
            Console.WriteLine(result);
            return result;
        }
        public T Get(Expression<Func<T, bool>> Fillter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbset;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (Fillter != null)
            {
                query = query.Where(Fillter);
            }
            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }
        public void Add(T entity)
        {
            dbset.Add(entity);
            Save();
        }
        public void Remove(T entity)
        {
            dbset.Remove(entity);
            Save();
        }
        public void RemoveRange(IEnumerable<T> entity)
        {
            throw new NotImplementedException();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

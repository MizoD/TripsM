﻿using DataAccess.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private DbSet<T> _db { set; get; }

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<bool> CreateAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            try
            {
                _db.Update(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            try
            {
                _db.Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex}");
                return false;
            }
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool tracked = true)
        {
            IQueryable<T> entities = _db;

            if (expression is not null)
            {
                entities = entities.Where(expression);
            }

            if (includes is not null)
            {
                entities = includes(entities);
            }

            if (!tracked)
            {
                entities = entities.AsNoTracking();
            }

            return (await entities.ToListAsync());
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IQueryable<T>>? includes = null, bool tracked = true)
        {
            return (await GetAsync(expression, includes, tracked)).FirstOrDefault();
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ex: {ex}");
                return false;
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(
Expression<Func<T, bool>>? filter = null,
Func<IQueryable<T>, IQueryable<T>>? includes = null,
bool tracked = true)
        {
            IQueryable<T> query = _db;

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes(query);

            if (!tracked)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }
    }
}


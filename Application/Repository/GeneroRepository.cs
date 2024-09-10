using Database.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class GeneroRepository
    {
        private readonly ApplicationContext _dbContext;

        public GeneroRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Genero genero)
        {
            await _dbContext.AddAsync(genero);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Genero genero)
        {
            _dbContext.Entry(genero).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Genero genero)
        {
            _dbContext.Set<Genero>().Remove(genero);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Genero> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Genero>().FindAsync(id);

        }
        public async Task<List<Genero>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Set<Genero>()
                .ToListAsync();
        }
    }
}

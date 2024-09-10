using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;


namespace Application.Repository
{
    public class SerieRepository
    {
        private readonly ApplicationContext _dbContext;
        public SerieRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Serie serie)
        {
            await _dbContext.AddAsync(serie);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Serie serie)
        {
            _dbContext.Entry(serie).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Serie serie)
        {
             _dbContext.Set<Serie>().Remove(serie);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Serie> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Serie>().FindAsync(id);

        }
        public async Task<List<Serie>> GetAllWithDetailsAsync()
        {
            return await _dbContext.Set<Serie>()
                .Include(s => s.Productora)
                .Include(s => s.SerieGeneros)
                    .ThenInclude(sg => sg.Genero)
                .ToListAsync();
        }
        public List<Genero> GetAllGeneros()
        {
            return _dbContext.Generos.ToList();
        }
        public List<Productora> GetAllProductoras()
        {
            return _dbContext.Productoras.ToList();
        }
        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            return await _dbContext.Generos.ToListAsync();
        }

        public async Task<List<Productora>> GetAllProductorasAsync()
        {
            return await _dbContext.Productoras.ToListAsync();
        }
        public async Task AddSerieGeneroAsync(SerieGenero serieGenero)
        {
            await _dbContext.AddAsync(serieGenero);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteSerieGenerosAsync(int serieId)
        {
            var serieGeneros = await _dbContext.SeriesGeneros.Where(sg => sg.SerieId == serieId).ToListAsync();
            _dbContext.SeriesGeneros.RemoveRange(serieGeneros);
            await _dbContext.SaveChangesAsync();
        }

    }
}   

using Application.Repository;
using Database.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels;

namespace Application.Service
{
    public  class GeneroService
    {
        private readonly GeneroRepository _generoRepository;
        public GeneroService(ApplicationContext dbContext)
        {
            _generoRepository = new GeneroRepository(dbContext);
        }

        public async Task Add(GeneroViewModel vm)
        {
            Genero genero = new()
            {
                Nombre = vm.Nombre
            };

            await _generoRepository.AddAsync(genero);
        }
        public async Task Update(GeneroViewModel vm)
        {
            Genero genero = new();
            genero.Id = vm.Id;
            genero.Nombre = vm.Nombre;

            await _generoRepository.UpdateAsync(genero);
        }

        public async Task Delete(int id)
        {
            var product = await _generoRepository.GetByIdAsync(id);
            await _generoRepository.DeleteAsync(product);
        }

        public async Task<GeneroViewModel> GetByIdSaveViewModel(int id)
        {
            var genero = await _generoRepository.GetByIdAsync(id);
            GeneroViewModel vm = new();
            vm.Id = genero.Id;
            vm.Nombre = genero.Nombre;


            return vm;
        }
        public async Task<List<GeneroViewModel>> GetAllViewModels()
        {
            var generoList = await _generoRepository.GetAllWithDetailsAsync();

            return generoList.Select(s => new GeneroViewModel
            {
                Id = s.Id,
                Nombre = s.Nombre,

            }).ToList();
        }
    }
}

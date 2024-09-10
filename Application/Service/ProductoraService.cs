using Application.Repository;
using Application.ViewModels;
using Database.Models;
using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class ProductoraService
    {
        private readonly ProductoraRepository _productoraRepository;
        public ProductoraService(ApplicationContext dbContext)
        {
            _productoraRepository = new ProductoraRepository(dbContext);
        }

        public async Task Add(ProductoraViewModel vm)
        {
            Productora productora = new()
            {
                Nombre = vm.Nombre
            };

            await _productoraRepository.AddAsync(productora);
        }
        public async Task Update(ProductoraViewModel vm)
        {
            Productora serie = new();
            serie.Id = vm.Id;
            serie.Nombre = vm.Nombre;

            await _productoraRepository.UpdateAsync(serie);
        }

        public async Task Delete(int id)
        {
            var product = await _productoraRepository.GetByIdAsync(id);
            await _productoraRepository.DeleteAsync(product);
        }

        public async Task<ProductoraViewModel> GetByIdSaveViewModel(int id)
        {
            var serie = await _productoraRepository.GetByIdAsync(id);
            ProductoraViewModel vm = new();
            vm.Id = serie.Id;
            vm.Nombre = serie.Nombre;
           

            return vm;
        }
        public async Task<List<ProductoraViewModel>> GetAllViewModels()
        {
            var productoraList = await _productoraRepository.GetAllWithDetailsAsync();

            return productoraList.Select(s => new ProductoraViewModel
            {
                Id = s.Id,
                Nombre = s.Nombre,

            }).ToList();
        }
    }
    
}


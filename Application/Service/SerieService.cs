using Application.Repository;
using Application.ViewModels;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Service
{
    public class SerieService 
    {
        private readonly SerieRepository _serieRepository;
        public SerieService(ApplicationContext dbContext)
        {
            _serieRepository =  new (dbContext);
        }

        public async Task Add(saveSerieViewModel vm)
        {
            Serie serie = new();
            serie.Nombre = vm.Nombre;
            serie.ImagenPortada = vm.ImagenPortada;
            serie.EnlaceVideoYouTube = vm.EnlaceVideoYouTube;
            serie.ProductoraId = vm.ProductoraId;


            await _serieRepository.AddAsync(serie);
        }
        public async Task Add(Serie serie, List<int> generoIds)
        {
            await _serieRepository.AddAsync(serie);

            foreach (var generoId in generoIds)
            {
                SerieGenero serieGenero = new SerieGenero
                {
                    SerieId = serie.Id,
                    GeneroId = generoId
                };

                await _serieRepository.AddSerieGeneroAsync(serieGenero);
            }
        }

        public async Task Update(saveSerieViewModel vm)
        {
            Serie serie = await _serieRepository.GetByIdAsync(vm.Id);

            serie.Nombre = vm.Nombre;
            serie.ImagenPortada = vm.ImagenPortada;
            serie.EnlaceVideoYouTube = vm.EnlaceVideoYouTube;
            serie.ProductoraId = vm.ProductoraId;

            await _serieRepository.DeleteSerieGenerosAsync(serie.Id);

            foreach (var generoId in vm.GeneroIds)
            {
                SerieGenero serieGenero = new SerieGenero
                {
                    SerieId = serie.Id,
                    GeneroId = generoId
                };

                await _serieRepository.AddSerieGeneroAsync(serieGenero);
            }

            if (vm.SegundoGeneroId.HasValue && vm.SegundoGeneroId > 0)
            {
                SerieGenero segundoGenero = new SerieGenero
                {
                    SerieId = serie.Id,
                    GeneroId = vm.SegundoGeneroId.Value
                };

                await _serieRepository.AddSerieGeneroAsync(segundoGenero);
            }

            await _serieRepository.UpdateAsync(serie);
        }

        public async Task Delete(int id)
        {
            var product = await _serieRepository.GetByIdAsync(id);
            await _serieRepository.DeleteAsync(product);
        }

        public async Task<saveSerieViewModel> GetByIdSaveViewModel(int id)
        {
            var serie = await _serieRepository.GetByIdAsync(id);
            saveSerieViewModel vm = new();
            vm.Id = serie.Id;
            vm.Nombre = serie.Nombre;
            vm.ImagenPortada = serie.ImagenPortada;
            vm.EnlaceVideoYouTube = serie.EnlaceVideoYouTube;
            vm.ProductoraId = serie.ProductoraId;

            return vm;
        }
        public async Task<List<SerieViewModel>> GetAllViewModels()
        {
            var serieList = await _serieRepository.GetAllWithDetailsAsync();

            return serieList.Select(s => new SerieViewModel
            {
                Id = s.Id,
                Nombre = s.Nombre,
                ImagenPortada = s.ImagenPortada,
                EnlaceVideoYouTube = s.EnlaceVideoYouTube,
                ProductoraNombre = s.Productora.Nombre,
                Generos = s.SerieGeneros.Select(sg => sg.Genero.Nombre).ToList(),
                SegundoGeneroNombre = s.SerieGeneros.ElementAtOrDefault(1)?.Genero.Nombre 

            }).ToList();
        }
        public List<Genero> GetAllGeneros()
        {
            return _serieRepository.GetAllGeneros();
        }
        public List<Productora> GetAllProductoras()
        {
            return _serieRepository.GetAllProductoras();
        }
        public async Task<List<SerieViewModel>> GetFilteredViewModels(string searchString)
        {
            var series = await _serieRepository.GetAllWithDetailsAsync();

            var seriesFiltradas = series.Where(s => s.Nombre.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                                       .Select(s => new SerieViewModel
                                       {
                                           Id = s.Id,
                                           Nombre = s.Nombre,
                                           ImagenPortada = s.ImagenPortada,
                                           EnlaceVideoYouTube = s.EnlaceVideoYouTube,
                                           ProductoraNombre = s.Productora.Nombre,
                                           Generos = s.SerieGeneros.Select(sg => sg.Genero.Nombre).ToList(),
                                           SegundoGeneroNombre = s.SerieGeneros.ElementAtOrDefault(1)?.Genero.Nombre 

                                       })
                                       .ToList();

            return seriesFiltradas;
        }
        public async Task<List<Genero>> GetAllGenerosAsync()
        {
            return await _serieRepository.GetAllGenerosAsync();
        }

        public async Task<List<Productora>> GetAllProductorasAsync()
        {
            return await _serieRepository.GetAllProductorasAsync();
        }



    }
}

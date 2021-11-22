using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Services
{
    public class ColorPaletteService
    {
        private readonly IColorPaletteRepository cp_repository;
        private readonly IUserRepository u_repository;
        private readonly ISaveRepository s_repository;

        public ColorPaletteService(IColorPaletteRepository cp_repository, IUserRepository u_repository, ISaveRepository s_repository)
        {
            this.cp_repository = cp_repository;
            this.u_repository = u_repository;
            this.s_repository = s_repository;
        }

        public IEnumerable<ColorPaletteDto> GetColorPalettes(string orderBy, string sortBy, string sortValue)
        {
            var list = cp_repository.ListAll();
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list)
            {          

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = palette.CreatorId,
                    CreatorName = palette.Creator.Name,
                    Saves = palette.Saves.Count,
                    SavedByCurrentUser = false,
                });
            }

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(sortValue)) result = SortList(result, sortBy, sortValue);
            if (!String.IsNullOrEmpty(orderBy)) result = OrderList(result, orderBy);

            return result;
        }

        public IEnumerable<ColorPaletteDto> GetColorPalettes(int userId, string orderBy, string sortBy, string sortValue)
        {
            var list = cp_repository.ListNotOwn(userId);
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list)
            {
                var isSavedByUser = s_repository.IsPaletteSavedByUser(palette.Id, userId);

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = palette.CreatorId,
                    CreatorName = palette.Creator.Name,
                    Saves = palette.Saves.Where(s => !s.IsDeleted).ToList().Count,
                    SavedByCurrentUser = isSavedByUser,
                });
            }

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(sortValue)) result = SortList(result, sortBy, sortValue);
            if (!String.IsNullOrEmpty(orderBy)) result = OrderList(result, orderBy);

            return result;
        }

        public IEnumerable<ColorPaletteDto> GetPalettesByUser(int userId, int creatorId, string orderBy, string sortBy, string sortValue) {
            var list = cp_repository.ListByUser(creatorId);
            var creatorName = u_repository.GetById(creatorId).Name;
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list) {
               
                var isSavedByUser = s_repository.IsPaletteSavedByUser(palette.Id, userId);

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = creatorId,
                    CreatorName = creatorName,
                    Saves = palette.Saves.Where(s => !s.IsDeleted).ToList().Count,
                    SavedByCurrentUser = isSavedByUser,
                });
            }

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(sortValue)) result = SortList(result, sortBy, sortValue);
            if (!String.IsNullOrEmpty(orderBy))result = OrderList(result, orderBy);

            return result;
        }

        public ColorPaletteDto GetById(int userId, int id)
        {
            var result = cp_repository.GetById(id);
            if (result == null) return null;

            var isSavedByUser = s_repository.IsPaletteSavedByUser(result.Id, userId);

            return (new ColorPaletteDto()
            {
                Id = result.Id,
                Name = result.Name,
                Colors = result.Colors,
                CreatorId = result.CreatorId,
                CreatorName = result.Creator.Name,
                Saves = result.Saves.Where(s => !s.IsDeleted).ToList().Count,
                SavedByCurrentUser = isSavedByUser,
            });
        }

        public IEnumerable<ColorPaletteDto> GetPalettesSavedByUser(int userId, string orderBy, string sortBy, string sortValue)
        {
            var user = u_repository.GetById(userId);
            var list = new List<ColorPalette>();
            var result = new List<ColorPaletteDto>();
            
            foreach (var save in user.Saves) {
                if (!save.IsDeleted) list.Add(save.ColorPalette);
            }

            foreach (var palette in list)
            {

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = palette.CreatorId,
                    CreatorName = palette.Creator.Name,
                    Saves = palette.Saves.Where(s => !s.IsDeleted).ToList().Count,
                    SavedByCurrentUser = true,
                });
            }

            if (!String.IsNullOrEmpty(sortBy) && !String.IsNullOrEmpty(sortValue)) result = SortList(result, sortBy, sortValue);
            if (!String.IsNullOrEmpty(orderBy)) result = OrderList(result, orderBy);

            return result;
        }

        public ColorPalette Add(ColorPalette ColorPalette)
        {
            cp_repository.Add(ColorPalette);
            return ColorPalette;
        }

        public ColorPalette Remove(int id)
        {
            s_repository.RemoveAllSavesForPalette(id);
            return cp_repository.Remove(id);
        }

        private List<ColorPaletteDto> SortList(List<ColorPaletteDto> list, string sortBy, string sortValue)
        {

            List<ColorPaletteDto> sorted = new List<ColorPaletteDto>();

            switch (sortBy)
            {
                case "name":
                    sorted = list.Where(s => s.Name.ToLower().Contains(sortValue.ToLower())).ToList();
                    return sorted;

                case "creator":
                    sorted = list.Where(s => s.CreatorName.ToLower().Contains(sortValue.ToLower())).ToList();
                    return sorted;

                case "min-saves":
                    int min = Int32.Parse(sortValue!);
                    Console.WriteLine(min);
                    sorted = list.Where(s => s.Saves >= min).ToList();
                    return sorted;

                case "max-saves":
                    int max = Int32.Parse(sortValue!);
                    sorted = list.Where(s => s.Saves <= max).ToList();
                    return sorted;

                default:
                    return list;
            }
        }

        private List<ColorPaletteDto> OrderList(List<ColorPaletteDto> list, string orderBy) {

            List<ColorPaletteDto> sorted = new List<ColorPaletteDto>();

            switch (orderBy) {
                case "name":
                    sorted = list.OrderBy(o => o.Name.ToLower()).ToList();
                    return sorted;

                case "name-desc":
                    sorted = list.OrderBy(o => o.Name.ToLower()).ToList();
                    sorted.Reverse();
                    return sorted;

                case "creator":
                    sorted = list.OrderBy(o => o.CreatorName.ToLower()).ToList();
                    return sorted;

                case "creator-desc":
                    sorted = list.OrderBy(o => o.CreatorName.ToLower()).ToList();
                    sorted.Reverse();
                    return sorted;

                case "least-saves":
                    sorted = list.OrderBy(o => o.Saves).ToList();
                    return sorted;

                case "most-saves":
                    sorted = list.OrderBy(o => o.Saves).ToList();
                    sorted.Reverse();
                    return sorted;

                case "new":
                    sorted = list.OrderBy(o => o.Id).ToList();
                    sorted.Reverse();
                    return sorted;

                case "old":
                    sorted = list.OrderBy(o => o.Id).ToList();
                    return sorted;

                default:
                    return list;
            }
        }
    }
}

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

        public async Task<IEnumerable<ColorPaletteDto>> GetColorPalettes(string orderBy, string sortBy, string sortValue)
        {
            var list = await cp_repository.ListAll();
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

        public async Task<IEnumerable<ColorPaletteDto>> GetColorPalettes(int userId, string orderBy, string sortBy, string sortValue)
        {
            var list = await cp_repository.ListNotOwn(userId);
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list)
            {
                var isSavedByUser = await s_repository.IsPaletteSavedByUser(palette.Id, userId);

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

        public async Task<IEnumerable<ColorPaletteDto>> GetPalettesByUser(int userId, int creatorId, string orderBy, string sortBy, string sortValue)
        {
            var list = await cp_repository.ListByUser(creatorId);
            var creatorName = (await u_repository.GetById(creatorId)).Name;
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list)
            {

                var isSavedByUser = await s_repository.IsPaletteSavedByUser(palette.Id, userId);

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
            if (!String.IsNullOrEmpty(orderBy)) result = OrderList(result, orderBy);

            return result;
        }

        public async Task<ColorPaletteDto> GetById(int userId, int id)
        {
            var result = await cp_repository.GetById(id);
            if (result == null) return null;

            var isSavedByUser = await s_repository.IsPaletteSavedByUser(result.Id, userId);

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

        public async Task<IEnumerable<ColorPaletteDto>> GetPalettesSavedByUser(int userId, string orderBy, string sortBy, string sortValue)
        {
            var user = await u_repository.GetById(userId);
            var list = new List<ColorPalette>();
            var result = new List<ColorPaletteDto>();

            foreach (var save in user.Saves)
            {
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

        public async Task<ColorPalette> Add(CreateColorPaletteDto colorPalette)
        {
            var palette = new ColorPalette()
            {
                Name = colorPalette.Name,
                Colors = colorPalette.Colors,
                CreatorId = colorPalette.CreatorId
            };

            var result = await cp_repository.Add(palette);
            if (result) return palette;
            else return null;
        }

        public async Task<ColorPalette> Remove(int id)
        {
            await s_repository.RemoveAllSavesForPalette(id);
            return await cp_repository.Remove(id);
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

        private List<ColorPaletteDto> OrderList(List<ColorPaletteDto> list, string orderBy)
        {

            List<ColorPaletteDto> ordered = new List<ColorPaletteDto>();

            switch (orderBy)
            {
                case "name":
                    ordered = list.OrderBy(o => o.Name.ToLower()).ToList();
                    return ordered;

                case "name-desc":
                    ordered = list.OrderBy(o => o.Name.ToLower()).ToList();
                    ordered.Reverse();
                    return ordered;

                case "creator":
                    ordered = list.OrderBy(o => o.CreatorName.ToLower()).ToList();
                    return ordered;

                case "creator-desc":
                    ordered = list.OrderBy(o => o.CreatorName.ToLower()).ToList();
                    ordered.Reverse();
                    return ordered;

                case "least-saves":
                    ordered = list.OrderBy(o => o.Saves).ToList();
                    return ordered;

                case "most-saves":
                    ordered = list.OrderBy(o => o.Saves).ToList();
                    ordered.Reverse();
                    return ordered;

                case "new":
                    ordered = list.OrderBy(o => o.Id).ToList();
                    ordered.Reverse();
                    return ordered;

                case "old":
                    ordered = list.OrderBy(o => o.Id).ToList();
                    return ordered;

                default:
                    return list;
            }
        }
    }
}

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

        public IEnumerable<ColorPaletteDto> GetColorPalettes(int userId)
        {
            var list = cp_repository.ListNotOwn(userId);
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list)
            {
                var saveCount = s_repository.ListSavesByPalette(palette.Id).Count();
                var isSavedByUser = s_repository.IsPaletteSavedByUser(palette.Id, userId);
                var creatorName = u_repository.GetById(palette.CreatorID).Name;

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = palette.CreatorID,
                    CreatorName = creatorName,
                    Saves = saveCount,
                    SavedByCurrentUser = isSavedByUser,
                });
            }
            return result;
        }

        public IEnumerable<ColorPaletteDto> GetPalettesByUser(int userId, int creatorId) {
            var list = cp_repository.ListByUser(creatorId);
            var creatorName = u_repository.GetById(creatorId).Name;
            var result = new List<ColorPaletteDto>();

            foreach (var palette in list) {
                var saveCount = s_repository.ListSavesByPalette(palette.Id).Count();
                var isSavedByUser = s_repository.IsPaletteSavedByUser(palette.Id, userId);

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = creatorId,
                    CreatorName = creatorName,
                    Saves = saveCount,
                    SavedByCurrentUser = isSavedByUser,
                });
            }
            return result;
        }

        public ColorPaletteDto GetById(int userId, int id)
        {
            var result = cp_repository.GetById(id);
            if (result == null) return null;

            var saveCount = s_repository.ListSavesByPalette(result.Id).Count();
            var isSavedByUser = s_repository.IsPaletteSavedByUser(result.Id, userId);
            var creatorName = u_repository.GetById(result.CreatorID).Name;

            return (new ColorPaletteDto()
            {
                Id = result.Id,
                Name = result.Name,
                Colors = result.Colors,
                CreatorId = result.CreatorID,
                CreatorName = creatorName,
                Saves = saveCount,
                SavedByCurrentUser = isSavedByUser,
            });
        }

        public IEnumerable<ColorPaletteDto> GetPalettesSavedByUser(int userId)
        {
            var saves = s_repository.ListSavesByUser(userId);
            var list = new List<ColorPalette>();
            var result = new List<ColorPaletteDto>();

            foreach (var save in saves) {
                list.Add(cp_repository.GetById(save.ColorPaletteID));
            }

            foreach (var palette in list)
            {
                var saveCount = s_repository.ListSavesByPalette(palette.Id).Count();
                var isSavedByUser = s_repository.IsPaletteSavedByUser(palette.Id, userId);
                var creatorName = u_repository.GetById(palette.CreatorID).Name;

                result.Add(new ColorPaletteDto()
                {
                    Id = palette.Id,
                    Name = palette.Name,
                    Colors = palette.Colors,
                    CreatorId = palette.CreatorID,
                    CreatorName = creatorName,
                    Saves = saveCount,
                    SavedByCurrentUser = isSavedByUser,
                });
            }
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
    }
}

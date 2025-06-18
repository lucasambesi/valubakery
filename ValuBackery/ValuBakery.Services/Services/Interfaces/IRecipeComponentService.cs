using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.DTOs;

namespace ValuBakery.Application.Services.Interfaces
{
    public interface IRecipeComponentService
    {
        Task<RecipeComponentDto?> GetByIdAsync(int id);
        Task<int> AddAsync(RecipeComponentDto dto);
        Task<List<RecipeComponentDto>> GetByRecipeIdAsync(int parentRecipeId);
        Task<bool> DeleteAsync(int parentRecipeId, int childRecipeId);
        Task<bool> UpdateAsync(RecipeComponentDto dto);
    }

}

using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Percistence.Percistence;
using ValuBakery.Percistence.Percistence.Interfaces;

namespace ValuBakery.Application.Services
{
    public class RecipeComponentService : IRecipeComponentService
    {
        private readonly IRecipeComponentDao _dao;
        private readonly IRecipeVariantDao _daoRecipeVariant;

        public RecipeComponentService(IRecipeComponentDao dao, IRecipeVariantDao daorecipe)
        {
            _dao = dao;
            _daoRecipeVariant = daorecipe;
        }

        public async Task<RecipeComponentDto?> GetByIdAsync(int id)
        {
            var recipe = await _dao.GetByIdAsync(id); 

            if(recipe != null)
            {
                recipe.ChildRecipeVariant = await _daoRecipeVariant.GetByIdAsync(recipe.ChildRecipeVariantId);
            }

            return recipe;
        }

        public async Task<int> AddAsync(RecipeComponentDto dto)
        {
            var entity = await _dao.GetByRecipesIdAsync(dto.ParentRecipeVariantId, dto.ChildRecipeVariantId);

            if (entity != null)
            {
                dto.Id = entity.Id;
                dto.IsDeleted = false;

                await _dao.UpdateAsync(dto);
                return entity.Id;
            }
            else
            {
                return await _dao.AddAsync(dto);
            }
        }

        public async Task<List<RecipeComponentDto>> GetByRecipeIdAsync(int parentRecipeId)
        {
            return await _dao.GetByRecipeIdAsync(parentRecipeId);
        }

        public async Task<bool> DeleteAsync(int parentRecipeId, int childRecipeId)
        {
            return await _dao.DeleteAsync(parentRecipeId, childRecipeId);
        }

        public async Task<bool> UpdateAsync(RecipeComponentDto dto)
        {
            return await _dao.UpdateAsync(dto);
        }
    }
}

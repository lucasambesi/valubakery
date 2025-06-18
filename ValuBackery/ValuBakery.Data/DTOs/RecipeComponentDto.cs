using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities;
using ValuBakery.Data.Enums;

namespace ValuBakery.Data.DTOs
{
    public class RecipeComponentDto
    {
        public int Id { get; set; }

        public int ParentRecipeVariantId { get; set; }
        public RecipeVariantDto ParentRecipeVariant { get; set; }

        public int ChildRecipeVariantId { get; set; }
        public RecipeVariantDto ChildRecipeVariant { get; set; }

        public decimal Quantity { get; set; }

        public string? ChildRecipeName { get; set; }

        public decimal GetCost()
        {
           return ChildRecipeVariant.GetCost() * Quantity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuBakery.Data.Entities;

namespace ValuBakery.Data.DTOs
{
    public class RecipeComponentDto
    {
        public int Id { get; set; }

        public int ParentRecipeId { get; set; }
        public RecipeDto ParentRecipe { get; set; }

        public int ChildRecipeId { get; set; }
        public RecipeDto ChildRecipe { get; set; }

        public decimal Quantity { get; set; }
    }
}

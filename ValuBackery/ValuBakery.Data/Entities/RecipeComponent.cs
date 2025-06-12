using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValuBakery.Data.Entities
{
    public class RecipeComponent
    {
        public int Id { get; set; }

        public int ParentRecipeId { get; set; }
        public Recipe ParentRecipe { get; set; }

        public int ChildRecipeId { get; set; }
        public Recipe ChildRecipe { get; set; }

        public decimal Quantity { get; set; }
    }
}

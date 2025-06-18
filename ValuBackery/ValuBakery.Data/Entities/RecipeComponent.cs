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

        public int ParentRecipeVariantId { get; set; }
        public RecipeVariant ParentRecipeVariant { get; set; }

        public int ChildRecipeVariantId { get; set; }
        public RecipeVariant ChildRecipeVariant { get; set; }

        public decimal Quantity { get; set; }
    }
}

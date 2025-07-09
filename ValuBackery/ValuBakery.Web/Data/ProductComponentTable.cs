using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.Enums;

namespace ValuBakery.Web.Data
{
    public class ProductComponentTable
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Total { get; set; }

        public decimal CostPerUnit { get; set; }

        public decimal Profit { get; set; }

        public decimal Quantity { get; set; }
        public ProductComponentType Type { get; set; }
    }
}

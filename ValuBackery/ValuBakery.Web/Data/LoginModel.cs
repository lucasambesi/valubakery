using System.ComponentModel.DataAnnotations;

namespace ValuBakery.Web.Data
{
    public class LoginModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

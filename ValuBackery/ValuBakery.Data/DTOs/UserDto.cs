using ValuBakery.Data.Entities.Common;

namespace ValuBakery.Data.DTOs
{
    public class UserDto : AuditableBaseEntity
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class ExchangeKey
    {
        public int Id { get; set; }
        public ulong P { get; set; }
        public ulong G { get; set; }
        public ulong ASecret { get; set; }
        public ulong BSecret { get; set; }
        public ulong CommonSecret { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
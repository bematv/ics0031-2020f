using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Cesar
    {
        public int Id { get; set; }
        public int Key { get; set; }
        public string PlainText { get; set; }
        public string CypherText { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}

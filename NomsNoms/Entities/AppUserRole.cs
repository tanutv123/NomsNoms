using Microsoft.AspNetCore.Identity;

namespace NomsNoms.Entities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        public AppUser AppUser{ get; set; }
        public AppRole AppRole{ get; set; }
    }
}

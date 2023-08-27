using Microsoft.AspNetCore.Identity;

namespace CoreWMS.Data
{
    public partial class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

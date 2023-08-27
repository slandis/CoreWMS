using Microsoft.AspNetCore.Identity;
using CoreWMS.Data;

namespace CoreWMS.Models
{
    public class RoleEdit
    {
        public ApplicationRole Role { get; set; }
        public string Description { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
    }
}
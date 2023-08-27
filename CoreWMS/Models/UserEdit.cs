using Microsoft.AspNetCore.Identity;
using CoreWMS.Data;

namespace CoreWMS.Models
{
    public class UserEdit
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<ApplicationRole> Members { get; set; }
        public IEnumerable<ApplicationRole> NonMembers { get; set; }
    }
}
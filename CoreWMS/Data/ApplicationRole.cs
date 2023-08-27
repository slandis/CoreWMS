using Microsoft.AspNetCore.Identity;

namespace CoreWMS.Data
{
    public partial class ApplicationRole : IdentityRole
    {
        public string Description { get; set; }
        public bool? IsEnabled { get; set; }

        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName, string description, bool? enabled) : base(roleName)
        {
            Name = roleName;
            Description = description;
            IsEnabled = enabled;
        }
    }
}

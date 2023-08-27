using CoreWMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using CoreWMS.Data;

namespace CoreWMS.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-user")]
    public class UserRolesTH : TagHelper
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public UserRolesTH(UserManager<ApplicationUser> usermgr, RoleManager<ApplicationRole> rolemgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("i-user")]
        public string User { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> roles = new List<string>();
            ApplicationUser user = await userManager.FindByIdAsync(User);

            if (user != null)
            {
                foreach (var role in roleManager.Roles)
                {
                    if (role != null && await userManager.IsInRoleAsync(user, role.Name))
                        roles.Add(role.Name);
                }
            }
            output.Content.SetContent(roles.Count == 0 ? "No Roles" : string.Join(", ", roles));
        }
    }
}
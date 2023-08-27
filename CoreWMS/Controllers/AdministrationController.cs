using CoreWMS.Data;
using CoreWMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreWMS.Controllers
{
    public class AdministrationController : Controller
    {
        private RoleManager<ApplicationRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<ApplicationRole> roleMgr, UserManager<ApplicationUser> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

        [Authorize(Roles = "CORE_ADMIN")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "CORE_ADMIN")]
        public IActionResult Roles()
        {
            return View();
        }

        [Authorize(Roles = "CORE_ADMIN")]
        public IActionResult CreateRole() => View();

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name, string description, bool? enabled)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new ApplicationRole(name, description, enabled));
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                else
                    Errors(result);
            }
            return View(name);
        }

        [Authorize(Roles = "CORE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            ApplicationRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Roles");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        [Authorize(Roles = "Adminm, CORE_ADMIN")]
        public async Task<IActionResult> UpdateRole(string id)
        {
            ApplicationRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();

            foreach (ApplicationUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Description = role.Description,
                Members = members,
                NonMembers = nonMembers
            }); ;
        }

        [Authorize(Roles = "CORE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    ApplicationUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }

                /* Update the custom description field now */
                ApplicationRole role = await roleManager.FindByIdAsync(model.RoleId);

                if (role != null)
                {
                    role.Description = model.Description;

                    await roleManager.UpdateAsync(role);
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Roles));
            else
                return await UpdateRole(model.RoleId);
        }

        [Authorize(Roles = "CORE_ADMIN")]
        public IActionResult Users()
        {
            return View();
        }

        public async Task<IActionResult> UpdateUser(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);

            List<ApplicationRole> members = new List<ApplicationRole>();
            List<ApplicationRole> nonMembers = new List<ApplicationRole>();

            foreach (ApplicationRole role in roleManager.Roles)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(role);
            }

            return View(new UserEdit
            {
                User = user,
                Members = members,
                NonMembers = nonMembers
            }); ;
        }

        [Authorize(Roles = "CORE_ADMIN")]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    foreach (string roleId in model.AddIds ?? new string[] { })
                    {
                        ApplicationRole role = await roleManager.FindByIdAsync(roleId);

                        if (role != null)
                        {
                            result = await userManager.AddToRoleAsync(user, role.Name);

                            if (!result.Succeeded)
                                Errors(result);
                        }
                    }

                    foreach (string roleId in model.DeleteIds ?? new string[] { })
                    {
                        ApplicationRole role = await roleManager.FindByIdAsync(roleId);

                        if (role != null)
                        {
                            result = await userManager.RemoveFromRoleAsync(user, role.Name);
                            if (!result.Succeeded)
                                Errors(result);
                        }
                    }

                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;

                    await userManager.UpdateAsync(user);
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Users));
            else
                return await UpdateUser(model.UserId);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.ControllerName = "Administration";
        }
    }
}

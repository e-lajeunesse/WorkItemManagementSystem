using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WIMS.Data;
using WIMS.Models.UserModels;

namespace WIMS.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        //Get: User/CreateRole
        public IActionResult CreateRole()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        //Post: User/CreateRole
        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRoleCreate model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "User");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);                
        }

        //GET: User/GetRoles
        //Gets list of all Roles
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult GetRoles()
        {
            var roles = _roleManager.Roles.Select(r => new RoleListItem
            {
                RoleId = r.Id,
                Name = r.Name
            });
            return View(roles);
        }

        
        //GET:User/EditRole
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditRole(string id)
        {
           var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View();
            }

            var model = new UserRoleEdit
            {
                RoleId = role.Id,
                RoleName = role.Name
                
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        //POST: User/EditRole
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditRole(UserRoleEdit model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.RoleId} cannot be found";
                return View();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }            
        }

        //GET: User/EditUsersInRole
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            ViewBag.roleName = role.Name;
            if (role == null)
            {
                return View();
            }

            var model = new List<UsersInRoleEdit>();
            foreach(var user in _userManager.Users)
            {
                var userModel = new UsersInRoleEdit
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userModel.IsSelected = true;
                }
                else
                {
                    userModel.IsSelected = false;
                }
                model.Add(userModel);                
            }
            return View(model);
        }

        //POST: User/EditUsersInRole
        // Assign or remove users from role
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UsersInRoleEdit> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ModelState.AddModelError("", "Role cannot be found");
                return View(model);
            }

            foreach(var userModel in model)
            {
                var user = await _userManager.FindByIdAsync(userModel.UserId);
                IdentityResult result = null;

                if (userModel.IsSelected && !(await _userManager.IsInRoleAsync(user,role.Name)))
                {
                    if (role.Name == "Manager")
                    {
                        user.IsManager = true;
                    }
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if ((!userModel.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name))))
                {
                    if (role.Name == "Manager")
                    {
                        user.IsManager = false;
                    }
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Update failed");
                    return View(model);
                }
            }
            return RedirectToAction("EditRole", new { id = roleId });
        }

        //GET: User/DeleteRole/{id}
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var roleListItem = new RoleListItem
            {
                RoleId = role.Id,
                Name = role.Name
            };
            return View(roleListItem);
        }

        //POST: User/DeleteRole/{id}
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("DeleteRole")]
        public async Task<IActionResult> DeleteRolePost(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("GetRoles");
            }
            return View();
        }
    }
}

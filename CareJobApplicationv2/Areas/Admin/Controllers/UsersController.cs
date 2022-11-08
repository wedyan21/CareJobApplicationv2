using CareJobApplicationv2.Areas.Admin.ViewModels;
using CareJobApplicationv2.Data;
using CareJobApplicationv2.Models;
using CareJobApplicationv2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CareJobApplicationv2.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("{area:exists}/[controller]/[action]")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
           // _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;

        }
        public IActionResult Index()
        {
           
            var ListOfUsers = _userManager.Users.Select(User => new UserViewModel
            {
                Id = User.Id,
                UserName = User.UserName,
                Email = User.Email,
                BranchId = User.BranchId,
                IsCareTeam = User.IsCareTeam,
                BranchName = User.Branchs.BranchTitleEn ,
                Roles = _userManager.GetRolesAsync(User).Result

            }).ToList();

            return View(ListOfUsers);
        }

        // Add New User
        [HttpGet]
        public async Task<IActionResult> AddNewUser()
        {
            ViewData["BranchsId"] = new SelectList(_context.Branchs, "Id", "BranchTitleEn");
           
            var roles = await _roleManager.Roles.Select(r=> new RoleViewModel { RoleId = r.Id, RoleName = r.Name}).ToListAsync();

            var viewModel = new AddUserViewModel
            {
                
                Roles = roles
            };
            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewUser(AddUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!model.Roles.Any(r => r.IsSelected))
             {
                ModelState.AddModelError("Roles", "Please Select at least one role");
                return View(model);
             }

            if(await _userManager.FindByEmailAsync(model.Email)!= null)
            {
                ModelState.AddModelError("Email", "Email is exists");
                return View(model);
            }

            var user = new ApplicationUser 
            { 
                UserName = model.Email, 
                Email = model.Email,
                IsCareTeam = model.IsCareTeam,
                BranchId = model.BranchId,
            };
           // bool SuperAdmin = Convert.ToBoolean(model.IsCareTeam.CheckState);
            //if(model.IsCareTeam)
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }

            // await _userManager.AddToRoleAsync(user, model.Roles.Where(r=> r.IsSelected).Select(n => n.RoleName).FirstOrDefault());
            var roleName = model.Roles.Where(x => x.IsSelected).Select(y => y.RoleName).ToList();

            if (roleName != null)
            {
                foreach (var roleN in roleName)
                {
                    result = await _userManager.AddToRoleAsync(user, roleN);
                }
            }

            return RedirectToAction(nameof(Index), "Users", new { area = "Admin" });

        }
        //User Roles Management
        [HttpGet]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();

            ViewData["BranchsId"] = new SelectList(_context.Branchs, "Id", "BranchTitleEn",user.BranchId);
            var viewModel = new EditUserViewModel
            {
                Id = userId,
                Email = user.Email,
                IsCareTeam= (bool)user.IsCareTeam, //(user.IsCareTeam == null ? false : user.IsCareTeam),
                BranchId = user.BranchId,
                
            };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
                return NotFound();
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
            if(userWithSameEmail != null && userWithSameEmail.Id != model.Id)
            {
                ModelState.AddModelError(string.Empty, "This Email is already assigned to another user");
                return View(model);
            }
            user.UserName = model.Email;
            user.Email = model.Email;
            user.IsCareTeam = model.IsCareTeam;
            user.BranchId = model.BranchId;

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index), "Users", new { area = "Admin" });

        }

        //User Roles Management
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();

            var viewModel = new UserRolesViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                Roles = roles.Select(role => new RoleViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()
            };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(UserRolesViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user == null)
                return NotFound();
            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in model.Roles)
            {
                if (userRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                if (!userRoles.Any(r => r == role.RoleName) && role.IsSelected)
                    await _userManager.AddToRoleAsync(user, role.RoleName);

            }

            return RedirectToAction(nameof(Index), "Users", new { area = "Admin" });

        }
    }
}

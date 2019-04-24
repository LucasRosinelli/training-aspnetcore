using AspNetCoreTraining.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ManageUsersController(UserManager<IdentityUser> userManager)
        {
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var administrators = (await this._userManager.GetUsersInRoleAsync("Administrator")).ToArray();
            var everyone = await this._userManager.Users.ToArrayAsync();

            var model = new ManagerUsersViewModel()
            {
                Administrators = administrators,
                Everyone = everyone
            };

            return this.View(model);
        }
    }
}
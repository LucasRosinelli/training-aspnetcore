using AspNetCoreTraining.Models.Dto;
using AspNetCoreTraining.Models.ViewModel;
using AspNetCoreTraining.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;
        private readonly UserManager<IdentityUser> _userManager;

        public ToDoController(IToDoItemService toDoItemService, UserManager<IdentityUser> userManager)
        {
            this._toDoItemService = toDoItemService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this._userManager.GetUserAsync(this.User);
            if (user == null)
            {
                return this.Challenge();
            }

            var items = await this._toDoItemService.GetIncompleteItemsAsync(user);

            var model = new ToDoViewModel()
            {
                Items = items
            };

            return this.View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(AddToDoItem newItem)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Index");
            }

            var successful = await this._toDoItemService.AddItemAsync(newItem);
            if (!successful)
            {
                return this.BadRequest(new { error = "Could not add item." });
            }

            return this.RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if (id == Guid.Empty)
            {
                return this.RedirectToAction("Index");
            }

            var successful = await this._toDoItemService.MarkDoneAsync(id);
            if (!successful)
            {
                return this.BadRequest(new { error = "Could not mark item as done." });
            }

            return this.RedirectToAction("Index");
        }
    }
}
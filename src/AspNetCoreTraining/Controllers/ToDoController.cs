using AspNetCoreTraining.Models.Dto;
using AspNetCoreTraining.Models.ViewModel;
using AspNetCoreTraining.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IToDoItemService _toDoItemService;

        public ToDoController(IToDoItemService toDoItemService)
        {
            this._toDoItemService = toDoItemService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await this._toDoItemService.GetIncompleteItemsAsync();

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
    }
}
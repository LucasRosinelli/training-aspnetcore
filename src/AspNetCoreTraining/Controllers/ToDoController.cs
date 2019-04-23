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

            return View(model);
        }
    }
}
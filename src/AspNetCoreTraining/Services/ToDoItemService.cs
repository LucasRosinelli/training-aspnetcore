using AspNetCoreTraining.Data;
using AspNetCoreTraining.Models.Database;
using AspNetCoreTraining.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Services
{
    public class ToDoItemService : IToDoItemService
    {
        private readonly ApplicationDbContext _context;

        public ToDoItemService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<ToDoItem[]> GetIncompleteItemsAsync()
        {
            return await this._context.Items
                .Where(i => i.IsDone == false)
                .ToArrayAsync();
        }
    }
}
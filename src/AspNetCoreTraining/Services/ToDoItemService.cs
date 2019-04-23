using AspNetCoreTraining.Data;
using AspNetCoreTraining.Models.Database;
using AspNetCoreTraining.Models.Dto;
using AspNetCoreTraining.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<bool> AddItemAsync(AddToDoItem newItem)
        {
            var toDoItem = new ToDoItem()
            {
                Id = Guid.NewGuid(),
                Title = newItem.Title,
                IsDone = false,
                DueAt = newItem.DueAt
            };

            this._context.Items.Add(toDoItem);

            var saveResult = await this._context.SaveChangesAsync();
            return saveResult == 1;
        }
    }
}
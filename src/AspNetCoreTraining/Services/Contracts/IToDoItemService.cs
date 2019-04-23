using AspNetCoreTraining.Models.Database;
using AspNetCoreTraining.Models.Dto;
using System;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemsAsync();
        Task<bool> AddItemAsync(AddToDoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
using AspNetCoreTraining.Models.Database;
using AspNetCoreTraining.Models.Dto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemsAsync(IdentityUser user);
        Task<bool> AddItemAsync(AddToDoItem newItem);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
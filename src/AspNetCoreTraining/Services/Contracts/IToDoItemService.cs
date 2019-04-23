using AspNetCoreTraining.Models.Database;
using System.Threading.Tasks;

namespace AspNetCoreTraining.Services.Contracts
{
    public interface IToDoItemService
    {
        Task<ToDoItem[]> GetIncompleteItemsAsync();
    }
}
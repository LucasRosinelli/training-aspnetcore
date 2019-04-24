using AspNetCoreTraining.Data;
using AspNetCoreTraining.Models.Dto;
using AspNetCoreTraining.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTraining.UnitTests
{
    public class ToDoItemServiceShould
    {
        [Fact]
        public async Task AddItemAsIncomplete()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_AddItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                var fakeUser = new IdentityUser()
                {
                    Id = "fake-000",
                    UserName = "fake@training.local"
                };

                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Is text working?"
                }, fakeUser);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);

                var item = await context.Items.FirstAsync();
                Assert.Equal("Is text working?", item.Title);
                Assert.False(item.IsDone);
                Assert.Null(item.DueAt);
            }
        }
    }
}
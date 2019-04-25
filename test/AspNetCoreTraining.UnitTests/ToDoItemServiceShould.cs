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

        [Fact]
        public async Task GetIncompleteItemsAssert()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                var fakeUser1 = new IdentityUser()
                {
                    Id = "fake-001",
                    UserName = "fake1@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 1 (completed)"
                }, fakeUser1);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 2"
                }, fakeUser1);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 3"
                }, fakeUser1);
                var fake1Items = await service.GetIncompleteItemsAsync(fakeUser1);
                await service.MarkDoneAsync(fake1Items[0].Id, fakeUser1);

                var fakeUser2 = new IdentityUser()
                {
                    Id = "fake-002",
                    UserName = "fake2@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 1"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 2 (completed)"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 3"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 4"
                }, fakeUser2);
                var fake2Items = await service.GetIncompleteItemsAsync(fakeUser2);
                await service.MarkDoneAsync(fake2Items[1].Id, fakeUser2);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                var fakeUser1 = new IdentityUser()
                {
                    Id = "fake-001",
                    UserName = "fake1@training.local"
                };
                var fake1Items = await service.GetIncompleteItemsAsync(fakeUser1);
                Assert.Equal(2, fake1Items.Length);

                var fakeUser2 = new IdentityUser()
                {
                    Id = "fake-002",
                    UserName = "fake2@training.local"
                };
                var fake2Items = await service.GetIncompleteItemsAsync(fakeUser2);
                Assert.Equal(3, fake2Items.Length);

                var fakeUser3 = new IdentityUser()
                {
                    Id = "fake-003",
                    UserName = "fake3@training.local"
                };
                var fake3Items = await service.GetIncompleteItemsAsync(fakeUser3);
                Assert.Empty(fake3Items);
            }
        }
    }
}
using AspNetCoreTraining.Data;
using AspNetCoreTraining.Models.Dto;
using AspNetCoreTraining.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
                    Id = "fake-001b",
                    UserName = "fake1b@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 1 (done)"
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
                    Id = "fake-002b",
                    UserName = "fake2b@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 1"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 2 (done)"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 3 (done)"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 4"
                }, fakeUser2);
                var fake2Items = await service.GetIncompleteItemsAsync(fakeUser2);
                await service.MarkDoneAsync(fake2Items[1].Id, fakeUser2);
                await service.MarkDoneAsync(fake2Items[2].Id, fakeUser2);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                var fakeUser1 = new IdentityUser()
                {
                    Id = "fake-001b",
                    UserName = "fake1b@training.local"
                };
                var fake1ItemsActual = await service.GetIncompleteItemsAsync(fakeUser1);
                var fake1ItemsExpected = await context.Items.
                    Where(i => i.IsDone == false && i.UserId == fakeUser1.Id).ToArrayAsync();
                Assert.Equal(fake1ItemsExpected.Length, fake1ItemsActual.Length);

                var fakeUser2 = new IdentityUser()
                {
                    Id = "fake-002b",
                    UserName = "fake2b@training.local"
                };
                var fake2ItemsActual = await service.GetIncompleteItemsAsync(fakeUser2);
                var fake2ItemsExpected = await context.Items.
                    Where(i => i.IsDone == false && i.UserId == fakeUser2.Id).ToArrayAsync();
                Assert.Equal(fake2ItemsExpected.Length, fake2ItemsActual.Length);

                var fakeUser3 = new IdentityUser()
                {
                    Id = "fake-003b",
                    UserName = "fake3b@training.local"
                };
                var fake3ItemsActual = await service.GetIncompleteItemsAsync(fakeUser3);
                var fake3ItemsExpected = await context.Items.
                    Where(i => i.IsDone == false && i.UserId == fakeUser3.Id).ToArrayAsync();
                Assert.Equal(fake3ItemsExpected.Length, fake3ItemsActual.Length);
            }
        }

        [Fact]
        public async Task MarkDoneAssert()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestAddItem").Options;

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                var fakeUser1 = new IdentityUser()
                {
                    Id = "fake-001c",
                    UserName = "fake1c@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 1"
                }, fakeUser1);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 2"
                }, fakeUser1);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 3"
                }, fakeUser1);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 1 - Item 4"
                }, fakeUser1);
                var fake1Items = await service.GetIncompleteItemsAsync(fakeUser1);
                await service.MarkDoneAsync(fake1Items[0].Id, fakeUser1);
                await service.MarkDoneAsync(fake1Items[1].Id, fakeUser1);
                await service.MarkDoneAsync(fake1Items[2].Id, fakeUser1);
                await service.MarkDoneAsync(fake1Items[3].Id, fakeUser1);

                var fakeUser2 = new IdentityUser()
                {
                    Id = "fake-002c",
                    UserName = "fake2c@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 1"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 2"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 3"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 4"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 5"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 2 - Item 6"
                }, fakeUser2);
                var fake2Items = await service.GetIncompleteItemsAsync(fakeUser2);
                await service.MarkDoneAsync(fake2Items[0].Id, fakeUser2);
                await service.MarkDoneAsync(fake2Items[3].Id, fakeUser2);
                await service.MarkDoneAsync(fake2Items[4].Id, fakeUser2);

                var fakeUser3 = new IdentityUser()
                {
                    Id = "fake-003c",
                    UserName = "fake3c@training.local"
                };
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 3 - Item 1"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 3 - Item 2"
                }, fakeUser2);
                await service.AddItemAsync(new AddToDoItem()
                {
                    Title = "Fake 3 - Item 3"
                }, fakeUser2);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var service = new ToDoItemService(context);

                // 100% done
                var fakeUser1 = new IdentityUser()
                {
                    Id = "fake-001c",
                    UserName = "fake1c@training.local"
                };
                var fake1Items = await context.Items.
                    Where(i => i.IsDone && i.UserId == fakeUser1.Id).ToArrayAsync();
                Assert.Equal(4, fake1Items.Length);

                // 50% done
                var fakeUser2 = new IdentityUser()
                {
                    Id = "fake-002c",
                    UserName = "fake2c@training.local"
                };
                var fake2Items = await context.Items.
                    Where(i => i.IsDone && i.UserId == fakeUser2.Id).ToArrayAsync();
                Assert.Equal(3, fake2Items.Length);

                // 0% done
                var fakeUser3 = new IdentityUser()
                {
                    Id = "fake-003c",
                    UserName = "fake3c@training.local"
                };
                var fake3Items = await context.Items.
                    Where(i => i.IsDone && i.UserId == fakeUser3.Id).ToArrayAsync();
                Assert.Empty(fake3Items);

                // 0% done - user not exists
                var fakeUser4 = new IdentityUser()
                {
                    Id = "fake-004c",
                    UserName = "fake4@training.local"
                };
                var fake4Items = await context.Items.
                    Where(i => i.IsDone && i.UserId == fakeUser4.Id).ToArrayAsync();
                Assert.Empty(fake4Items);
            }
        }
    }
}
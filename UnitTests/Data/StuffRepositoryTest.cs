using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Moq;
using MvsMyTest.Data;
using MvsMyTest.Models;
using Xunit;

namespace UnitTests.Data
{
    public class StuffRepositoryTest
    {
        private IStuffRepository _service;

        public StuffRepositoryTest()
        {
            var mockDb = new Mock<IMongoDatabase>();
            var mockContext = new Mock<MyDbContext>(mockDb.Object);
            _service = new StuffRepository(mockContext.Object);
        }

        [Fact]
        public async Task ThrowsInvalidStuffId()
        {
            var result = await _service.Get("-1");
            Assert.Null(result);
        }

        [Fact]
        public async Task AddStuffItem()
        {
            var item = new StuffItem
            {
                Name = "name1",
                Tags = new List<TagItem>
                {
                    new TagItem{ Value = "sience"},
                    new TagItem{ Value = "red"}
                }
            };

            await _service.Add(item);

            Assert.NotNull(item.Id);
        }

        // Проверяем обновление для используемых пропертей
        [Fact]
        public async Task UpdateStuffItem()
        {
            await AddStuffItem();
            var item = new StuffItem
            {
                Id = 1,
                Name = "Name",
                Tags = new List<TagItem>
                {
                    new TagItem{ Value = "grey"},
                    new TagItem{ Value = "white"}
                }
            };

            await _service.Update(item);

            Assert.Equal(item.Description, StuffItem.Undefined);

            item = new StuffItem
            {
                Id = 1,
                Description = "Description",
                Tags = new List<TagItem>
                {
                    new TagItem{ Value = "grey"},
                    new TagItem{ Value = "white"}
                }
            };

            await _service.Update(item);

            Assert.Equal(item.Name, StuffItem.Undefined);
        }

        [Fact]
        public async Task DeleteStuffItem()
        {
            await AddStuffItem();
            await _service.Remove("1");
        }
    }
}

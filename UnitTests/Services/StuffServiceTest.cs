using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MvsMyTest.Models;
using MvsMyTest.Services;
using Xunit;

namespace UnitTests.Services
{
    public class StuffServiceTest
    {
        private IStuffService _service;

        public StuffServiceTest()
        {
            var mockStuffContext = new Mock<StuffContext>();
            var mockTagItemContext = new Mock<TagItemContext>();
            _service = new StuffService(mockStuffContext.Object, mockTagItemContext.Object);
        }

        [Fact]
        public async Task ThrowsInvalidStuffId()
        {
            //var result = await _service.GetByIdAsync(1);
            await Assert.ThrowsAsync<System.ArgumentNullException>(async () =>
                await _service.GetByIdAsync(-1));
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

            await _service.UpdateAsync(item);

            Assert.Null(item.Id);
        }

        [Fact]
        public async Task UpdateStuffItem()
        {
            await AddStuffItem();
            var item = new StuffItem
            {
                Id = 1,
                Name = "name1",
                Tags = new List<TagItem>
                {
                    new TagItem{ Value = "grey"},
                    new TagItem{ Value = "white"}
                }
            };

            //await _service.UpdateAsync(item);
            await Assert.ThrowsAsync<System.ArgumentNullException>(async () =>
                await _service.UpdateAsync(item));
        }

        [Fact]
        public async Task DeleteStuffItem()
        {
            await AddStuffItem();
            //await _service.DeleteAsync(1);
            await Assert.ThrowsAsync<System.ArgumentNullException>(async () =>
                await _service.DeleteAsync(1));
        }
    }
}

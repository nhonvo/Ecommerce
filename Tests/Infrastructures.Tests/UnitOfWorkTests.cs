using Application;
using Domain.Tests;

namespace Infrastructures.Tests
{
    public class UnitOfWorkTests : SetupTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTests()
        {
            _unitOfWork = new UnitOfWork(_dbContext,
                                         _userRepository.Object,
                                         _bookRepository.Object,
                                         _customerRepository.Object,
                                         _productRepository.Object,
                                         _orderRepository.Object,
                                         _orderDetailRepository.Object);
        }

        // [Fact]
        // public async Task TestUnitOfWork()
        // {
        //     // arrange
        //     var mockData = _fixture.Build<Book>().CreateMany(10).ToList();

        //     _bookRepository.Setup(x => x.GetAsync()).ReturnsAsync(new Pagination<Book> { Items = mockData, TotalItemsCount = mockData.Count });

        //     // act
        //     var items = await _unitOfWork.BookRepository.GetAsync();

        //     // assert
        //     items.Should().BeEquivalentTo(mockData);
        // }

    }
}

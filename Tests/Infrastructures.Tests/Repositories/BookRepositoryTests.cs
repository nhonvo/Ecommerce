using Application.Repositories;
using Domain.Tests;
using Infrastructures.Repositories;

namespace Infrastructures.Tests.Repositories
{
    public class ProductRepositoryTests : SetupTest
    {
        private readonly IProductRepository _productRepository;
        public ProductRepositoryTests()
        {
            _productRepository = new ProductRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        // [Fact]
        // public async Task ProductRepository_Should_ReturnCorrectData()
        // {
        //     // arrange
        //     var mockData = _fixture.Build<Product>().CreateMany(10).ToList();
        //     await _dbContext.Product.AddRangeAsync(mockData);
        //     await _dbContext.SaveChangesAsync();

        //     // act
        //     var result = await _productRepository.GetAsync();

        //     // assert
        //     result.Should().BeEquivalentTo(mockData);
        // }
    }
}

using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;

namespace Infrastructures.Tests.Repositories
{
    public class BookRepositoryTests : SetupTest
    {
        private readonly IBookRepository _bookRepository;
        public BookRepositoryTests()
        {
            _bookRepository = new BookRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task BookRepository_Should_ReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<Product>().CreateMany(10).ToList();
            await _dbContext.Books.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _bookRepository.GetAsync();

            // assert
            result.Should().BeEquivalentTo(mockData);
        }
    }
}

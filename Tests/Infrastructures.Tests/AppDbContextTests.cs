using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Tests
{
    public class AppDbContextTests : SetupTest, IDisposable
    {
        [Fact]
        public async Task AppDbContext_BooksDbSetShouldReturnCorrectData()
        {

            var mockData = _fixture.Build<Book>().CreateMany(10).ToList();
            await _dbContext.Books.AddRangeAsync(mockData);
            
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.Books.ToListAsync();
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task AppDbContext_BooksDbSetShouldReturnEmptyListWhenNotHavingData()
        {
            var result = await _dbContext.Books.ToListAsync();
            result.Should().BeEmpty();
        }
    }
}

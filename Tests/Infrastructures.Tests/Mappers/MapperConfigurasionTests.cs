using Application.ViewModels.Book;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;

namespace Infrastructures.Tests.Mappers
{
    public class MapperConfigurasionTests : SetupTest
    {
        [Fact]
        public void TestMapper()
        {
            //arrange
            var bookMock = _fixture.Build<Book>().Create();

            //act
            var result = _mapperConfig.Map<BookResponse>(bookMock);

            //assert
            result.Id.Should().Be(bookMock.Id.ToString());
        }
    }
}

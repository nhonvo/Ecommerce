using Application.ViewModels.Product;
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
            var productMock = _fixture.Build<Product>().Create();

            //act
            var result = _mapperConfig.Map<ProductResponse>(productMock);

            //assert
            result.Id.Should().Be(productMock.Id.ToString());
        }
    }
}

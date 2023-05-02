using Application.Interfaces;
using Domain.Tests;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class ProductControllerTests : SetupTest
    {
        private readonly ProductController _productController;
        private Mock<IProductService> _productService;
        public ProductControllerTests()
        {
            _productController = new ProductController(_productService.Object);
        }

    }
}

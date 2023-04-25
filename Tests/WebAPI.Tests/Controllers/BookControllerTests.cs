using Domain.Tests;
using Moq;
using WebAPI.Controllers;
using Application.Interfaces;

namespace WebAPI.Tests.Controllers
{
    public class BookControllerTests : SetupTest
    {
        private readonly BookController _bookController;
        private Mock<IBookService> _bookService;
        public BookControllerTests()
        {
            _bookController = new BookController(_bookService.Object);
        }

       

    }
}

using Application.Interfaces;
using Application.Commons;
using Domain.Tests;
using Infrastructures.Services;
using Application.ViewModels.Book;
using Domain.Interfaces;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Moq;
using System.Linq.Expressions;
using Domain.Aggregate;
using AutoMapper;
using Application.Repositories;

namespace Application.Tests.Services
{
    public class BookServiceTests : SetupTest
    {
        private readonly IBookService _bookService;
        private readonly IEmailService _emailService;


        public BookServiceTests()
        {
            _bookService = new BookService(_unitOfWorkMock.Object, _emailService, _mapperConfig);
        }
        [Fact]
        public async Task GetBookPagingsion_ShouldReturnCorrectDataWithDefaultParametor()
        {
            // Arrange
            var expectedPageSize = 20;
            var expectedPageIndex = 0;

            // Act
            var result = await _bookService.GetAsync(expectedPageIndex, expectedPageSize);
            var okResult = result as ApiSuccessResult<Pagination<BookResponse>>;

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(okResult.ResultObject);
            Assert.Equal(expectedPageSize, okResult.ResultObject.PageSize);
            Assert.Equal(expectedPageIndex, okResult.ResultObject.PageIndex);
        }
        [Fact]
        public async Task AdvancedSearch_ShouldReturnCorrectData()
        {
            // Arrange
            var expectedPageSize = 20;
            var expectedPageIndex = 0;
            var searchRequest = new SearchRequest
            {
                Title = "Harry Potter",
                Author = "J.K. Rowling",
                StartPrice = 10,
                PublicationDate = new DateTime(2005, 6, 30),
                Genre = "Fantasy"
            };

            // Act
            var result = await _bookService.AdvancedSearch(searchRequest, expectedPageIndex, expectedPageSize);
            var okResult = result;
            var books = okResult.ResultObject;

            // Assert
            Assert.NotNull(books);
            Assert.Equal(expectedPageSize, books.PageSize);
            Assert.Equal(expectedPageIndex, books.PageIndex);
        }
        [Fact]
        public async Task AdvancedSearch_ShouldReturnCorrectDataWithValidRequest()
        {
            // Arrange
            var searchRequest = new SearchRequest
            {
                Title = "The Alchemist",
                Author = "Paulo Coelho",
                StartPrice = 15.99m,
                PublicationDate = new DateTime(2000, 1, 1),
                Genre = "Fiction"
            };
            var expectedPageSize = 20;
            var expectedPageIndex = 0;

            // Act
            var result = await _bookService.AdvancedSearch(searchRequest, expectedPageIndex, expectedPageSize);
            var okResult = result;
            var books = okResult.ResultObject;

            // Assert
            Assert.NotNull(books);
            Assert.Equal(expectedPageSize, books.PageSize);
            Assert.Equal(expectedPageIndex, books.PageIndex);
            Assert.Contains(books.Items, b => b.Title.Contains(searchRequest.Title)
                                              || b.Author.Contains(searchRequest.Author)
                                              || b.Price == searchRequest.StartPrice
                                              || b.PublicationDate == searchRequest.PublicationDate
                                              || b.Genre.Contains(searchRequest.Genre));
        }
        [Fact]
        public async Task SendMail_ShouldSendEmailToAllUsers()
        {
            // Arrange
            var book = new BookResponse
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Price = 9.99m,
                Inventory = 10,
                Image = "https://example.com/book.jpg"
            };
            var users = new List<User>
    {
        new User { Id = new Guid("c9fa80d6-dd5e-47aa-3eed-08db445f43e7"), Email = "fptvttnhon2017@gmail.com" },
        new User { Id = new Guid("c9fa80d6-dd5e-47aa-3eed-08db445f43e8"), Email = "fptvttnhon2018@gmail.com" },
        new User { Id = new Guid("c9fa80d6-dd5e-47aa-3eed-08db445f43e9"), Email = "fptvttnhon2019@gmail.com" },
    };
            var emailServiceMock = new Mock<IEmailService>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(uow => uow.UserRepository.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(users);
            var bookService = new BookService(unitOfWorkMock.Object, emailServiceMock.Object, _mapperConfig);

            // Act
            await bookService.SendMail(book);

            // Assert
            emailServiceMock.Verify(es => es.SendEmailAsync(It.IsAny<SendMailRequest>()), Times.Once);
            var expectedRecipients = users.Select(u => new ToRecipient { emailAddress = new EmailAddress { address = u.Email } }).ToList();
            emailServiceMock.Verify(es => es.SendEmailAsync(It.Is<SendMailRequest>(r =>
                r.message.toRecipients.SequenceEqual(expectedRecipients) &&
                r.message.body.content.Contains(book.Title) &&
                r.message.body.content.Contains(book.Author) &&
                r.message.body.content.Contains(book.Genre) &&
                r.message.body.content.Contains(book.Price.ToString()) &&
                r.message.body.content.Contains(book.Inventory.ToString()) &&
                r.message.body.content.Contains(book.Image)
            )), Times.Once);
        }
        [Fact]
        public async Task AddAsync_ShouldReturnApiSuccessResult_WithValidBook()
        {
            // Arrange
            var createBook = new CreateBook
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Price = 9.99m,
                Inventory = 10,
                Image = "http://testimage.com/image.jpg"
            };

            var mockMapper = new Mock<IMapper>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockBookRepository = new Mock<IBookRepository>();
            var mockEmailService = new Mock<IEmailService>();

            var book = new Product
            {
                Name = "Test Book",
                Price = 9.99m
            };

            var bookResponse = new BookResponse
            {
                Title = "Test Book",
                Author = "Test Author",
                Genre = "Test Genre",
                Price = 9.99m,
                Inventory = 10,
                Image = "http://testimage.com/image.jpg"
            };

            mockMapper.Setup(m => m.Map<Product>(createBook)).Returns(book);
            mockMapper.Setup(m => m.Map<BookResponse>(book)).Returns(bookResponse);

            mockUnitOfWork.Setup(u => u.BeginTransaction());
            mockUnitOfWork.Setup(u => u.BookRepository).Returns(mockBookRepository.Object);
            mockBookRepository.Setup(r => r.AddAsync(book));
            mockUnitOfWork.Setup(u => u.CommitAsync());

            var emailRequest = new SendMailRequest();
            var toRecipient = new ToRecipient
            {
                emailAddress = new EmailAddress
                {
                    address = "test@test.com"
                }
            };
            emailRequest.message.toRecipients = new List<ToRecipient> { toRecipient };
            emailRequest.message.body.content = It.IsAny<string>();
            mockEmailService.Setup(e => e.SendEmailAsync(emailRequest)).Verifiable();

            var bookService = new BookService(mockUnitOfWork.Object, mockEmailService.Object, mockMapper.Object);

            // Act
            var result = await bookService.AddAsync(createBook);

            // Assert
            // Assert.IsInstanceOf<ApiSuccessResult<BookResponse>>(result);
            Assert.Equal(bookResponse, result.ResultObject);
            mockEmailService.Verify(e => e.SendEmailAsync(emailRequest), Times.Once);
        }

    }
}

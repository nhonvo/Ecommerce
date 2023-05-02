using Application;
using Application.Interfaces;
using Application.Repositories;
using AutoFixture;
using AutoMapper;
using Infrastructures;
using Infrastructures.Mappers;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Domain.Tests
{
    public class SetupTest : IDisposable
    {
        protected readonly IMapper _mapperConfig;
        protected readonly Fixture _fixture;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<IClaimsService> _claimsServiceMock;
        protected readonly Mock<ICurrentTime> _currentTimeMock;
        protected readonly Mock<IBookRepository> _bookRepository;
        protected readonly Mock<ICustomerRepository> _customerRepository;
        protected readonly Mock<IOrderRepository> _orderRepository;
        protected readonly Mock<IOrderDetailRepository> _orderDetailRepository;
        protected readonly Mock<IProductRepository> _productRepository;

        protected readonly Mock<IUserRepository> _userRepository;
        protected readonly ApplicationDbContext _dbContext;

        public SetupTest()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfigurationsProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _claimsServiceMock = new Mock<IClaimsService>();
            _currentTimeMock = new Mock<ICurrentTime>();
            _userRepository = new Mock<IUserRepository>();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new ApplicationDbContext(options);

            _currentTimeMock.Setup(x => x.GetCurrentTime()).Returns(DateTime.UtcNow);
            _claimsServiceMock.Setup(x => x.CurrentUserId).Returns(Guid.Empty);

        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}

using Application.Interfaces;
using Domain.Interfaces;
using Domain.Tests;
using Infrastructures.Services;

namespace Application.Tests.Services
{
    public class CustomerServiceTests : SetupTest
    {
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;


        public CustomerServiceTests()
        {
            _customerService = new CustomerService(_unitOfWorkMock.Object, _mapperConfig);
        }

    }
}

using Application.Interfaces;
using Application.Commons;
using Domain.Tests;
using Infrastructures.Services;
using Application.ViewModels.Customer;
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

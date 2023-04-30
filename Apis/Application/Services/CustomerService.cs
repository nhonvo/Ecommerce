using Application;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructures.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
        }
    }
}

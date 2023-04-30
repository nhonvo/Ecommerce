using Application;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;

namespace Infrastructures.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public OrderDetailService(
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

using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels.UserViewModels;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Application.ViewModels.Product;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));

            // User
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<UserUpdateRequest, User>().ReverseMap();
            CreateMap<LoginResponse, User>().ReverseMap();
            CreateMap<RegisterRequest, User>().ReverseMap();

            // Customer
            CreateMap<Customer, CustomerResponse>().ReverseMap();
            CreateMap<CreateCustomer, Customer>().ReverseMap();
            CreateMap<UpdateCustomer, Customer>().ReverseMap();
            CreateMap<CustomerOrder, Order>().ReverseMap();


            // Order
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<CreateOrder, Order>().ReverseMap();
            CreateMap<UpdateOrder, Order>().ReverseMap();
            CreateMap<OrderDetails, OrderDetail>().ReverseMap();

            // Product
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();


        }
    }
}

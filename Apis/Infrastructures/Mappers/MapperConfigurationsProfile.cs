using Application.Commons;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.Product;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Entities;

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
            CreateMap<UpdateCustomerOrder, Order>().ReverseMap();


            // Order
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<CreateOrder, Order>().ReverseMap();
            CreateMap<UpdateOrder, Order>().ReverseMap();
            CreateMap<OrderDetails, OrderDetail>().ReverseMap();
            CreateMap<UpdateOrderDetail, OrderDetail>().ReverseMap();

            // Product
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();


        }
    }
}

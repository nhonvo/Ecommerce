using AutoMapper;
using Application.Commons;
using Domain.Entities;
using Application.ViewModels.UserViewModels;
using Application.ViewModels.Book;
using Application.ViewModels.Order;
using Application.ViewModels.Review;
using Application.ViewModels.WishList;

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

            // Book
            CreateMap<Product, BookResponse>().ReverseMap();
            CreateMap<CreateBook, Product>().ReverseMap();
            CreateMap<UpdateBook, Product>().ReverseMap();


        }
    }
}

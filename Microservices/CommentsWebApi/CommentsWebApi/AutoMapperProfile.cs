using AutoMapper;
using BLL.Models;
using CommentsWebApi.DTOs;
using DAL.Entities;

namespace CommentsWebApi
{
    public sealed class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Comment, CommentModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();

            CreateMap<CommentDTO, CommentModel>().ReverseMap();

            CreateMap<EmailDTO, EmailModel>().ReverseMap();

            CreateMap<UserDTO, UserModel>()
                .ForMember(dest => dest.AccessLevel, opt => opt.MapFrom(src=>Role.RegularUser));
        }
    }
}

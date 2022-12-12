using Application.DogInfo.Commands;
using AutoMapper;
using WebApi.DTOs.DogInfoDTOs;

namespace WebApi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateDogInfoDTO, CreateDogInfoCommand>().ReverseMap();
        }
    }
}

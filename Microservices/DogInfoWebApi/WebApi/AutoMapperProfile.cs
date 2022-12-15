using Application.DogInfo.Commands.CreateDogInfo;
using Application.DogInfo.Commands.DeleteDogInfo;
using Application.DogInfo.Commands.UpdateDogInfo;
using Application.DogInfo.DTOs;
using AutoMapper;
using Domain;
using WebApi.DTOs.DogInfoDTOs;

namespace WebApi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateDogInfoDTO, CreateDogInfoCommand>().ReverseMap();
            CreateMap<UpdateDogInfoDTO, UpdateDogInfoCommand>().ReverseMap();
            CreateMap<DogInfo, DogInfoLookupDTO>();
        }
    }
}

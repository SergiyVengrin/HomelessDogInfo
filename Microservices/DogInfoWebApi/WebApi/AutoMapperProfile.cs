using Application.DogInfo.Commands.CreateDogInfo;
using Application.DogInfo.Commands.DeleteDogInfo;
using Application.DogInfo.Commands.UpdateDogInfo;
using Application.DogInfo.DTOs;
using Application.Image.Commands.CreateImage;
using AutoMapper;
using Domain;
using WebApi.DTOs.DogInfoDTOs;
using WebApi.DTOs.ImageDTOs;

namespace WebApi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateDogInfoDTO, CreateDogInfoCommand>();
            CreateMap<UpdateDogInfoDTO, UpdateDogInfoCommand>();
            CreateMap<DogInfo, DogInfoLookupDTO>();

            CreateMap<CreateImageDTO, CreateImageCommand>();
        }
    }
}

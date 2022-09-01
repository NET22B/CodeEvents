using AutoMapper;
using CodeEvents.Api.Core.Entities;
using CodeEvents.Common.Dto;

namespace CodeEvents.Api.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CodeEvent, CodeEventDto>().ReverseMap();
            CreateMap<Lecture, LectureDto>().ReverseMap();
            CreateMap<Lecture, LectureCreateDto>().ReverseMap();

        }
    }
}

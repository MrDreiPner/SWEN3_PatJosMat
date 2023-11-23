using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NPaperless.REST.DTOs;
using NPaperless.BusinessLogic.Entities;

namespace NPaperless.BusinessLogic.Mappers
{
    internal class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<CreateTagRequest, TagBL>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color))
                .ForMember(dest => dest.IsInboxTag, opt => opt.MapFrom(src => src.IsInboxTag))
                .ForMember(dest => dest.MatchingAlgorithm, opt => opt.MapFrom(src => src.MatchingAlgorithm))
                .ForMember(dest => dest.Match, opt => opt.MapFrom(src => src.Match))
                .ForMember(dest => dest.IsInsensitive, opt => opt.MapFrom(src => src.IsInsensitive))
                .ReverseMap();
        }
    }
}

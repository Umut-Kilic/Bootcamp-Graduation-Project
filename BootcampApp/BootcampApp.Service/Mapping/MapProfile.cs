using AutoMapper;
using BootcampApp.Core.Models;
using BootcampApp.Core.ViewModels;

namespace BootcampApp.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<PostViewModel, Post>().ReverseMap();
        }
    }
}

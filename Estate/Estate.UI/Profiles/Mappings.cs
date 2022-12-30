using AutoMapper;
using EntityLayer.Entities;
using Estate.UI.Features.Advert.Commands;
using Estate.UI.Models;

namespace Estate.UI.Profiles
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<CreateAdvertCommand, AdvertModel>().ReverseMap();
            CreateMap<CreateAdvertCommand, Advert>().ReverseMap();
            CreateMap<AdvertModel, Advert>().ReverseMap();

        }
    }
}

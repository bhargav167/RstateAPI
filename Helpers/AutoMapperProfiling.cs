using System.Linq;
using AutoMapper;
using RstateAPI;
using RstateAPI.Entities;
using RstateAPI.Entities.Dtos;
using RstateAPI.Entities.Profile;

public class AutoMapperProfiling : Profile {
    public AutoMapperProfiling () {
        CreateMap<PhotoDtos, Images1> ();
    }
}
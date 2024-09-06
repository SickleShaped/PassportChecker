using AutoMapper;
using PassportChecker.Models;
using PassportChecker.Models.DbModels;

namespace PassportChecker.Configurations;

public class AppMapingProfile:Profile
{
    public AppMapingProfile()
    {
        CreateMap<PassportModel, Passport>().ReverseMap();
        CreateMap<ChangeModel, Change>().ReverseMap();
    }
}

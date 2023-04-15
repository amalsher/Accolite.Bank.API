using Accolite.Bank.Data.MsSql.Entities;
using Accolite.Bank.Services.Models;
using AutoMapper;

namespace Accolite.Bank.Services.Mapping;

public class MappingConfiguration : Profile
{
    public MappingConfiguration()
    {
        CreateMap<AccountEntity, Account>().ReverseMap();
        CreateMap<UserEntity, User>().ReverseMap();
    }
}

using AutoMapper;
using Eshop.RazorPage.Models.UserAddress;
using Eshop.RazorPage.ViewModels.Users.Addresses;

namespace Eshop.RazorPage.Infrastructure;

public class AutoMapperProfile:Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateUserAddressViewModel, CreateUserAddressCommand>().ReverseMap();
        CreateMap<EditUserAddressViewModel, EditUserAddressCommand>().ReverseMap();
        CreateMap<AddressDto, EditUserAddressViewModel>().ReverseMap();
    }
}
using AutoMapper;
using Sales.DTOs;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils
{
    public class MapperConfig: Profile
    {
       public MapperConfig()
        {
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<RoleDto, Role>().ReverseMap();


            CreateMap<VendorAddressDto, VendorAddress>().ReverseMap();
            CreateMap<VendorAddressDto, Address>().ReverseMap();

            CreateMap<VendorPhoneDto, Phone>().ReverseMap();
            CreateMap<VendorPhoneDto, VendorPhone>().ReverseMap();

            CreateMap<VendorBankAccountDto, BankAccount>().ReverseMap();
            CreateMap<VendorBankAccountDto, VendorBankAccount>().ReverseMap();

            
        }
    }
}

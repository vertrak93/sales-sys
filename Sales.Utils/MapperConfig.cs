﻿using AutoMapper;
using Sales.DTOs;
using Sales.DTOs.UserDtos;
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
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserRoleDto, UserRole>().ReverseMap();

            CreateMap<BankDto, Bank>().ReverseMap();
            CreateMap<BrandDto, Brand>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<PresentationDto, Presentation>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();
            CreateMap<SubCategoryDto, SubCategory>().ReverseMap();
            CreateMap<TelephonyDto, Telephony>().ReverseMap();

            CreateMap<UnitOfMeasureDto, UnitOfMeasure>().ReverseMap();


            CreateMap<VendorAddressDto, VendorAddress>().ReverseMap();
            CreateMap<VendorAddressDto, Address>().ReverseMap();

            CreateMap<VendorBankAccountDto, BankAccount>().ReverseMap();
            CreateMap<VendorBankAccountDto, VendorBankAccount>().ReverseMap();

            CreateMap<VendorPhoneDto, VendorPhone>().ReverseMap();
            CreateMap<VendorPhoneDto, Phone>().ReverseMap();

            CreateMap<VendorProductDto, VendorProduct>().ReverseMap();
           
            CreateMap<VendorDto, Vendor>().ReverseMap();
           
            
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AssetWebAPI.DTO;
using AssetWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetWebAPI.AutoMapper
{
    public class AutoMapperprofile : Profile  
    {
        public AutoMapperprofile()
        {
            CreateMap<Asset, ListDTO>().ForMember(p => p.ModelName, q => q.MapFrom(r => r.Model.Name))
                                        .ForMember(p => p.ColorName, q => q.MapFrom(r => r.Color.Name))
                                        .ForMember(p => p.ManufacturerName, q => q.MapFrom(r => r.Manufacturer.Name));
            CreateMap<ListDTO, Asset>().ForMember(x => x.ModelId, y => y.Ignore())
                                        .ForMember(x => x.ManuFactId, y => y.Ignore())
                                        .ForMember(x => x.ColorId, y => y.Ignore());


            CreateMap<ListDTO, ExcelDTO>().ForMember(p => p.ModelName, q => q.MapFrom(r => r.ModelName))
                            .ForMember(p => p.ColorName, q => q.MapFrom(r => r.ColorName))
                            .ForMember(p => p.ManufacturerName, q => q.MapFrom(r => r.ManufacturerName));

        }
          
    }
}

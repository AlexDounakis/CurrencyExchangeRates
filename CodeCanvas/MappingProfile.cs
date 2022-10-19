using AutoMapper;
using CodeCanvas.Entities;
using CodeCanvas.Models;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace CodeCanvas
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EuropeanCentralBank.CurrencyRate, Entities.CurrencyRateEntity>();

            CreateMap<CurrencyRateEntity ,CurrencyRateModel >();

        }
    }
}

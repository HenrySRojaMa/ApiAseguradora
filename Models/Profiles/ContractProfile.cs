using AutoMapper;
using Models.Entities;
using Models.Queries.Contratos;
using Models.Responses.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Profiles
{
    public class ContractProfile: Profile
    {
        public ContractProfile()
        {
            CreateMap<ContractQuery, Contrato>();
            CreateMap<Contrato, Insurer>()
                .ForMember(dest => dest.IdSeguro, opt => opt.MapFrom(src => src.IdSeguroNavigation.IdSeguro))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.IdSeguroNavigation.Nombre))
                ;
            CreateMap<Cliente, ContractResponse>()
                .ForMember(dest => dest.Aseguradoras, opt => opt.MapFrom(src => src.Contratos.ToList()))
                ;
        }
    }
}

using AutoMapper;
using Models.Entities;
using Models.Queries.Aseguradoras;
using Models.Responses.Aseguradoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Profiles
{
    public class InsurerProfile: Profile
    {
        public InsurerProfile()
        {
            CreateMap<InsurerQuery, Aseguradora>();
            CreateMap<Aseguradora, InsurerResponse>();
        }
    }
}

using AutoMapper;
using Models.Entities;
using Models.Queries.Clientes;
using Models.Responses.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Profiles
{
    public class ClientProfile: Profile
    {
        public ClientProfile()
        {
            CreateMap<ClientQuery, Cliente>();
            CreateMap<Cliente, ClientResponse>();
        }
    }
}

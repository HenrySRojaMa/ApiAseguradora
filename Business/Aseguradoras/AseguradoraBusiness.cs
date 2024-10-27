using AutoMapper;
using Data.Aseguradoras;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Queries.Aseguradoras;
using Models.Responses;
using Models.Responses.Aseguradoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Aseguradoras
{
    public class AseguradoraBusiness : IAseguradoraBusiness
    {
        private readonly IMapper _mapper;
        private readonly IAseguradoraMappings _aseguradoraMappings;
        public AseguradoraBusiness(IMapper mapper, IAseguradoraMappings aseguradoraMappings)
        {
            _mapper = mapper;
            _aseguradoraMappings = aseguradoraMappings;
        }

        public async Task<Response> ListInsurersBusiness(int? IdSeguro)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response = await _aseguradoraMappings.ListInsurersMapping(IdSeguro);
                response.Data = _mapper.Map<List<InsurerResponse>>(response.Data);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurersBusiness));
            }
            return response;
        }
        public async Task<Response> SetInsurerBusiness(InsurerQuery aseguradora)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                response = await _aseguradoraMappings.SetInsurerMapping(_mapper.Map<Aseguradora>(aseguradora));
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetInsurerBusiness));
            }
            return response;
        }
        public async Task<Response> DeleteInsurerBusiness(int IdSeguro)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                response = await _aseguradoraMappings.DeleteInsurerMapping(IdSeguro);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteInsurerBusiness));
            }
            return response;
        }
    }
}

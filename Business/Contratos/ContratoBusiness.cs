using AutoMapper;
using Data.Contratos;
using Models.Entities;
using Models.Queries.Clientes;
using Models.Responses.Clientes;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Queries.Contratos;
using Models.Responses.Aseguradoras;
using Models.Responses.Contratos;
using Data;

namespace Business.Contratos
{
    public class ContratoBusiness : IContratoBusiness
    {
        private readonly IMapper _mapper;
        private readonly IContratoMappings _contratoMappings;
        private readonly ITransactionMappings _transactionMappings;
        public ContratoBusiness(IMapper mapper, IContratoMappings contratoMappings, ITransactionMappings transactionMappings)
        {
            _mapper = mapper;
            _contratoMappings = contratoMappings;
            _transactionMappings = transactionMappings;
        }

        public async Task<Response> ListClientContractsBusiness(string cedula)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response = await _contratoMappings.ListClientContractsMapping(cedula);
                response.Data = _mapper.Map<List<InsurerResponse>>(response.Data);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientContractsBusiness));
            }
            return response;
        }
        public async Task<Response> ListInsurerClientsBusiness(string Codigo)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response = await _contratoMappings.ListInsurerClientsMapping(Codigo);
                response.Data = _mapper.Map<List<ClientResponse>>(response.Data);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurerClientsBusiness));
            }
            return response;
        }
        public async Task<Response> ListContractsBusiness()
        {
            var response = RspHandler.OkQuery();
            try
            {
                response = await _contratoMappings.ListContractsMapping();
                response.Data = _mapper.Map<List<ContractResponse>>(response.Data);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListContractsBusiness));
            }
            return response;
        }
        public async Task<Response> SetContractBusiness(List<ContractQuery> contrato)
        {
            var response = RspHandler.OkTransaction();
            bool next = true;
            try
            {
                response = await _transactionMappings.BeginTransaction();
                if (response.Code != MsgCd.Error)
                {
                    foreach (var item in contrato)
                    {
                        response = await _contratoMappings.SetContractMapping(_mapper.Map<Contrato>(item));
                        if (response.Code == MsgCd.Error)
                        {
                            next = false;
                            break;
                        }
                    }
                    if (next) await _transactionMappings.CommitTransaction();
                    else await _transactionMappings.RollbackTransaction();
                }
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetContractBusiness));
            }
            return response;
        }
        public async Task<Response> DeleteContractBusiness(int IdContrato)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                response = await _contratoMappings.DeleteContractMapping(IdContrato);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteContractBusiness));
            }
            return response;
        }
    }
}

using Models.Entities;
using Models.Responses;

namespace Data.Contratos
{
    public interface IContratoMappings
    {
        Task<Response> DeleteContractMapping(int IdContrato);
        Task<Response> ListClientContractsMapping(string cedula);
        Task<Response> ListContractsMapping();
        Task<Response> ListInsurerClientsMapping(string Codigo);
        Task<Response> SetContractMapping(Contrato contrato);
    }
}
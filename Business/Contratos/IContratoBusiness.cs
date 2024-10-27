using Models.Queries.Contratos;
using Models.Responses;

namespace Business.Contratos
{
    public interface IContratoBusiness
    {
        Task<Response> DeleteContractBusiness(int IdContrato);
        Task<Response> ListClientContractsBusiness(string cedula);
        Task<Response> ListContractsBusiness();
        Task<Response> ListInsurerClientsBusiness(string Codigo);
        Task<Response> SetContractBusiness(List<ContractQuery> contrato);
    }
}
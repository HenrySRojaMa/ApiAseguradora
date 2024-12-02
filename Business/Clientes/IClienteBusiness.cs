using Microsoft.AspNetCore.Http;
using Models.Queries.Clientes;
using Models.Responses;

namespace Business.Clientes
{
    public interface IClienteBusiness
    {
        Task<Response> DeleteClientBusiness(int IdSeguro);
        Task<Response> ImportClientsBusiness(IFormFile file);
        Task<Response> ListClientsBusiness(ClientListFilter query);
        Task<Response> SetClientBusiness(ClientQuery cliente);
    }
}
using Models.Entities;
using Models.Queries.Clientes;
using Models.Responses;

namespace Data.Clientes
{
    public interface IClienteMappings
    {
        Task<Response> DeleteClientMapping(int IdCliente);
        Task<Response> ListClientsMapping(ClientListFilter query);
        Task<Response> SetClientMapping(Cliente cliente);
    }
}
using Models.Entities;
using Models.Responses;

namespace Data.Clientes
{
    public interface IClienteMappings
    {
        Task<Response> DeleteClientMapping(int IdCliente);
        Task<Response> ListClientsMapping(int? IdCliente);
        Task<Response> SetClientMapping(Cliente cliente);
    }
}
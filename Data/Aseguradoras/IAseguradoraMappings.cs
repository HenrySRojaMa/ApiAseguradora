using Models.Entities;
using Models.Responses;

namespace Data.Aseguradoras
{
    public interface IAseguradoraMappings
    {
        Task<Response> DeleteInsurerMapping(int IdSeguro);
        Task<Response> ListInsurersMapping(int? IdSeguro);
        Task<Response> SetInsurerMapping(Aseguradora aseguradora);
    }
}
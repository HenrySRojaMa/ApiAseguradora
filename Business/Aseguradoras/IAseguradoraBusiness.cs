using Models.Queries.Aseguradoras;
using Models.Responses;

namespace Business.Aseguradoras
{
    public interface IAseguradoraBusiness
    {
        Task<Response> DeleteInsurerBusiness(int IdSeguro);
        Task<Response> ListInsurersBusiness(int? IdSeguro);
        Task<Response> SetInsurerBusiness(InsurerQuery aseguradora);
    }
}
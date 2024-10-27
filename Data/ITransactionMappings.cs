using Models.Responses;

namespace Data
{
    public interface ITransactionMappings
    {
        Task<Response> BeginTransaction();
        Task<Response> CommitTransaction();
        Task<Response> RollbackTransaction();
    }
}
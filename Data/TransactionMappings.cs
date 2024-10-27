using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TransactionMappings : ITransactionMappings
    {
        private readonly SegurosContext _context;
        public TransactionMappings(SegurosContext context)
        {
            this._context = context;
        }

        public async Task<Response> BeginTransaction()
        {
            var response = RspHandler.OkTransaction();
            try
            {
                await _context.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(BeginTransaction));
            }
            return response;
        }
        public async Task<Response> CommitTransaction()
        {
            var response = RspHandler.OkTransaction();
            try
            {
                await _context.Database.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(CommitTransaction));
            }
            return response;
        }
        public async Task<Response> RollbackTransaction()
        {
            var response = RspHandler.OkTransaction();
            try
            {
                await _context.Database.RollbackTransactionAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(RollbackTransaction));
            }
            return response;
        }
    }
}

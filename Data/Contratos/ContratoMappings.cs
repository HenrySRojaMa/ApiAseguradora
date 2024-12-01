using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contratos
{
    public class ContratoMappings : IContratoMappings
    {
        private readonly SegurosContext _context;
        public ContratoMappings(SegurosContext context)
        {
            _context = context;
        }

        public async Task<Response> SetContractMapping(Contrato contrato)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                int IdContrato = 0;
                try { IdContrato = await _context.Contratos.Where(x => x.IdCliente == contrato.IdCliente && x.IdSeguro == contrato.IdSeguro).Select(x => x.IdContrato).FirstAsync(); } catch (Exception) { }
                if (IdContrato == 0)
                {
                    try { IdContrato = await _context.Contratos.MaxAsync(x => x.IdContrato); } catch (Exception) { }
                    IdContrato++;
                    contrato.IdContrato = IdContrato;
                    contrato.Estado = "A";
                    await _context.Contratos.AddAsync(contrato);
                }
                else
                {
                    var temp = await _context.Contratos.FindAsync(IdContrato);
                    temp.Estado = "A";
                    _context.Entry(temp).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                response.Data = IdContrato;
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetContractMapping));
            }
            return response;
        }

        public async Task<Response> ListClientContractsMapping(string cedula)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response.Data = await _context.Aseguradoras//.Include(a => a.Contratos).ThenInclude(c => c.IdClienteNavigation)
                    .Where(a => a.Estado == "A" && a.Contratos.Any(co => co.Estado == "A" && co.IdClienteNavigation.Cedula == cedula))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientContractsMapping));
            }
            return response;
        }

        public async Task<Response> ListInsurerClientsMapping(string Codigo)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response.Data = await _context.Clientes//.Include(a => a.Contratos).ThenInclude(c => c.IdClienteNavigation)
                    .Where(c => c.Estado == "A" && c.Contratos.Any(co => co.Estado == "A" && co.IdSeguroNavigation.Codigo == Codigo))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurerClientsMapping));
            }
            return response;
        }

        public async Task<Response> ListContractsMapping()
        {
            var response = RspHandler.OkQuery();
            try
            {
                response.Data = await _context.Clientes
                    .Include(c => c.Contratos.Where(co => co.Estado == "A" && (co.IdSeguroNavigation != null ? co.IdSeguroNavigation.Estado : "") == "A"))
                    .ThenInclude(co => co.IdSeguroNavigation)
                    .Where(c => c.Estado == "A")
                    .ToListAsync();

                /*response.Data = await (from cl in _context.Clientes
                                 join co in _context.Contratos on cl.IdCliente equals co.IdCliente
                                 join a in _context.Aseguradoras on co.IdSeguro equals a.IdSeguro
                                 where cl.Estado == "A" && co.Estado == "A" && a.Estado == "A"
                                 select cl).Include(c => c.Contratos).ThenInclude(co => co.IdSeguroNavigation)
                                 .ToListAsync();*/
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListContractsMapping));
            }
            return response;
        }

        public async Task<Response> DeleteContractMapping(int IdContrato)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                var temp = await _context.Contratos.FindAsync(IdContrato);
                temp.Estado = "I";
                _context.Entry(temp).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteContractMapping));
            }
            return response;
        }

    }
}

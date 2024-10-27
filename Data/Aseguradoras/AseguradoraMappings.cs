using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Aseguradoras
{
    public class AseguradoraMappings : IAseguradoraMappings
    {
        private readonly SegurosContext _context;
        public AseguradoraMappings(SegurosContext context)
        {
            _context = context;
        }

        public async Task<Response> ListInsurersMapping(int? IdSeguro)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response.Data = await _context.Aseguradoras
                    .Where(x => x.Estado == "A"
                    && x.IdSeguro == (IdSeguro != null ? IdSeguro : x.IdSeguro))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurersMapping));
            }
            return response;
        }
        public async Task<Response> SetInsurerMapping(Aseguradora aseguradora)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                int IdSeguro = 0;
                try { IdSeguro = await _context.Aseguradoras.Where(x => x.Codigo == aseguradora.Codigo).Select(x => x.IdSeguro).FirstAsync(); } catch (Exception) { }
                if (IdSeguro == 0)
                {
                    try { IdSeguro = await _context.Aseguradoras.MaxAsync(x => x.IdSeguro); } catch (Exception) { }
                    IdSeguro++;
                    aseguradora.IdSeguro = IdSeguro;
                    aseguradora.Estado = "A";
                    await _context.Aseguradoras.AddAsync(aseguradora);
                }
                else
                {
                    var temp = await _context.Aseguradoras.FindAsync(IdSeguro);
                    temp.Nombre = aseguradora.Nombre != null ? aseguradora.Nombre : temp.Nombre;
                    temp.Cobertura = aseguradora.Cobertura != null ? aseguradora.Cobertura : temp.Cobertura;
                    temp.Prima = aseguradora.Prima != null ? aseguradora.Prima : temp.Prima;
                    _context.Entry(temp).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                response.Data = IdSeguro;
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetInsurerMapping));
            }
            return response;
        }
        public async Task<Response> DeleteInsurerMapping(int IdSeguro)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                var temp = await _context.Aseguradoras.FindAsync(IdSeguro);
                temp.Estado = "I";
                _context.Entry(temp).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteInsurerMapping));
            }
            return response;
        }

    }
}

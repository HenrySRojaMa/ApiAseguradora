using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Clientes
{
    public class ClienteMappings : IClienteMappings
    {
        private readonly SegurosContext _context;
        public ClienteMappings(SegurosContext context)
        {
            _context = context;
        }

        public async Task<Response> ListClientsMapping(int? IdCliente)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response.Data = await _context.Clientes
                    .Where(x => x.Estado == "A"
                    && x.IdCliente == (IdCliente != null ? IdCliente : x.IdCliente))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientsMapping));
            }
            return response;
        }
        public async Task<Response> SetClientMapping(Cliente cliente)
        {
            if (cliente.Cedula == "" || cliente.Cedula.Length != 10)
            {
                return new() { Code = MsgCd.Error, Message = "Identificacion invalida" };
            }
            var response = RspHandler.OkTransaction();
            try
            {
                int IdCliente = 0;
                try { IdCliente = await _context.Clientes.Where(x => x.Cedula == cliente.Cedula).Select(x => x.IdCliente).FirstAsync(); } catch (Exception) { }
                if (IdCliente == 0)
                {
                    try { IdCliente = await _context.Clientes.MaxAsync(x => x.IdCliente); } catch (Exception) { }
                    IdCliente++;
                    cliente.IdCliente = IdCliente;
                    cliente.Estado = "A";
                    await _context.Clientes.AddAsync(cliente);
                }
                else
                {
                    var temp = await _context.Clientes.FindAsync(IdCliente);
                    temp.Nombre = cliente.Nombre != null ? cliente.Nombre : temp.Nombre;
                    temp.Telefono = cliente.Telefono != null ? cliente.Telefono : temp.Telefono;
                    temp.Edad = cliente.Edad != null ? cliente.Edad : temp.Edad;
                    _context.Entry(temp).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                response.Data = IdCliente;
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetClientMapping));
            }
            return response;
        }
        public async Task<Response> DeleteClientMapping(int IdCliente)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                var temp = await _context.Clientes.FindAsync(IdCliente);
                temp.Estado = "I";
                _context.Entry(temp).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteClientMapping));
            }
            return response;
        }

    }
}

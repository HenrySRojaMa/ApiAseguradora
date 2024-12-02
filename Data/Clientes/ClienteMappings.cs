using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Queries.Clientes;
using Models.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<Response> ListClientsMapping1(ClientListFilter query)
        {
            var response = RspHandler.OkQuery();
            try
            {
                List<Cliente> clientes = await _context.Clientes
                    .Where(x => x.Estado == "A"
                    && x.IdCliente == (query.IdCliente != null ? query.IdCliente : x.IdCliente)
                    //&& x.Cedula == (query.Cedula != null ? query.Cedula : x.Cedula)
                    //&& x.Nombre == (query.Nombre != null ? query.Nombre : x.Nombre)
                    //The data types varchar(10) encrypted with (encryption_type = 'DETERMINISTIC', encryption_algorithm_name = 'AEAD_AES_256_CBC_HMAC_SHA_256', column_encryption_key_name = 'CEK-HSRM-2024', column_encryption_key_database_name = 'Seguros') collation_name = 'Latin1_General_BIN2' and varchar are incompatible in the like operator.",
                    //&& x.Cedula.Contains(query.Cedula != null ? query.Cedula : x.Cedula)
                    //&& x.Nombre.Contains(query.Nombre != null ? query.Nombre : x.Nombre)
                    ).ToListAsync();
                clientes = clientes.Where(x =>
                    (x.Cedula ?? "").Contains(query.Cedula != null ? query.Cedula : (x.Cedula ?? ""), StringComparison.OrdinalIgnoreCase)
                    && (x.Nombre ?? "").Contains(query.Nombre != null ? query.Nombre : (x.Nombre ?? ""), StringComparison.OrdinalIgnoreCase)
                ).ToList();
                response.Data = clientes;
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientsMapping));
            }
            return response;
        }
        public async Task<Response> ListClientsMapping(ClientListFilter query)
        {
            Response response = await _context.ConnectDatabaseAsync();
            if (response.Code != "00") return response;
            SqlConnection connection = (SqlConnection)response.Data;
            try
            {
                List<Cliente> clientes = new();
                SqlCommand command = new("sp_listar_clientes", connection);
                command.Parameters.Add(new SqlParameter("@IdCliente", SqlDbType.Int)).Value = query.IdCliente;
                command.Parameters.Add(new SqlParameter("@Cedula", SqlDbType.VarChar)).Value = query.Cedula;
                command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar)).Value = query.Nombre;
                command.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new();
                dataAdapter.Fill(dataSet);

                string objetoSerializado = JsonConvert.SerializeObject(dataSet.Tables[0]);
                clientes = JsonConvert.DeserializeObject<List<Cliente>>(objetoSerializado);

                clientes = clientes.Where(x =>
                    (x.Cedula ?? "").Contains(query.Cedula != null ? query.Cedula : (x.Cedula ?? ""), StringComparison.OrdinalIgnoreCase)
                    && (x.Nombre ?? "").Contains(query.Nombre != null ? query.Nombre : (x.Nombre ?? ""), StringComparison.OrdinalIgnoreCase)
                ).ToList();

                response.Data = clientes;
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientsMapping));
            }
            finally { if (connection.State > 0) await connection.CloseAsync(); }
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

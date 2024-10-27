using AutoMapper;
using Data.Clientes;
using Microsoft.AspNetCore.Http;
using Models.Entities;
using Models.Queries.Clientes;
using Models.Responses;
using Models.Responses.Clientes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace Business.Clientes
{
    public class ClienteBusiness : IClienteBusiness
    {
        private readonly IMapper _mapper;
        private readonly IClienteMappings _clienteMappings;
        public ClienteBusiness(IMapper mapper, IClienteMappings clienteMappings)
        {
            _mapper = mapper;
            _clienteMappings = clienteMappings;
        }

        public async Task<Response> ListClientsBusiness(int? IdSeguro)
        {
            var response = RspHandler.OkQuery();
            try
            {
                response = await _clienteMappings.ListClientsMapping(IdSeguro);
                response.Data = _mapper.Map<List<ClientResponse>>(response.Data);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientsBusiness));
            }
            return response;
        }
        public async Task<Response> SetClientBusiness(ClientQuery cliente)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                response = await _clienteMappings.SetClientMapping(_mapper.Map<Cliente>(cliente));
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetClientBusiness));
            }
            return response;
        }
        public async Task<Response> DeleteClientBusiness(int IdSeguro)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                response = await _clienteMappings.DeleteClientMapping(IdSeguro);
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteClientBusiness));
            }
            return response;
        }
        public async Task<Response> ImportClientsBusiness(IFormFile file)
        {
            var response = RspHandler.OkTransaction();
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (MemoryStream stream = new MemoryStream())
                {
                    file.CopyTo(stream);

                    using (ExcelPackage excelPackage = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets[0];
                        if (worksheet != null)
                        {
                            int totalRows = worksheet.Dimension.Rows;

                            for (int i = 2; i <= totalRows; i++)
                            {
                                await _clienteMappings.SetClientMapping(new()
                                {
                                    Cedula = (worksheet.Cells[i, 1].Value ?? "").ToString(),
                                    Nombre = (worksheet.Cells[i, 2].Value ?? "").ToString(),
                                    Telefono = (worksheet.Cells[i, 3].Value ?? "").ToString(),
                                    Edad = (worksheet.Cells[i, 4].Value ?? "").ToString() == "" ? null : int.Parse((worksheet.Cells[i, 4].Value ?? "").ToString())
                                });
                            }
                        }
                        else
                        {
                            response.Code = MsgCd.Error;
                            response.Message = "Error al cargar el excel";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ImportClientsBusiness));
            }
            return response;
        }

    }
}

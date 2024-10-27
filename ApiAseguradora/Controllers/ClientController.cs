using Business.Clientes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Queries.Clientes;
using Models.Responses;

namespace ApiAseguradora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClienteBusiness _clienteBusiness;
        public ClientController(IClienteBusiness clienteBusiness)
        {
            _clienteBusiness = clienteBusiness;
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteClientController(int IdSeguro)
        {
            try
            {
                return Ok(await _clienteBusiness.DeleteClientBusiness(IdSeguro));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteClientController)));
            }
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportClientsController(IFormFile file)
        {
            try
            {
                return Ok(await _clienteBusiness.ImportClientsBusiness(file));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ImportClientsController)));
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> ListClientsController(int? IdSeguro)
        {
            try
            {
                return Ok(await _clienteBusiness.ListClientsBusiness(IdSeguro));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientsController)));
            }
        }
        [HttpPost("set")]
        public async Task<IActionResult> SetClientController(ClientQuery cliente)
        {
            try
            {
                return Ok(await _clienteBusiness.SetClientBusiness(cliente));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetClientController)));
            }
        }
    }
}

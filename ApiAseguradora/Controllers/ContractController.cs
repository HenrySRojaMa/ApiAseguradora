using Business.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Queries.Contratos;
using Models.Responses;

namespace ApiAseguradora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContratoBusiness _contratoBusiness;
        public ContractController(IContratoBusiness contratoBusiness)
        {
            _contratoBusiness = contratoBusiness;
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteContractController(int IdContrato)
        {
            try
            {
                return Ok(await _contratoBusiness.DeleteContractBusiness(IdContrato));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteContractController)));
            }
        }
        [HttpGet("ListClientContracts")]
        public async Task<IActionResult> ListClientContractsController(string cedula)
        {
            try
            {
                return Ok(await _contratoBusiness.ListClientContractsBusiness(cedula));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListClientContractsController)));
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> ListContractsController()
        {
            try
            {
                return Ok(await _contratoBusiness.ListContractsBusiness());
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListContractsController)));
            }
        }
        [HttpGet("ListInsurerClients")]
        public async Task<IActionResult> ListInsurerClientsController(string Codigo)
        {
            try
            {
                return Ok(await _contratoBusiness.ListInsurerClientsBusiness(Codigo));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurerClientsController)));
            }
        }
        [HttpPost("set")]
        public async Task<IActionResult> SetContractController(List<ContractQuery> contrato)
        {
            try
            {
                return Ok(await _contratoBusiness.SetContractBusiness(contrato));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetContractController)));
            }
        }
    }
}

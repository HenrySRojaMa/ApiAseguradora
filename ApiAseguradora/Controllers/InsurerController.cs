using Business.Aseguradoras;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Queries.Aseguradoras;
using Models.Responses;

namespace ApiAseguradora.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurerController : ControllerBase
    {
        private readonly IAseguradoraBusiness _aseguradoraBusiness;
        public InsurerController(IAseguradoraBusiness aseguradoraBusiness)
        {
            _aseguradoraBusiness = aseguradoraBusiness;
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteInsurerController(int IdSeguro)
        {
            try
            {
                return Ok(await _aseguradoraBusiness.DeleteInsurerBusiness(IdSeguro));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(DeleteInsurerController)));
            }
        }
        [HttpGet("list")]
        public async Task<IActionResult> ListInsurersController(int? IdSeguro)
        {
            try
            {
                return Ok(await _aseguradoraBusiness.ListInsurersBusiness(IdSeguro));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(ListInsurersController)));
            }
        }
        [HttpPost("set")]
        public async Task<IActionResult> SetInsurerController(InsurerQuery aseguradora)
        {
            try
            {
                return Ok(await _aseguradoraBusiness.SetInsurerBusiness(aseguradora));
            }
            catch (Exception ex)
            {
                return Ok(RspHandler.BadResponse(ex, this.GetType().Name, nameof(SetInsurerController)));
            }
        }
    }
}

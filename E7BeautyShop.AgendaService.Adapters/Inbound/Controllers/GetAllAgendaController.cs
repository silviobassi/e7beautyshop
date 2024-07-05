using E7BeautyShop.AgendaService.Application;
using E7BeautyShop.AgendaService.Application.Ports.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace E7BeautyShop.AgendaService.Adapters.Inbound.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetAllAgendaController(IPersistenceQueryPort agendaPersistencePort): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAllAgendaResponse>>> GetAsync()
    {
        var agendas = await agendaPersistencePort.GetAllAgendasAsync();
        return Ok(agendas);
    }
}
using E7BeautyShop.AgendaService.Application;
using E7BeautyShop.AgendaService.Application.DTOs;
using E7BeautyShop.AgendaService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace E7BeautyShop.AgendaService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GetAllAgendaController(IPersistenceQuery agendaPersistence): ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgendaResponse>>> GetAsync()
    {
        var agendas = await agendaPersistence.GetAllAgendasAsync();
        return Ok(agendas);
    }
}